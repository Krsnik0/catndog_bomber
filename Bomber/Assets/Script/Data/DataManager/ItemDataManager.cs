using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Boomscape.Data.ValueObject.Game.InGameObject.Item;
using Boomscape.Data.Constant;

namespace Boomscape.Data.DataManager
{
    class ItemDataManager : AbstractDataManager
    {
        static private ItemDataManager _instance;

        private bool _initFlag = false;
        private Dictionary<string, ItemValueObject> _hashmap;

        static public ItemDataManager getInstance()
        {
            if( _instance == null )
            {
                _instance = new ItemDataManager();
            }
            return _instance;
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

        public ItemValueObject findItem( string code_ )
        {
            ItemValueObject retValue;

            if (_hashmap.TryGetValue(code_, out retValue))
            {
                return retValue;
            }
            return null;
            //return _hashmap[code_];
        }

        private ItemDataManager()
        {
            _hashmap = new Dictionary<string, ItemValueObject>();
        }

        public override void loadData()
        {
            if( !_initFlag )
            {
                _initFlag = true;
                _hashmap.Add(ItemKindConst.SPEED_ITEM, new ItemValueObject(ItemKindConst.SPEED_ITEM, 3));
                _hashmap.Add(ItemKindConst.STAMINA_ITEM, new ItemValueObject(ItemKindConst.STAMINA_ITEM, 4));
                _hashmap.Add(ItemKindConst.STRENGTH_ITEM, new ItemValueObject(ItemKindConst.STRENGTH_ITEM, 5));
            }
        }
    }
}
