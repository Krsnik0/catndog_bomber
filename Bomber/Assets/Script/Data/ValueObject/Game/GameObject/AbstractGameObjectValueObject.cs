using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractGameObjectValueObject : AbstractValueObject {

	abstract public string code { get; }
	abstract public KeyValuePair<System.Type, string> prefabData { get; }
}
