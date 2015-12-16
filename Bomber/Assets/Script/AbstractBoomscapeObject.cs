using UnityEngine;
using System.Collections;

public abstract class AbstractBoomscapeObject : MonoBehaviour {

	abstract protected void initObject();
	abstract protected void updateObject();
}
