using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomscape.Data.ValueObject.Game.InGameObject.Bomb;
using System;

namespace Boomscape.Data.DataManager
{
    public class BombDataManager : AbstractDataManager
    {

        static private BombDataManager _instance;
        private bool _initFlag = false;

        private Dictionary<string, AbstractBombValueObject> _hashmap;

        static public BombDataManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new BombDataManager();
            }

            return _instance;
        }

        private BombDataManager()
        {
            _hashmap = new Dictionary<string, AbstractBombValueObject>();
        }

        public override void loadData()
        {
            if (!_initFlag)
            {
                _initFlag = true;
                _hashmap.Add("BOMB0000",
                         new BombType1("BOMB0000", 3, 3, 1, 5)
                            );
                _hashmap.Add("BOMB0001",
                             new BombType1("BOMB0001", 3, 3, 1, 5)
                             );
                _hashmap.Add("BOMB0002",
                             new BombType1("BOMB0002", 3, 3, 1, 5)
                             );
                _hashmap.Add("BOMB0003",
                             new BombType1("BOMB0003", 3, 3, 1, 5)
                             );
            }
        }

        public override void dispose()
        {
            string[] keys = new string[_hashmap.Count];
            _hashmap.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; ++i)
            {
                _hashmap.Remove(keys[i]);
            }

            _hashmap = null;
            _instance = null;
        }

        public AbstractBombValueObject findBombData(string code_)
        {
            AbstractBombValueObject retValue;

            if (_hashmap.TryGetValue(code_, out retValue))
            {
                return retValue;
            }
            return null;
        }

        public AbstractBombValueObject[] allBombs
        {
            get
            {
                AbstractBombValueObject[] ret = new AbstractBombValueObject[_hashmap.Values.Count];
                _hashmap.Values.CopyTo(ret, 0);

                return ret;
            }
        }
    }
}
