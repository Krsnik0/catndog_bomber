using UnityEngine;
using System.Collections;
using Boomscape.Manager;

namespace Boomscape.Data.Event.State
{
    public class GameStateEvent : AbstractEvent
    {

        public const string STATE_EVENT_KEY = "stateEvent";

        GameState _prevState;
        GameState _nextState;

        public GameStateEvent(GameState prevState_, GameState nextState_)
        {
            this._prevState = prevState_;
            this._nextState = nextState_;
        }

        public GameStateEvent(GameState prevState_, GameState nextState_, object[] eventParams_) : base(eventParams_)
        {
            this._prevState = prevState_;
            this._nextState = nextState_;
        }

        public GameState prevState
        {
            get
            {
                return _prevState;
            }
        }

        public GameState nextState
        {
            get
            {
                return _nextState;
            }
        }

        public override string eventKey
        {
            get
            {
                return STATE_EVENT_KEY;
            }
        }
    }
}