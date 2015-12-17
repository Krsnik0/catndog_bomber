using UnityEngine;
using System.Collections;

public class UIDataValueObject : AbstractValueObject {

	public UIDataValueObject()
	{
	}

	public override AbstractValueObject clone ()
	{
		UIDataValueObject retValue = new UIDataValueObject ();
		retValue.selectedBomb = selectedBomb;
		return retValue;
	}

	public AbstractBombValueObject selectedBomb {
		get;
		set;
	}
}
