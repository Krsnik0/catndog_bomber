using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public enum GameState
	{
		LOADING,
		PAUSED,
		THROWING,
		PLAYING
	};

	static private GameManager _instance;

	private bool _initFlag;

	private GameState _currentState;

	private GameMap _map;
	private RootUI _rootUI;
	private StageValueObject _stageData;

	// Use this for initialization
	void Start () {
	
		_instance = this;
		_currentState = GameState.LOADING;

		_map = GetComponentInChildren<GameMap> ();
		_rootUI = GetComponentInChildren<RootUI> ();
		_initFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_initFlag) {
			_initFlag = true;

			BlockDataManager.getInstance ().loadData ();
			CharacterDataManager.getInstance ().loadData ();
			BombDataManager.getInstance().loadData();

			List<KeyValuePair<System.Type, string>> usedObjects;
			_stageData = GameStageParser.parseMap( "stage_0001", out usedObjects );

			StartCoroutine( ResourceManager.getInstance().LoadResourcesByCoroutine(usedObjects, onResourceLoaded) );
		}

        InputManager.getInstance().triggerInput();
    }

	static public GameManager getInstance()
	{
		return _instance;
	}

	public GameState currentState
	{
		get {
			return _currentState;
		}
	}

	private void onResourceLoaded()
	{
		changeState (GameState.PLAYING);
	}

	public void changeState( GameState newState_ )
	{
		if (_currentState != newState_) {
			Debug.Log ("State change : " + _currentState.ToString () + " -> " + newState_.ToString ());
			EventManager.getInstance().dispatchEvent( new GameStateEvent( _currentState, newState_ ) );
			onStateEnd (_currentState);
			_currentState = newState_;
			onStateStart (_currentState);
		}
	}

	private void onStateEnd( GameState state_ )
	{
		switch (state_) {
		case GameState.LOADING:
			_map.loadStage( _stageData );
			_rootUI.loadStageUI( _stageData );
			break;
		case GameState.THROWING:
			Time.timeScale = 1f;
			break;
		}
	}

	private void onStateStart( GameState state_ )
	{
		switch (state_) {
		case GameState.THROWING:
			Time.timeScale = 0.1f;
			//Time.fixedDeltaTime = Time.fixedDeltaTime
			break;
		}
	}
}
