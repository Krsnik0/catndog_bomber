using UnityEngine;
using System;
using System.Collections;

namespace Boomscape.Data.Event.Request
{
    public class UpdateRequestEvent : AbstractEvent
    {

        public const string UPDATE_REQUEST_EVENT_KEY = "updateRequest";

        private System.Object _extraData;
        private Type _targetType;

        public UpdateRequestEvent(Type targetType_, System.Object extraData_)
        {
            _extraData = extraData_;
            _targetType = targetType_;
        }

        public UpdateRequestEvent(Type targetType_)
        {
            _extraData = null;
            _targetType = targetType_;
        }

        public System.Object extraData
        {
            get
            {
                return _extraData;
            }
        }

        public Type targetType
        {
            get
            {
                return _targetType;
            }
        }

        public override string eventKey
        {
            get
            {
                return UPDATE_REQUEST_EVENT_KEY;
            }
        }
    }
}