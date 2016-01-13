using UnityEngine;
using System.Collections;
using System;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.InGameObject.Container.Map;

namespace Boomscape.InGameObject.Tile
{
    public abstract class AbstractTile : AbstractGameObject
    {

        public override void onExplosion(AbstractBombValueObject bombData_)
        {
        }

        public override bool isObstacle
        {
            get
            {
                return false;
            }
        }

        public override GameMapLayer layer
        {
            get
            {
                return GameMapLayer.TILE;
            }
        }

        public override void destroyObject()
        {
            Destroy(gameObject);
        }
    }
}