using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomscape.Data.ValueObject.Game.InGameObject.Block;
using System;

namespace Boomscape.Data.DataManager
{
    public class BlockDataManager : AbstractDataManager
    {

        static private BlockDataManager _instance;
        private bool _initFlag = false;
        private Dictionary<string, BlockValueObject> _hashmap;

        static public BlockDataManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new BlockDataManager();
            }

            return _instance;
        }

        private BlockDataManager()
        {
            _hashmap = new Dictionary<string, BlockValueObject>();
        }

        public override void loadData()
        {
            if (!_initFlag)
            {
                _initFlag = true;

                _hashmap.Add("SYSBLOCK0000", new BlockValueObject("SYSBLOCK0000"));
                _hashmap.Add("SYSBLOCK0001", new BlockValueObject("SYSBLOCK0001"));
                _hashmap.Add("BLOCK0000", new BlockValueObject("BLOCK0000"));
                _hashmap.Add("BLOCK0001", new BlockValueObject("BLOCK0001"));
                _hashmap.Add("BLOCK0002", new BlockValueObject("BLOCK0002"));
            }

        }

        public override void dispose()
        {
            string[] keys = new string[_hashmap.Count];
            _hashmap.Keys.CopyTo(keys, 0);
            for( int i = 0; i < keys.Length; ++ i )
            {
                _hashmap.Remove(keys[i]);
            }

            _hashmap = null;
            _instance = null;
        }

        public BlockValueObject[] allBlocks
        {
            get
            {
                BlockValueObject[] ret = new BlockValueObject[_hashmap.Values.Count];
                _hashmap.Values.CopyTo(ret, 0);

                return ret;
            }
        }

        public BlockValueObject findBlockData(string code_)
        {
            BlockValueObject retValue;

            if (_hashmap.TryGetValue(code_, out retValue))
            {
                return retValue;
            }
            return null;
        }
    }
}