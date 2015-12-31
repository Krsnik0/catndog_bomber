using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        LOADING,
        LOOK_AROUND,
        PAUSED,
        THROWING,
        PLAYING,
        CLEAR,
        GAME_OVER
    };

    public float _lookAroundTime;

    static private GameManager _instance;

    private bool _initFlag;

    private GameState _currentState;

    private GameMap _map;
    private RootUI _rootUI;
    private StageValueObject _stageData;

    void Start()
    {

        _instance = this;
        _currentState = GameState.LOADING;

        _map = GetComponentInChildren<GameMap>();
        _rootUI = GetComponentInChildren<RootUI>();
        _initFlag = false;
    }

    void Update()
    {
        if (!_initFlag)
        {
            _initFlag = true;

            TileDataManager.getInstance().loadData();
            BlockDataManager.getInstance().loadData();
            CharacterDataManager.getInstance().loadData();
            BombDataManager.getInstance().loadData();

            List<KeyValuePair<System.Type, string>> usedObjects;
            _stageData = GameStageParser.parseMap("stage_0001", out usedObjects);

            StartCoroutine(ResourceManager.getInstance().LoadResourcesByCoroutine(usedObjects, onResourceLoaded));
        }

        switch (_currentState)
        {
            case GameState.LOADING:
                break;
            case GameState.LOOK_AROUND:
                break;
            case GameState.GAME_OVER:
                break;
            case GameState.CLEAR:
                break;
            default:
                InputManager.getInstance().triggerInput();
                break;

        }
    }

    static public GameManager getInstance()
    {
        return _instance;
    }

    public GameState currentState
    {
        get
        {
            return _currentState;
        }
    }

    private void onResourceLoaded()
    {
        changeState(GameState.LOOK_AROUND);
    }

    public void changeState(GameState newState_)
    {
        if (_currentState != newState_)
        {
            Debug.Log("State change : " + _currentState.ToString() + " -> " + newState_.ToString());
            EventManager.getInstance().dispatchEvent(new GameStateEvent(_currentState, newState_));
            onStateEnd(_currentState);
            _currentState = newState_;
            onStateStart(_currentState);
        }
    }

    private void onInputEvent(AbstractEvent event_)
    {
        if( currentState == GameState.THROWING && ( event_.target == null || event_.target is GameMap ) )
        {
            changeState(GameState.PLAYING);
        }
    }

    private void onStateEnd(GameState state_)
    {
        switch (state_)
        {
            case GameState.LOADING:
                _map.loadStage(_stageData);
                _rootUI.loadStageUI(_stageData);

                EventManager.getInstance().addEventListener(InputEvent.INPUT_EVENT_KEY, onInputEvent);
                break;
            case GameState.THROWING:
                Time.timeScale = 1f;
                break;
        }
    }

    private void onStateStart(GameState state_)
    {
        switch (state_)
        {
            case GameState.LOOK_AROUND:
                Camera.main.GetComponent<CameraScript>().moveCamTo(PositionCalcUtil.mapIndexToVector3(_stageData.goal), _lookAroundTime);
                Invoke("onCamOnGoal", _lookAroundTime * 2);
                break;
            case GameState.THROWING:
                Time.timeScale = 0.1f;
                break;
        }
    }


    // TODO : refactor. implement cam action queue
    private void onCamOnGoal()
    {
        Camera.main.GetComponent<CameraScript>().moveCamTo(PositionCalcUtil.mapIndexToVector3(_stageData.entryPoint), _lookAroundTime);
        Invoke("onCamReturn", _lookAroundTime);
    }

    private void onCamReturn()
    {
        changeState(GameState.PLAYING);
    }
}
