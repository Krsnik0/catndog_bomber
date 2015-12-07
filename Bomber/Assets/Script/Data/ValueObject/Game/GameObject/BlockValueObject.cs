using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockValueObject : AbstractGameObjectValueObject {

	private string _blockCode;

	public BlockValueObject( string code_ )
	{
		this._blockCode = code_;
	}

	private BlockValueObject( Object rawData_ )
	{

	}

	public override AbstractValueObject clone ()
	{
		return new BlockValueObject (rawData);
	}

	public override string code {
		get {
			return _blockCode;
		}
	}

	public override KeyValuePair<System.Type, string> prefabData {
		get {
			return new KeyValuePair<System.Type, string>( typeof(GameObject), "Prefab/Block/" + code );
		}
	}
}
