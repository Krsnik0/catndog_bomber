using UnityEngine;
using System.Collections;
using Boomscape.UI.InGame;
using Boomscape.Data.ValueObject.System.UI;
using Boomscape.Manager;
using Boomscape.Data.Event.State;
using Boomscape.Data.ValueObject.Game;
using Boomscape.Data.Event.Request;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.Event;

namespace Boomscape.UI.InGame
{
    public class RootUI : AbstractUI
    {

        static private RootUI _instance;

        private bool _rootUIInitFlag = false;

        private BombLister _bombLister;
        private UIDataValueObject _uiData;

        private GameOverMsgUI _gameOverMsg;
        private StageClearMsgUI _stageClearMsg;

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

        static public RootUI getInstance()
        {
            return _instance;
        }

        protected override void initObject()
        {
            base.initObject();

            if (!_rootUIInitFlag)
            {
                _instance = this;

                _uiData = new UIDataValueObject();
                _rootUIInitFlag = true;
                _bombLister = GetComponentInChildren<BombLister>();
                _gameOverMsg = GetComponentInChildren<GameOverMsgUI>();
                _stageClearMsg = GetComponentInChildren<StageClearMsgUI>();

                _gameOverMsg.setVisibility(false);
                _stageClearMsg.setVisibility(false);

                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onStateChanged);
            }
        }

        protected override void updateObject()
        {

        }

        public void loadStageUI(StageValueObject stageData_)
        {
            _bombLister.loadAllowedBombsData(stageData_.allowedBombs);
        }

        public UIDataValueObject uiData
        {
            get
            {
                return _uiData;
            }
        }

        public override void onClick()
        {

        }

        private void updateUIData()
        {
            _uiData.selectedBomb = _bombLister.selectedBomb;
        }

        public override void onChildClick(AbstractUI child_)
        {
            updateUIData();
            EventManager.getInstance().dispatchEvent(new UpdateRequestEvent(typeof(GameMap)));
        }

        private void onStateChanged(AbstractEvent event_)
        {
            GameStateEvent stateEvent = event_ as GameStateEvent;

            switch (stateEvent.nextState)
            {
                case GameState.GAME_OVER:
                    _gameOverMsg.setVisibility(true);
                    break;
                case GameState.CLEAR:
                    _stageClearMsg.setVisibility(true);
                    break;
            }
        }
    }

}
