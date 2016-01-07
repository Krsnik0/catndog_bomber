using UnityEngine;
using System.Collections;
using Boomscape.Manager;
using Boomscape.Data.Event.Input;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.Event;
using Boomscape.Util;
using Boomscape.Util.PathFinding;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.InGameObject.Character
{
    public class PlayerCharacter : AbstractCharacter
    {

        private bool _playerInitFlag = false;

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

                EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
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
                return 3;
            }
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
    }
}