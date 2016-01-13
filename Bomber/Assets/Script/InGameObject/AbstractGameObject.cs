using UnityEngine;
using System.Collections;

using Boomscape.InGameObject.Container.Map;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using Boomscape.Util;

namespace Boomscape.InGameObject
{
    public abstract class AbstractGameObject : AbstractBoomscapeObject
    {

        public IntegerPair positionIndexPair
        {
            get
            {
                return PositionCalcUtil.vector3ToMapIndex(transform.position);
            }

            set
            {
                transform.position = PositionCalcUtil.mapIndexToVector3(value);
            }
        }

        public abstract bool isObstacle { get; }
        public abstract GameMapLayer layer { get; }

        public abstract void onExplosion(AbstractBombValueObject bombData_);
        public abstract void destroyObject();
    }
}