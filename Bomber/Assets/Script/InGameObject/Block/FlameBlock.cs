using UnityEngine;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Data.Event.GameObject;
using Boomscape.Data.Event.Request;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Manager;

namespace Boomscape.InGameObject.Block
{
    public class FlameBlock : AbstractBlock
    {

        private bool _flameBlockInitFlag = false;

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

            if (!_flameBlockInitFlag)
            {
                _flameBlockInitFlag = true;

                _blockState = BlockState.DESTROYING;

                GetComponentInChildren<SpriteRenderer>().material.color = new Color(1f, 0, 0, 0.3f);

                Invoke("destroyObject", 0.3f);
            }
        }

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
        }

        protected override void updateObject()
        {
            switch (_blockState)
            {
                case BlockState.DESTROYING:
                    Color c = _spriteRenderer.material.color;
                    c.a -= 1f * Time.deltaTime;
                    _spriteRenderer.material.color = c;
                    break;
            }
        }

        public override bool isObstacle
        {
            get
            {
                return false;
            }
        }

        public override void destroyObject()
        {
            EventManager.getInstance().dispatchEvent(new ObjectRemovedEvent(this));
            EventManager.getInstance().dispatchEvent(new UpdateRequestEvent(typeof(GameMap)));
            //GameMap.getInstance ().removeObject (this);
            Destroy(gameObject);
        }
    }
}