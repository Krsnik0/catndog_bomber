using UnityEngine;
using System.Collections;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Manager;
using Boomscape.Data.Event.State;
using Boomscape.Data.Event;
using Boomscape.Data.Event.GameObject;

namespace Boomscape.InGameObject.Block
{
    public class MarkerBlock : AbstractBlock
    {

        public enum MarkerType { NONE, THROW_RANGE, EXPLOSION_RANGE, BOMB_POSITION };

        private bool _markerBlockInitFlag = false;
        public MarkerType markerType { get; set; }

        public bool isTouchable { get; set; }
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

        public override GameMapLayer layer
        {
            get
            {
                return GameMapLayer.MARKER;
            }
        }

        protected override void initObject()
        {
            if (!_markerBlockInitFlag)
            {
                _markerBlockInitFlag = true;

                EventManager.getInstance().addEventListener(GameStateEvent.STATE_EVENT_KEY, onStateChanged);
            }
        }

        protected override void updateObject()
        {
        }

        private void onStateChanged(AbstractEvent event_)
        {
            GameStateEvent stateEvent = event_ as GameStateEvent;

            if (stateEvent.prevState == GameState.THROWING)
            {
                destroyObject();
            }
        }

        public override void destroyObject()
        {
            EventManager.getInstance().removeEventListener(GameStateEvent.STATE_EVENT_KEY, onStateChanged);
            EventManager.getInstance().dispatchEvent(new ObjectRemovedEvent(this));
            Destroy(gameObject);
        }
    }

}