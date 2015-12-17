using UnityEngine;
using System.Collections;

public class GameStateEvent : AbstractEvent {

	public const string STATE_EVENT_KEY = "stateEvent";

	GameManager.GameState _prevState;
	GameManager.GameState _nextState;

	public GameStateEvent( GameManager.GameState prevState_, GameManager.GameState nextState_ )
	{
		this._prevState = prevState_;
		this._nextState = nextState_;
	}

	public GameManager.GameState prevState
	{
		get
		{
			return _prevState;
		}
	}

	public GameManager.GameState nextState
	{
		get
		{
			return _nextState;
		}
	}

	public override string eventKey {
		get {
			return STATE_EVENT_KEY;
		}
	}
}
