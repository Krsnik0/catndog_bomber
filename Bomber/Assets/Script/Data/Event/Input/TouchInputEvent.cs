using UnityEngine;
using System.Collections;

namespace Boomscape.Data.Event.Input
{
    public class TouchInputEvent : InputEvent
    {

        public TouchInputEvent(AbstractBoomscapeObject target_) : base(target_)
        {
        }

        public TouchInputEvent(Vector2 inputPos_) : base(inputPos_)
        {
        }

        public override InputType inputType
        {
            get
            {
                return InputType.TOUCH;
            }
        }
    }
}