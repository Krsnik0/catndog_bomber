using UnityEngine;
using System.Collections;

public class BombIconUI : AbstractUI {

	private BombValueObject _bombData;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	protected override void updateObject ()
	{

	}

	public override void onChildClick (AbstractUI child_)
	{
		base.onChildClick (child_);
	}

	public BombValueObject bombData
	{
		get {
			return _bombData;
		}

		set {
			_bombData = value;
		}
	}
}
