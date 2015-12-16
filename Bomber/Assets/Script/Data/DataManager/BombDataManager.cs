using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombDataManager : AbstractDataManager {

	static private BombDataManager _instance;
	
	private Dictionary<string, AbstractBombValueObject> _hashmap;
	
	static public BombDataManager getInstance()
	{
		if (_instance == null) {
			_instance = new BombDataManager ();
		}
		
		return _instance;
	}
	
	private BombDataManager()
	{
		_hashmap = new Dictionary<string, AbstractBombValueObject>();
	}
	
	public override void loadData ()
	{
		_hashmap.Add( "BOMB0000",
		             new BombType1("BOMB0000", 3, 3, 1, 5)
		);
		_hashmap.Add( "BOMB0001",
                     new BombType1("BOMB0001", 3, 3, 1, 5)
                     );
		_hashmap.Add( "BOMB0002",
                     new BombType1("BOMB0002", 3, 3, 1, 5)
                     );
		_hashmap.Add( "BOMB0003",
                     new BombType1("BOMB0003", 3, 3, 1, 5)
                     );
	}
	
	public AbstractBombValueObject findBombData( string code_ )
	{
		AbstractBombValueObject retValue;
		
		if (_hashmap.TryGetValue (code_, out retValue)) {
			return retValue;
		}
		return null;
	}
}
