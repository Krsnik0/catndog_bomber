using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomscape.Data.ValueObject.Game.InGameObject.Character;

namespace Boomscape.Data.DataManager
{
    public class CharacterDataManager : AbstractDataManager
    {

        static private CharacterDataManager _instance;

        private bool _initFlag = false;
        private Dictionary<string, CharacterValueObject> _hashmap;

        static public CharacterDataManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new CharacterDataManager();
            }

            return _instance;
        }

        private CharacterDataManager()
        {
            _hashmap = new Dictionary<string, CharacterValueObject>();
        }

        public override void loadData()
        {
            if (!_initFlag)
            {
                _initFlag = true;

                _hashmap.Add("CHAR0000", new CharacterValueObject("CHAR0000"));
                _hashmap.Add("CHAR0001", new CharacterValueObject("CHAR0001"));
            }
        }

        public CharacterValueObject findCharacterData(string code_)
        {
            CharacterValueObject retValue;

            if (_hashmap.TryGetValue(code_, out retValue))
            {
                return retValue;
            }
            return null;
        }
    }
}