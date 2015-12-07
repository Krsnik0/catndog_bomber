using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectFactory {

	static private GameObjectFactory _instance;

	static public GameObjectFactory getInstance()
	{
		if (_instance == null) {
			_instance = new GameObjectFactory();
		}

		return _instance;
	}

	private GameObjectFactory()
	{
	}

	public GameObject generateObject( string prefabPath_ )
	{
		return MonoBehaviour.Instantiate ((GameObject)ResourceManager.getInstance().findResource( prefabPath_ ) );
	}

	public void dispose()
	{
		_instance = null;
		//Resources.UnloadAsset ();
	}
}