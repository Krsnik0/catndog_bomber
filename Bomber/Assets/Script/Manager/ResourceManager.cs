using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager {

	public delegate void ResourceLoadCallback ();

	static private ResourceManager _instance;

	private Dictionary<string, Object> _resourceHashmap;

	private ResourceManager()
	{
		_resourceHashmap = new Dictionary<string, Object> ();
	}

	static public ResourceManager getInstance()
	{
		if (_instance == null) {
			_instance = new ResourceManager();
		}

		return _instance;
	}

	public void dispose()
	{
		_resourceHashmap.Clear ();
		_resourceHashmap = null;
	}

	public Object findResource( string resourcePath_ )
	{
		Object retValue;
		if (_resourceHashmap.TryGetValue ( resourcePath_, out retValue)) {
			return retValue;
		}
		return null;
	}

	public IEnumerator LoadResourcesByCoroutine( List<KeyValuePair<System.Type, string>> resourcePaths_, ResourceLoadCallback resourceLoadCallback_ )
	{
		ResourceRequest req;


		for( int i = 0; i < resourcePaths_.Count; ++ i )
		{
			Debug.Log ("Loading resources...[" + i.ToString() + "/" + resourcePaths_.Count.ToString() + "] : " + resourcePaths_[i].Value );
			req = Resources.LoadAsync( resourcePaths_[i].Value, resourcePaths_[i].Key );
			yield return req;

			_resourceHashmap.Add( resourcePaths_[i].Value, req.asset );
		}

		Debug.Log ("Resource loading done");

		resourceLoadCallback_ ();
	}
}
