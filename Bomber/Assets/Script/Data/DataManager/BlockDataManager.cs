using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockDataManager : AbstractDataManager {

	static private BlockDataManager _instance;
    private bool _initFlag = false;
	private Dictionary<string, BlockValueObject> _hashmap;

	static public BlockDataManager getInstance()
	{
		if (_instance == null) {
			_instance = new BlockDataManager ();
		}

		return _instance;
	}

	private BlockDataManager()
	{
		_hashmap = new Dictionary<string, BlockValueObject>();
	}

	public override void loadData ()
	{
        if (!_initFlag)
        {
            _initFlag = true;

            _hashmap.Add("SYSBLOCK0000", new BlockValueObject("SYSBLOCK0000"));
            _hashmap.Add("SYSBLOCK0001", new BlockValueObject("SYSBLOCK0001"));
            _hashmap.Add("BLOCK0000", new BlockValueObject("BLOCK0000"));
            _hashmap.Add("BLOCK0001", new BlockValueObject("BLOCK0001"));
        }
		
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

    public BlockValueObject findBlockData( string code_ )
	{
		BlockValueObject retValue;

		if (_hashmap.TryGetValue (code_, out retValue)) {
			return retValue;
		}
		return null;
	}
}
