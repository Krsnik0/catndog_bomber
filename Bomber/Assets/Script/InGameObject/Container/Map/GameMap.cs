using UnityEngine;
using System.Collections;
using System.Xml;
using Boomscape.Util;
using Boomscape.Data.ValueObject.Game;
using Boomscape.InGameObject.Container.Map.Layer;
using Boomscape.Manager;
using Boomscape.Data.Event.Request;
using Boomscape.Data.Event.GameObject;
using Boomscape.Data.Event.State;
using Boomscape.Data.Constant;
using Boomscape.Data.Event.Input;
using Boomscape.Data.Event;
using Boomscape.UI;
using Boomscape.InGameObject.Block;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.UI.InGame;
using Boomscape.InGameObject.Bomb;
using Boomscape.Util.Factory;

namespace Boomscape.InGameObject.Container.Map
{
    public enum GameMapLayer { TILE, OBJECT, MARKER };

    public class GameMap : AbstractContainerObject
    {
        static private GameMap _instance;

        public IntegerPair mapSize { get; set; }

        private bool _mapInitFlag = false;
        private bool _mapUpdatePerFrameFlag;            // updateMap method must be called once per frame

        private StageValueObject _stageData;
        private MarkerLayer _markerLayer;
        private ObjectLayer _objectLayer;
        private TileLayer _tileLayer;

        public BoxCollider2D boxCollider { get; set; }

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {
            updateObject();
        }

        protected override void updateObject()
        {
            _mapUpdatePerFrameFlag = true;
            base.updateObject();
        }

        static public GameMap getInstance()
        {
            return _instance;
        }

        protected override void initObject()
        {
            base.initObject();

            if (!_mapInitFlag)
            {
                _mapInitFlag = true;

                _instance = this;

                _markerLayer = GetComponentInChildren<MarkerLayer>();
                _objectLayer = GetComponentInChildren<ObjectLayer>();
                _tileLayer = GetComponentInChildren<TileLayer>();

                addObject(_markerLayer);
                addObject(_objectLayer);
                addObject(_tileLayer);

                EventManager.getInstance().addEventListener(UpdateRequestEvent.UPDATE_REQUEST_EVENT_KEY, onUpdateRequest);
                EventManager.getInstance().addEventListener(ObjectRemovedEvent.OBJECT_REMOVED_EVENT_KEY, onObjectRemoved);
                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onStateChanged);
            }
        }

        public void loadStage(StageValueObject mapData_)
        {
            _stageData = mapData_;

            mapSize = mapData_.mapSize.clone();
            _objectLayer.loadLayer(mapData_.objLayer);
            _tileLayer.loadLayer(mapData_.tileLayer);
            _markerLayer.loadEmptyLayer(mapSize);

            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(mapSize.x * GameMapConst.BLOCK_SIZE_WIDTH, mapSize.y * GameMapConst.BLOCK_SIZE_HEIGHT);
            boxCollider.offset = boxCollider.size / 2;
            EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
        }

        public bool checkCleared()
        {
            bool ret = _stageData.goal.ToString() == _objectLayer.playerCharacter.positionIndexPair.ToString();

            if (ret)
            {
                GameManager.getInstance().changeState(GameState.CLEAR);
            }

            return ret;
        }

        public bool isMovable(int x_, int y_)
        {
            if (0 <= x_ && x_ < mapSize.x &&
                0 <= y_ && y_ < mapSize.y)
            {
                return !_objectLayer.isObjectExistAt(x_, y_);
            }
            else
            {
                return false;
            }
        }

        public bool isThrowable(int x_, int y_)
        {
            return isMovable(x_, y_) && !_objectLayer.nonObstacleObjExistAt(x_, y_);
        }

        public bool isSafe(int x_, int y_)
        {
            return _objectLayer.isSafe(x_, y_);
        }

        public override AbstractGameObject addObject(AbstractGameObject obj_)
        {
            switch (obj_.layer)
            {
                case GameMapLayer.MARKER:
                    return _markerLayer.addObject(obj_);
                case GameMapLayer.OBJECT:
                    return _objectLayer.addObject(obj_);
                case GameMapLayer.TILE:
                    return _tileLayer.addObject(obj_);
            }

            return null;
        }

        public override AbstractGameObject removeObject(AbstractGameObject obj_)
        {
            switch (obj_.layer)
            {
                case GameMapLayer.MARKER:
                    return _markerLayer.removeObject(obj_);
                case GameMapLayer.OBJECT:
                    return _objectLayer.removeObject(obj_);
                case GameMapLayer.TILE:
                    return _tileLayer.removeObject(obj_);
            }

            return null;
        }

        public override bool contains(AbstractGameObject obj_)
        {
            switch (obj_.layer)
            {
                case GameMapLayer.MARKER:
                    return _markerLayer.contains(obj_);
                case GameMapLayer.OBJECT:
                    return _objectLayer.contains(obj_);
                case GameMapLayer.TILE:
                    return _tileLayer.contains(obj_);
            }

            return false;
        }

        public override void moveObject(AbstractGameObject obj_, IntegerPair dst_)
        {
            switch (obj_.layer)
            {
                case GameMapLayer.MARKER:
                    _markerLayer.moveObject(obj_, dst_);
                    break;
                case GameMapLayer.OBJECT:
                    _objectLayer.moveObject(obj_, dst_);
                    break;
                case GameMapLayer.TILE:
                    _tileLayer.moveObject(obj_, dst_);
                    break;
            }
        }

