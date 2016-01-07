using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Boomscape.Data.ValueObject.Game.InGameObject.Character
{
    public class CharacterValueObject : AbstractGameObjectValueObject
    {

        private string _characterCode;

        public CharacterValueObject(string code_)
        {
            this._characterCode = code_;
        }

        private CharacterValueObject(object rawData_)
        {

        }

        public override AbstractValueObject clone()
        {
            return new CharacterValueObject(rawData);
        }

        public override string code
        {
            get
            {
                return _characterCode;
            }
        }

        public override KeyValuePair<Type, string> prefabData
        {
            get
            {
                return new KeyValuePair<Type, string>(typeof(GameObject), "Prefab/Character/" + code);
            }
        }
    }
}