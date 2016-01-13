using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Manager;
using Boomscape.Util.Factory;
using Boomscape.Data.Event.State;
using Boomscape.Data.Event;

namespace Boomscape.UI.InGame
{
    public class BombLister : AbstractUI
    {

        private bool _bombListerInitFlag = false;

        private BombCursor _cursor;
        private AbstractBombValueObject _selectedBomb;

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

        protected override void initObject()
        {
            base.initObject();
            if (!_bombListerInitFlag)
            {
                _bombListerInitFlag = true;
                _cursor = GetComponentInChildren<BombCursor>();
                _cursor.setVisibility(false);

                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onThrowingStateEnded);
            }
        }

        private void onThrowingStateEnded(AbstractEvent event_)
        {
            GameStateEvent stateEvent = event_ as GameStateEvent;

            if (stateEvent.prevState == GameState.THROWING)
            {
                _cursor.setVisibility(false);
            }
        }

        public void loadAllowedBombsData(AbstractBombValueObject[] allowedBombs_)
        {
            Transform backgroundTransform = transform.Find("Background");
            Vector3 localScale = backgroundTransform.localScale;

            localScale.x *= allowedBombs_.Length;
            backgroundTransform.localScale = localScale;

            //BoxCollider2D backgroundCollider = background.GetComponent<BoxCollider2D>();
            //Vector2 colliderSize = backgroundCollider.size;
            //colliderSize.x *= allowedBombs_.Length;


            GameObject icon;
            Transform iconTransform;
            float offset;

            if (allowedBombs_.Length % 2 == 0)
            {
                offset = Mathf.Floor(allowedBombs_.Length / 2) - 0.5f;
            }
            else
            {
                offset = Mathf.Floor(allowedBombs_.Length / 2);
            }

            for (int i = 0; i < allowedBombs_.Length; ++i)
            {
                icon = GameObjectFactory.getInstance().generateObject(allowedBombs_[i].iconPath.Value);
                icon.GetComponent<BombIconUI>().bombData = allowedBombs_[i];
                icon.transform.SetParent(transform);
                iconTransform = (Transform)icon.transform;
                iconTransform.localPosition = new Vector3(1f * i - offset, 0, 0);
            }

            updateChildrenList();
        }

        protected override void updateObject()
        {
        }

        public override void onChildClick(AbstractUI child_)
        {
            if (child_ is BombIconUI)
            {
                GameManager.getInstance().changeState(GameState.THROWING);

                _cursor.setVisibility(true);
                _cursor.transform.localPosition = child_.transform.localPosition;
                _selectedBomb = child_.GetComponent<BombIconUI>().bombData;

                base.onChildClick(child_);
            }
        }

        public void cancelBombSelection()
        {
            _selectedBomb = null;
        }

        public AbstractBombValueObject selectedBomb
        {
            get
            {
                return _selectedBomb;
            }
        }
    }
}