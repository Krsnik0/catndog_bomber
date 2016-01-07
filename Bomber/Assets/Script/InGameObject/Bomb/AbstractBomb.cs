using UnityEngine;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Manager;
using Boomscape.Data.Event.GameObject;

namespace Boomscape.InGameObject.Bomb
{
    public abstract class AbstractBomb : AbstractGameObject
    {

        public enum BombState
        {
            TICKING,
            EXPLODED
        };
        public AbstractBombValueObject bombData { get; set; }

        private float _countdown;
        private BombState _state;
        private bool _bombInitFlag = false;

        // Use this for initialization
        void Start()
        {
            initObject();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public override bool isObstacle
        {
            get
            {
                return true;
            }
        }

        public override GameMapLayer layer
        {
            get
            {
                return GameMapLayer.OBJECT;
            }
        }

        public void explode()
        {
            if (_state == BombState.TICKING)
            {
                _state = BombState.EXPLODED;
                EventManager.getInstance().dispatchEvent(new BombExplodeEvent(this));
                //GameMap.getInstance ().explode (this);
                destroyObject();
            }
        }

        public void startCountdown()
        {
            _countdown = bombData.explosionTime;
            _state = BombState.TICKING;
        }

        protected override void initObject()
        {
            if (!_bombInitFlag)
            {
                _bombInitFlag = true;
            }
        }

        protected override void updateObject()
        {
            //if (GameManager.getInstance ().currentState == GameManager.GameState.PLAYING && _state == BombState.TICKING && _countdown >= 0 ) {
            if (_state == BombState.TICKING && _countdown >= 0)
            {
                _countdown -= Time.deltaTime;

                if (_countdown < 0)
                {
                    explode();
                }
            }
        }
    }
}