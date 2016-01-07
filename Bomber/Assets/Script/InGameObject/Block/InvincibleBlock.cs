using UnityEngine;
using System.Collections;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.InGameObject.Block
{
    public class InvincibleBlock : AbstractBlock
    {

        public bool isMovable;
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

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
        }

        public override void destroyObject()
        {
        }

        protected override void updateObject()
        {
        }
    }
}