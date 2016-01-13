using UnityEngine;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Manager;
using Boomscape.Data.Event.Input;
using Boomscape.Data.Event;

namespace Boomscape.UI.InGame
{
    public class BombIconUI : AbstractUI
    {

        private bool _initBombIconFlag = false;

        private AbstractBombValueObject _bombData;

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

        protected override void initObject()
        {
            base.initObject();

            if (!_initBombIconFlag)
            {
                _initBombIconFlag = true;

                EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
            }
        }

        protected override void updateObject()
        {

        }

        private void onInputEvent(AbstractEvent event_)
        {
            InputEvent inputEvent = event_ as InputEvent;
            if (inputEvent.inputType == InputEvent.InputType.TOUCH)
            {
                TouchInputEvent touchinputEvent = inputEvent as TouchInputEvent;

                if (touchinputEvent.target == this)
                {
                    onClick();
                }
            }
        }

        public override void onChildClick(AbstractUI child_)
        {
            base.onChildClick(child_);
        }

        public AbstractBombValueObject bombData
        {
            get
            {
                return _bombData;
            }

            set
            {
                _bombData = value;
            }
        }
    }
}