using UnityEngine;
using System.Collections;

namespace Boomscape.Data.Event.Input
{
    public class DragInputEvent : InputEvent
    {

        public enum DragState
        {
            STARTED,
            DRAGGING,
            ENDED
        };

        private DragState _dragState;
        private Vector2 _dragDir;
        private Vector2 _dragOrigin;

        public DragInputEvent(AbstractBoomscapeObject target_, DragState dragState_, Vector2 dragOrigin_, Vector2 dragDir_) : base(target_)
        {
            this._dragState = dragState_;
            this._dragDir = dragDir_;
            this._dragOrigin = dragOrigin_;
        }

        public DragInputEvent(Vector2 targetPos_, DragState dragState_, Vector2 dragOrigin_, Vector2 dragDir_) : base(targetPos_)
        {
            this._dragState = dragState_;
            this._dragDir = dragDir_;
            this._dragOrigin = dragOrigin_;
        }

        public override InputType inputType
        {
            get
            {
                return InputType.DRAG;
            }
        }

        public Vector2 dragDirection
        {
            get
            {
                return _dragDir;
            }
        }

        public Vector2 dragOrigin
        {
            get
            {
                return _dragOrigin;
            }
        }

        public DragState dragState
        {
            get
            {
                return _dragState;
            }
        }
    }
}