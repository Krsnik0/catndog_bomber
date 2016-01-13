using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Boomscape.Data.ValueObject.Game.InGameObject.Tile
{
    public class TileValueObject : AbstractGameObjectValueObject
    {

        private string _tileCode;

        public TileValueObject()
        {
        }

        public TileValueObject(string code_)
        {
            _tileCode = code_;
        }


        public override AbstractValueObject clone()
        {
            return new TileValueObject();
        }

        public override string code
        {
            get
            {
                return _tileCode;
            }
        }

        public override KeyValuePair<Type, string> prefabData
        {
            get
            {
                return new KeyValuePair<Type, string>(typeof(GameObject), "Prefab/Tile/" + code);
            }
        }
    }
}