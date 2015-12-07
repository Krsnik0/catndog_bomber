using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombDataManager : AbstractDataManager {

	static private BombDataManager _instance;
	
	private Dictionary<string, BombValueObject> _hashmap;
	
	static public BombDataManager getInstance()
	{
		if (_instance == null) {
			_instance = new BombDataManager ();
		}
		
		return _instance;
	}
	
	private BombDataManager()
	{
		_hashmap = new Dictionary<string, BombValueObject>();
	}
	
	public override void loadData ()
	{
		_hashmap.Add( "BOMB0000",
		             new BombValueObject(
						"BOMB0000",
						new bool[][] {
							new bool[] { true, true, true },
							new bool[] { true, false, true },
							new bool[] { true, true, true }
						},
						new IntegerPair( 1, 1 ),
						3,
						3)
		);
		_hashmap.Add( "BOMB0001",
		             new BombValueObject(
						"BOMB0001",
						new bool[][] {
							new bool[] { false, true, false },
							new bool[] { true, false, true },
							new bool[] { false, true, false }
						},
						new IntegerPair( 1, 1 ),
						4,
						4)
		             );
		_hashmap.Add( "BOMB0002",
		             new BombValueObject(
						"BOMB0002",
						new bool[][] {
							new bool[] { true, false, true },
							new bool[] { false, false, false },
							new bool[] { true, false, true }
						},
						new IntegerPair( 1, 1 ),
						5,
						5)
		             );
		_hashmap.Add( "BOMB0003",
		             new BombValueObject(
						"BOMB0003",
						new bool[][] {
							new bool[] { true, false, true },
							new bool[] { true, false, true },
							new bool[] { true, false, true }
						},
						new IntegerPair( 1, 1 ),
						2,
						2)
		             );
	}
	
	public BombValueObject findBombData( string code_ )
	{
		BombValueObject retValue;
		
		if (_hashmap.TryGetValue (code_, out retValue)) {
			return retValue;
		}
		return null;
	}
}
