using UnityEngine;

using Boomscape.Manager;
using Boomscape.Data.Event;
using Boomscape.Data.Constant;
using Boomscape.Data.Event.GameObject;
using Boomscape.Util.Factory;

namespace Boomscape.UI.HUD
{
    public class HUDRoot : AbstractUI
    {

        private bool _initHUDRootFlag = false;

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
            if( !_initHUDRootFlag )
            {
                _initHUDRootFlag = true;

                EventManager.getInstance().addEventListener(BombPlacedEvent.BOMB_PLACED_EVENT_KEY, onBombPlaced);
            }
        }

        private void onBombPlaced( AbstractEvent event_ )
        {
            BombPlacedEvent bombPlacedEvent = event_ as BombPlacedEvent;

            HUDCountdown counter = GameObjectFactory.getInstance().generateObject(UIPrefabConst.HUD_BOMB_COUNTER_PATH).GetComponent<HUDCountdown>();
            Vector3 pos = bombPlacedEvent.targetGameObject.transform.position;
            pos.z = -30;
            counter.transform.position = pos;
            counter.transform.localScale = new Vector3(0.5f, 0.5f);
            counter.transform.parent = this.transform;
            counter.targetBomb = bombPlacedEvent.targetGameObject;
            counter.start(bombPlacedEvent.targetGameObject.bombData.explosionTime);
        }

        protected override void updateObject()
        {
        }
    }
}