using UnityEngine;
using System.Collections.Generic;
using Boomscape.InGameObject.Character;
using Boomscape.InGameObject.Bomb;
using Boomscape.Manager;
using Boomscape.Data.Event.GameObject;
using Boomscape.Data.ValueObject.Game.InGameObject;
using Boomscape.Util;
using Boomscape.InGameObject.Block;
using Boomscape.Data.DataManager;
using Boomscape.Util.Factory;
using Boomscape.Data.Event;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.InGameObject.Container.Map.Layer
{
    public class ObjectLayer : AbstractLayer
    {

        private bool _objLayerInitFlag = false;
        private PlayerCharacter _playerCharacter;

        private bool[][] _safezone;
        private List<AbstractBomb> _bombs;

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected override void initObject()
        {
            base.initObject();

            if (!_objLayerInitFlag)
            {
                _objLayerInitFlag = true;

                _bombs = new List<AbstractBomb>();
                EventManager.getInstance().addEventListener(BombExplodeEvent.BOMB_EXPLODE_EVENT_KEY, onBombExploded);
            }
        }

        public override void loadLayer(AbstractGameObjectValueObject[][] layerData_)
        {
            base.loadLayer(layerData_);

            _playerCharacter = GetComponentInChildren<PlayerCharacter>();
            _safezone = new bool[layerSize.x][];

            for (int i = 0; i < layerSize.x; ++i)
            {
                _safezone[i] = new bool[layerSize.y];
            }
        }

        public PlayerCharacter playerCharacter
        {
            get
            {
                return _playerCharacter;
            }
        }

        public override AbstractGameObject addObject(AbstractGameObject obj_)
        {
            if (obj_ is AbstractBomb)
            {
                _bombs.Add(obj_ as AbstractBomb);
                updateSafezone();
            }
            return base.addObject(obj_);
        }

        public void explode(AbstractBomb bomb_)
        {
            int i, j, k;
            IntegerPair bombPos = bomb_.positionIndexPair;
            IntegerPair bombCenter = bomb_.bombData.bombPosition;
            IntegerPair currentEffectPos;

            bool[][] explosionShape = bomb_.bombData.explosionShape;

            FlameBlock flameBlock;

            for (i = 0; i < explosionShape.Length; ++i)
            {
                for (j = 0; j < explosionShape[i].Length; ++j)
                {
                    if (explosionShape[i][j])
                    {
                        currentEffectPos = bombPos.clone().sub(bombCenter).add(new IntegerPair(i, j));

                        if (0 <= currentEffectPos.x && currentEffectPos.x < layerSize.x &&
                           0 <= currentEffectPos.y && currentEffectPos.y < layerSize.y)
                        {
                            if (isObjectExistAt(currentEffectPos.x, currentEffectPos.y))
                            {
                                getObjectAt(currentEffectPos.x, currentEffectPos.y).onExplosion(bomb_.bombData);
                            }

                            flameBlock = GameObjectFactory.getInstance().generateObject(BlockDataManager.getInstance().findBlockData("SYSBLOCK0001").prefabData.Value).GetComponent<FlameBlock>();
                            flameBlock.positionIndexPair = currentEffectPos.clone();
                            flameBlock.transform.parent = transform;        // WARNING: must not add to layerHashmap. May overwrite not destructed block.


                            for (k = _nonObstacleObjects.Count - 1; k >= 0; --k)
                            {
                                if (PositionCalcUtil.tileRectFromIdxPair(_nonObstacleObjects[k].positionIndexPair).Overlaps(
                                    PositionCalcUtil.tileRectFromIdxPair(currentEffectPos))
                                   )
                                {
                                    _nonObstacleObjects[k].onExplosion(bomb_.bombData);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void onBombExploded(AbstractEvent event_)
        {
            BombExplodeEvent bombEvent = event_ as BombExplodeEvent;

            if (bombEvent.targetGameObject != null)
            {
                _bombs.Remove(bombEvent.targetGameObject);
                explode(bombEvent.targetGameObject);

                updateSafezone();
            }
        }

        private void updateSafezone()
        {
            int i, j, k;
            IntegerPair leftTop, rightBottom;
            IntegerPair position;
            AbstractBombValueObject bombData;

            for (i = 0; i < _safezone.Length; ++i)
            {
                for (j = 0; j < _safezone[i].Length; ++j)
                {
                    _safezone[i][j] = true;
                }
            }

            for (i = 0; i < _bombs.Count; ++i)
            {
                position = _bombs[i].positionIndexPair;
                bombData = _bombs[i].bombData;
                leftTop = new IntegerPair(
                                                  Mathf.Max(position.x - bombData.bombPosition.x, 0),
                                                  Mathf.Max(position.y - bombData.bombPosition.y, 0));
                rightBottom = new IntegerPair(
                                                          Mathf.Min(position.x - bombData.bombPosition.x + bombData.explosionShape.Length, layerSize.x - 1),
                                                          Mathf.Min(position.y - bombData.bombPosition.y + bombData.explosionShape[0].Length, layerSize.y - 1));

                for (j = leftTop.x; j < rightBottom.x; ++j)
                {
                    for (k = leftTop.y; k < rightBottom.y; ++k)
                    {
                        if (bombData.explosionShape[j - leftTop.x][k - leftTop.y])
                        {
                            _safezone[j][k] = false;
                        }
                    }
                }
            }
        }

        public bool isSafe(int x_, int y_)
        {
            return _safezone[x_][y_];
        }
    }
}