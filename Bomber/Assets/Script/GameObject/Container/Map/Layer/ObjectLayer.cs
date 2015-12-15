using UnityEngine;
using System.Collections;

public class ObjectLayer : AbstractLayer {

	private PlayerCharacter _playerCharacter;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void loadLayer (AbstractGameObjectValueObject[][] layerData_)
	{
		base.loadLayer (layerData_);

		_playerCharacter = GetComponentInChildren<PlayerCharacter> ();
	}

	public PlayerCharacter playerCharacter
	{
		get {
			return _playerCharacter;
		}
	}
}
