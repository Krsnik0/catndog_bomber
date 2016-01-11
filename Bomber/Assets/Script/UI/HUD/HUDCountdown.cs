using UnityEngine;
using Boomscape.InGameObject.Bomb;
using Boomscape.Data.Event;
using Boomscape.Data.Event.GameObject;
using Boomscape.Manager;

namespace Boomscape.UI.HUD
{
    class HUDCountdown : AbstractUI
    {
        private bool _initCountdownFlag = false;

        private HUDDigitalNumberCounter _num10;
        private HUDDigitalNumberCounter _num1;
        private HUDDigitalNumberCounter _numdot1;
        private HUDDigitalNumberCounter _numdot01;

        private float _time;
        public AbstractBomb targetBomb
        {
            get; set;
        }

        private void Start()
        {
            initObject();
        }

        private void Update()
        {
            updateObject();
        }

        protected override void initObject()
        {
            base.initObject();

            if( !_initCountdownFlag )
            {
                _initCountdownFlag = true;

                //DigitalNumber10
                _num10 = transform.Find("DigitalNumber10").gameObject.GetComponent<HUDDigitalNumberCounter>();
                _num1 = transform.Find("DigitalNumber1").gameObject.GetComponent<HUDDigitalNumberCounter>();
                _numdot1 = transform.Find("DigitalNumber.1").gameObject.GetComponent<HUDDigitalNumberCounter>();
                _numdot01 = transform.Find("DigitalNumber.01").gameObject.GetComponent<HUDDigitalNumberCounter>();

                EventManager.getInstance().addEventListener(BombExplodeEvent.BOMB_EXPLODE_EVENT_KEY, onBombExploded);
            }
        }

        protected override void updateObject()
        {
            if( _time > 0)
            {
                _time -= Time.deltaTime;

                if( _time < 0 )
                {
                    _time = 0;
                }

                _num10.setInteger(Mathf.FloorToInt(_time / 10) % 10);
                _num1.setInteger(Mathf.FloorToInt(_time ) % 10);
                _numdot1.setInteger(Mathf.FloorToInt( _time * 10) % 10);
                _numdot01.setInteger(Mathf.FloorToInt(_time * 100) % 10);
            }
        }

        private void onBombExploded( AbstractEvent event_ )
        {
            BombExplodeEvent bombExplodeEvent = event_ as BombExplodeEvent;

            if( bombExplodeEvent.targetGameObject == targetBomb)
            {
                Destroy(gameObject);
            }
        }

        public void start( float time_ )
        {
            _time = time_;
        }
    }
}