        private int getThrowRange( int bombThrowRange )
        {
            return bombThrowRange + (int)_objectLayer.playerCharacter.strength;
        }

        private void onInputEvent(AbstractEvent event_)
        {
            InputEvent inputEvent = (InputEvent)event_;

            if (inputEvent.target is AbstractUI)
            {
                return;
            }

            switch (GameManager.getInstance().currentState)
            {
                case GameState.THROWING:

                    if (inputEvent.target is MarkerBlock)
                    {
                        AbstractBombValueObject bombData;

                        MarkerBlock marker = inputEvent.targetGameObject as MarkerBlock;
                        IntegerPair posIdxPair = marker.positionIndexPair;

                        bombData = RootUI.getInstance().uiData.selectedBomb;

                        switch (inputEvent.inputType)
                        {
                            case InputEvent.InputType.TOUCH:
                                #region onTouchInput
                                TouchInputEvent touchInput = (TouchInputEvent)inputEvent;

                                if (marker.markerType == MarkerBlock.MarkerType.BOMB_POSITION)
                                {
                                    AbstractBomb bomb = GameObjectFactory.getInstance().generateObject(bombData.prefabData.Value).GetComponent<AbstractBomb>();
                                    bomb.positionIndexPair = marker.positionIndexPair;
                                    bomb.bombData = bombData;
                                    bomb.startCountdown();
                                    addObject(bomb);

                                    GameManager.getInstance().changeState(GameState.PLAYING);
                                }
                                else if (!_objectLayer.isObjectExistAt(posIdxPair.x, posIdxPair.y))
                                {
                                    _markerLayer.resetBackbuffer();

                                    _markerLayer.markThrowableArea(_objectLayer.playerCharacter.positionIndexPair, getThrowRange( bombData.throwRange ));
                                    _markerLayer.markExplosionArea(posIdxPair,
                                                                    bombData.explosionShape,
                                                                    bombData.bombPosition);

                                    _markerLayer.drawBackbuffer();
                                }
                                #endregion
                                break;
                            case InputEvent.InputType.DRAG:

                                #region onDragInput
                                DragInputEvent dragInput = (DragInputEvent)inputEvent;

                                switch (dragInput.dragState)
                                {
                                    case DragInputEvent.DragState.DRAGGING:
                                        //AbstractGameObject originObj = dragInput.target;
                                        posIdxPair = PositionCalcUtil.vector3ToMapIndex(new Vector3(dragInput.inputPos.x, dragInput.inputPos.y));

                                        if (_markerLayer.isSelectedBlock(posIdxPair))
                                        {
                                            bombData.currentSize -= Vector2.Dot(dragInput.inputPos - dragInput.dragOrigin, dragInput.dragDirection);
                                            //Debug.Log(Vector2.Dot(origin - dragInput.dragOrigin, dragInput.dragDirection) + "," + bombData.currentSize);

                                            _markerLayer.resetBackbuffer();
                                            //_markerLayer.removeAll(true);
                                            _markerLayer.markThrowableArea(_objectLayer.playerCharacter.positionIndexPair, getThrowRange( bombData.throwRange));
                                            _markerLayer.markExplosionArea(posIdxPair,
                                                                            bombData.explosionShape,
                                                                            bombData.bombPosition);
                                            _markerLayer.drawBackbuffer();
                                        }

                                        break;
                                }
                                #endregion

                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }

        private void updateMap()
        {
            if (_mapUpdatePerFrameFlag)
            {
                _mapUpdatePerFrameFlag = false;

                switch (GameManager.getInstance().currentState)
                {
                    case GameState.THROWING:
                        _markerLayer.resetBackbuffer();

                        _markerLayer.markThrowableArea(_objectLayer.playerCharacter.positionIndexPair, getThrowRange(RootUI.getInstance().uiData.selectedBomb.throwRange));
                        IntegerPair selectedPos = _markerLayer.getSelectedPos();
                        if (selectedPos != null && isThrowable(selectedPos.x, selectedPos.y))
                        {
                            AbstractBombValueObject bombData;
                            bombData = RootUI.getInstance().uiData.selectedBomb;
                            _markerLayer.markExplosionArea(_markerLayer.getSelectedPos(),
                                                                            bombData.explosionShape,
                                                                            bombData.bombPosition);
                        }
                        else
                        {
                            _markerLayer.resetSelectedPos();
                        }
                        //_markerLayer.removeAll(true);
                        _markerLayer.drawBackbuffer();
                        break;
                }
            }
        }

        private void onStateChanged(AbstractEvent event_)
        {
            GameStateEvent stateEvent = event_ as GameStateEvent;

            if (stateEvent.prevState == GameState.THROWING)
            {
                _markerLayer.resetSelectedPos();
            }
        }

        private void onUpdateRequest(AbstractEvent event_)
        {
            UpdateRequestEvent reqEvent = event_ as UpdateRequestEvent;

            if (reqEvent.targetType == GetType())
            {
                updateMap();
            }
        }

        private void onObjectRemoved(AbstractEvent event_)
        {
            ObjectRemovedEvent removedEvent = event_ as ObjectRemovedEvent;

            if (removedEvent.targetGameObject != null)
            {
                removeObject(removedEvent.targetGameObject);
            }
        }
    }
}