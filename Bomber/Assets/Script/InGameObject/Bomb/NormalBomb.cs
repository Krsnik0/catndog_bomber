using UnityEngine;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Manager;
using Boomscape.Data.Event.GameObject;

namespace Boomscape.InGameObject.Bomb
{
    public class NormalBomb : AbstractBomb
    {

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
        }

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
            explode();
        }

        public override void destroyObject()
        {
            EventManager.getInstance().dispatchEvent(new ObjectRemovedEvent(this));
            Destroy(gameObject);
        }
    }
}