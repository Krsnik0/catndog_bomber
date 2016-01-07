using UnityEngine;
using System.Collections;
using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.InGameObject.Block
{

    public abstract class AbstractBlock : AbstractGameObject
    {

        protected enum BlockState { NORMAL, DESTROYING, MOVING };
        protected BlockState _blockState { get; set; }

        private bool _blockInitFlag = false;

        protected SpriteRenderer _spriteRenderer;

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
            if (!_blockInitFlag)
            {
                _blockInitFlag = true;

                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }

        public override GameMapLayer layer
        {
            get
            {
                return GameMapLayer.OBJECT;
            }
        }

        public override bool isObstacle
        {
            get
            {
                return true;
            }
        }

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
            destroyObject();
        }
    }

}