using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDataManager : AbstractDataManager {

	static private CharacterDataManager _instance;
	
	private Dictionary<string, CharacterValueObject> _hashmap;
	
	static public CharacterDataManager getInstance()
	{
		if (_instance == null) {
			_instance = new CharacterDataManager ();
		}
		
		return _instance;
	}
	
	private CharacterDataManager()
	{
		_hashmap = new Dictionary<string, CharacterValueObject>();
	}
	
	public override void loadData ()
	{
        _hashmap.Add("CHAR0000", new CharacterValueObject("CHAR0000"));
        _hashmap.Add("CHAR0001", new CharacterValueObject("CHAR0001"));
    }
	
	public CharacterValueObject findCharacterData( string code_ )
	{
		CharacterValueObject retValue;
		
		if (_hashmap.TryGetValue (code_, out retValue)) {
			return retValue;
		}
		return null;
	}
}
