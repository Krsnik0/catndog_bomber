using UnityEngine;
using System.Collections;

public abstract class AbstractValueObject {

	protected Object rawData { get; set; }

	public abstract AbstractValueObject clone ();
}
