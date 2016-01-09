using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomscape.Manager;
using Boomscape.Data.Event.Input;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.Event;
using Boomscape.Util;
using Boomscape.Util.PathFinding;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Data.ValueObject.Game.InGameObject.Item;
using Boomscape.Data.Event.State;
using Boomscape.Data.Constant;

namespace Boomscape.InGameObject.Character
{
    public class PlayerCharacter : AbstractCharacter
    {

        private bool _playerInitFlag = false;

        private List<ItemValueObject> _itemOnEffect;

        private float str;
        private float stm;
        private float spd;

        private float str_add;
        private float stm_add;
        private float spd_add;

        private void Start()
        {
            initObject();
        }

        private void Update()
        {
            updateObject();
        }

        protected override void initObject()
        {
            if (!_playerInitFlag)
            {
                _playerInitFlag = true;

                _itemOnEffect = new List<ItemValueObject>();

                EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onGameStateChanged);
            }
        }

        protected override void updateObject()
        {
            base.updateObject();

            GameMap.getInstance().checkCleared();
        }

        private void onInputEvent(AbstractEvent event_)
        {
            InputEvent inputEvent = event_ as InputEvent;

            if (inputEvent.inputType == InputEvent.InputType.TOUCH && event_.target is GameMap)
            {
                IntegerPair dst = PositionCalcUtil.vector3ToMapIndex(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                setPath(PathFinder.findPath(GameMap.getInstance(), positionIndexPair, dst));
            }
        }

        public override float speed
        {
            get
            {
                return spd + spd_add;
            }
        }

        public float strength
        {
            get
            {
                return str+str_add;
            }
        }

        public float stamina
        {
            get
            {
                return stm + stm_add;
            }
        }

        public void getItem( ItemValueObject item_ )
        {
            _itemOnEffect.Add(item_);           // add item collided

            switch( item_.code )                // add item effect when player got code here
            {
                case ItemKindConst.SPEED_ITEM:
                    break;
                case ItemKindConst.STAMINA_ITEM:
                    break;
                case ItemKindConst.STRENGTH_ITEM:
                    break;
            }

            StartCoroutine(loseItemOnTimeUp(item_));

            Debug.Log("item got : " + item_.code);
        }

        public void loseItem( ItemValueObject item_ )
        {
            bool ret = _itemOnEffect.Remove(item_);

            if( ret )
            {
                switch (item_.code)                // add item effect when player lost code here
                {
                    case ItemKindConst.SPEED_ITEM:
                        break;
                    case ItemKindConst.STAMINA_ITEM:
                        break;
                    case ItemKindConst.STRENGTH_ITEM:
                        break;
                }

                Debug.Log("item lost : " + item_.code);
            }
        }

        [SerializeField]
        private IEnumerator loseItemOnTimeUp( ItemValueObject item_ )
        {
            yield return new WaitForSeconds(item_.effectTime);

            loseItem(item_);
        }

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
            destroyObject();
        }

        public override void destroyObject()
        {
            GameManager.getInstance().changeState(GameState.GAME_OVER);
            base.destroyObject();
        }

        private void onGameStateChanged( AbstractEvent event_ )
        {
            GameStateEvent gameStateEvent = event_ as GameStateEvent;

            switch( gameStateEvent.prevState )
            {
                case GameState.LOOK_AROUND:
                    // stat set
                    
                    str = (float)gameStateEvent.eventParams[0];
                    stm = (float)gameStateEvent.eventParams[1];
                    spd = (float)gameStateEvent.eventParams[2];

                    str_add = 0;
                    stm_add = 0;
                    spd_add = 0;

                    break;
            }
        }
    }
}