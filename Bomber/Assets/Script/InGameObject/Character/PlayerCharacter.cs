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
        private Animator _animator;

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

                _animator = GetComponentInChildren<Animator>();

                EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onGameStateChanged);
            }
        }

        protected override void updateObject()
        {
            base.updateObject();

            GameMap.getInstance().checkCleared();

            _animator.SetBool("R", false);
            _animator.SetBool("L", false);
            _animator.SetBool("U", false);
            _animator.SetBool("D", false);

            Vector2 dir = movingDirection;

            if ( dir.x > 0)
            {
                _animator.SetBool("R", true);
            }
            else if( dir.x < 0)
            {
                _animator.SetBool("L", true);
            }
            else if( dir.y > 0 )
            {
                _animator.SetBool("U", true);
            }
            else if( dir.y < 0 )
            {
                _animator.SetBool("D", true);
            }
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
                return str + str_add;
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
                    spd_add = spd * 0.5f;
                    Debug.Log("speed increase");
                    break;
                case ItemKindConst.STAMINA_ITEM:
                    stm_add = stm * 0.3f;
                    Debug.Log("stamina increase");
                    break;
                case ItemKindConst.STRENGTH_ITEM:
                    str_add = str * 0.3f;
                    Debug.Log("strength increase");
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
                        if (!isItemLeft(item_.code))
                        {
                            spd_add = 0;
                            Debug.Log("speed rollback");
                        }
                        else
                        {
                            Debug.Log("speed not rollback");
                        }
                        break;
                    case ItemKindConst.STAMINA_ITEM:
                        if (!isItemLeft(item_.code))
                        {
                            stm_add = 0;
                            Debug.Log("stamina rollback");
                        }
                        else
                        {
                            Debug.Log("stamina not rollback");
                        }
                        break;
                    case ItemKindConst.STRENGTH_ITEM:
                        if (!isItemLeft(item_.code))
                        {
                            str_add = 0;
                            Debug.Log("strength rollback");
                        }
                        else
                        {
                            Debug.Log("strength rollback");
                        }
                        break;
                }

                Debug.Log("item lost : " + item_.code);
            }
        }

        public bool isItemLeft(string item_kind)
        {
            int i = 0;
            int length = _itemOnEffect.Count;

            for(i = 0; i< length; i++)
            {
                if(_itemOnEffect[i].code == item_kind)
                {
                    return true;
                }                
            }
            return false;
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