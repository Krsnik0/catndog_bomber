using UnityEngine;
using System.Collections;

public abstract class AbstractBoomscapeObject : MonoBehaviour {

	abstract protected void initObject();
	abstract protected void updateObject();

	abstract public void onStateEnd( GameManager.GameState gameState_ );
	abstract public void onStateStart( GameManager.GameState gameState_ );
	
	abstract public bool triggerInput( InputEvent input_ );
	//abstract public bool onChildClick( AbstractBoomscapeObject child_ );
}
