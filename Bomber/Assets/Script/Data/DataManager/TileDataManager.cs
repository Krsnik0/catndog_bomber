using Boomscape.Data.ValueObject.Game.InGameObject.Tile;
using System.Collections.Generic;

namespace Boomscape.Data.DataManager
{
    public class TileDataManager : AbstractDataManager
    {

        static private TileDataManager _instance;
        private bool _initFlag = false;
        private Dictionary<string, TileValueObject> _hashmap;

        private TileDataManager()
        {
            _hashmap = new Dictionary<string, TileValueObject>();
        }

        static public TileDataManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new TileDataManager();
            }

            return _instance;
        }

        public override void loadData()
        {
            if (!_initFlag)
            {
                _initFlag = true;

                _hashmap.Add("SYSTILE0000", new TileValueObject("SYSTILE0000"));
                _hashmap.Add("SYSTILE0001", new TileValueObject("SYSTILE0001"));
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

        public TileValueObject findTileData(string code_)
        {
            if (_hashmap.ContainsKey(code_))
            {
                return _hashmap[code_];
            }

            return null;
        }
    }
}