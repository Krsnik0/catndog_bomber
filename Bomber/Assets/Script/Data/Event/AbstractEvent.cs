using UnityEngine;
using System.Collections;

namespace Boomscape.Data.Event
{
    public abstract class AbstractEvent
    {
        private AbstractBoomscapeObject _target;
        private object[] _eventParams;

        public AbstractEvent(AbstractBoomscapeObject target_)
        {
            _target = target_;
            _eventParams = new object[0];
        }

        public AbstractEvent(AbstractBoomscapeObject target_, object[] eventParams_ )
        {
            _target = target_;
            _eventParams = eventParams_;
        }

        public AbstractEvent()
        {
            _target = null;
        }

        public AbstractEvent(object[] eventParams_)
        {
            _target = null;
            _eventParams = eventParams_;
        }

        public AbstractBoomscapeObject target
        {
            get
            {
                return _target;
            }
        }

        public object[] eventParams
        {
            get
            {
                return _eventParams;
            }
        }

        abstract public string eventKey { get; }
    }
}