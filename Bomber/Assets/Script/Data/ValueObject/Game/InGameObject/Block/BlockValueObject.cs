using UnityEngine;
using System.Collections.Generic;
using System;

namespace Boomscape.Data.ValueObject.Game.InGameObject.Block
{
    public class BlockValueObject : AbstractGameObjectValueObject
    {

        private string _blockCode;

        public BlockValueObject(string code_)
        {
            this._blockCode = code_;
        }

        private BlockValueObject(object rawData_)
        {

        }

        public override AbstractValueObject clone()
        {
            return new BlockValueObject(rawData);
        }

        public override string code
        {
            get
            {
                return _blockCode;
            }
        }

        public override KeyValuePair<Type, string> prefabData
        {
            get
            {
                return new KeyValuePair<Type, string>(typeof(GameObject), "Prefab/Block/" + code);
            }
        }
    }
}