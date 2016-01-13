using System;
using Boomscape.InGameObject.Bomb;

namespace Boomscape.Data.Event.GameObject
{
    class BombPlacedEvent : AbstractEvent
    {
        public const string BOMB_PLACED_EVENT_KEY = "bombPlaced";

        public BombPlacedEvent(AbstractBomb target_) : base(target_)
        {
        }

        public AbstractBomb targetGameObject
        {
            get
            {
                if (target != null)
                {
                    return target as AbstractBomb;
                }
                else
                {
                    return null;
                }
            }
        }

        public override string eventKey
        {
            get
            {
                return BOMB_PLACED_EVENT_KEY;
            }
        }
    }
}
