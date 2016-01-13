using UnityEngine;
using System.Collections;

using Boomscape;
using Boomscape.InGameObject.Container.Map;
using System;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Data.ValueObject.Game.InGameObject.Item;

namespace Boomscape.InGameObject.Item
{
    public class InGameItem : AbstractGameObject
    {

        public ItemValueObject itemData
        {
            get; set;
        }
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
                return GameMapLayer.OBJECT;
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
            Destroy(this.gameObject);
        }

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
        }

        protected override void initObject()
        {

        }

        protected override void updateObject()
        {

        }
    }
}