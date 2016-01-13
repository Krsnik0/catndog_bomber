using UnityEngine;
using System.Collections;
using Boomscape.InGameObject;

namespace Boomscape.Data.Event.Input
{
    public abstract class InputEvent : AbstractEvent
    {

        public const string INPUT_EVENT_KEY = "InputEvent";

        private Vector2 _inputPos;

        public enum InputType
        {
            TOUCH,
            DRAG
        };

        public InputEvent(AbstractBoomscapeObject target_) : base(target_)
        {
            _inputPos = target_.transform.position;
        }

        public InputEvent(Vector2 inputPos_)
        {
            _inputPos = inputPos_;
        }

        public abstract InputType inputType { get; }

        public AbstractGameObject targetGameObject
        {
            get
            {
                if (target != null)
                {
                    return target as AbstractGameObject;
                }
                else
                {
                    return null;
                }
            }
        }

        public Vector2 inputPos
        {
            get
            {
                return _inputPos;
            }
        }

        public override string eventKey
        {
            get
            {
                return INPUT_EVENT_KEY;
            }
        }
    }
}