using UnityEngine;
using System.Collections;

using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;

namespace Boomscape.Data.ValueObject.System.UI
{
    public class UIDataValueObject : AbstractValueObject
    {

        public UIDataValueObject()
        {
        }

        public override AbstractValueObject clone()
        {
            UIDataValueObject retValue = new UIDataValueObject();
            retValue.selectedBomb = selectedBomb;
            return retValue;
        }

        public AbstractBombValueObject selectedBomb
        {
            get;
            set;
        }
    }
}