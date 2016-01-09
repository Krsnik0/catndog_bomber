using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boomscape.Data.ValueObject.Game.InGameObject.Item
{
    public class ItemValueObject : AbstractGameObjectValueObject
    {
        private string _itemCode;
        private float _effectTime;

        public ItemValueObject( string code_, float effectTime_ )
        {
            _itemCode = code_;
            _effectTime = effectTime_;
        }

        public override AbstractValueObject clone()
        {
            return new ItemValueObject(code, effectTime);
        }

        public override KeyValuePair<Type, String> prefabData
        {
            get
            {
                return new KeyValuePair<Type, string>(typeof(GameObject), "Prefab/Item/" + code);
            }
        }


        public override string code
        {
            get
            {
                return _itemCode;
            }
        }

        public float effectTime
        {
            get
            {
                return _effectTime;
            }
        }
    }
}
