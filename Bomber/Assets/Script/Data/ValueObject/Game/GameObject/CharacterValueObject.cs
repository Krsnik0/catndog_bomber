using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterValueObject : AbstractGameObjectValueObject {

	private string _characterCode;
	
	public CharacterValueObject( string code_
	                            )
	{
		this._characterCode = code_;
	}
	
	private CharacterValueObject( Object rawData_ )
	{
		
	}
	
	public override AbstractValueObject clone ()
	{
		return new CharacterValueObject (rawData);
	}
	
	public override string code {
		get {
			return _characterCode;
		}
	}

	public override KeyValuePair<System.Type, string> prefabData {
		get {
			return new KeyValuePair<System.Type, string>( typeof(GameObject), "Prefab/Character/" + code );
		}
	}
}
