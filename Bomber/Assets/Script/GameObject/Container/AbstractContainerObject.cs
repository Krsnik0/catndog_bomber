using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractContainerObject : AbstractBoomscapeObject {

	private bool _containerInitFlag = false;
	
	private List<AbstractBoomscapeObject> _containingObjects;

	private void Start()
	{
		initObject ();
	}

	private void Update()
	{
		updateObject ();
	}

	protected override void updateObject ()
	{

	}

	protected override void initObject ()
	{
		if (!_containerInitFlag) {
			_containingObjects = new List<AbstractBoomscapeObject>();
		}
	}

	/*public override bool triggerInput (InputEvent input_)
	{
		bool retValue = false;
		for (int i = _containingObjects.Count - 1; i >= 0; -- i) {
			if( _containingObjects[i].triggerInput( input_ ) )
			{
				retValue = true;
			}
		}

		return retValue;
	}*/
	
	public override void onStateEnd (GameManager.GameState gameState_)
	{
		for (int i = _containingObjects.Count - 1; i >= 0; -- i) {
			_containingObjects[i].onStateEnd( gameState_ );
		}
	}
	
	public override void onStateStart (GameManager.GameState gameState_)
	{
		for (int i = _containingObjects.Count - 1; i >= 0; -- i) {
			_containingObjects[i].onStateStart( gameState_ );
		}
	}

	virtual public AbstractBoomscapeObject addObject( AbstractBoomscapeObject obj_ )
	{
		obj_.gameObject.transform.parent = transform;
		_containingObjects.Add (obj_);
		return obj_;
	}

	virtual public AbstractBoomscapeObject removeObject( AbstractBoomscapeObject obj_ )
	{
		_containingObjects.Remove (obj_);
		return obj_;
	}

	virtual public AbstractGameObject addObject( AbstractGameObject obj_ )
	{
		obj_.gameObject.transform.parent = transform;
		_containingObjects.Add (obj_);
		return obj_;
	}
	
	virtual public AbstractGameObject removeObject( AbstractGameObject obj_ )
	{
		_containingObjects.Remove (obj_);
		return obj_;
	}

	virtual public void removeAll( bool destroyFlag )
	{
		AbstractGameObject obj;
		for (int i = _containingObjects.Count - 1; i >= 0; -- i) {
			obj = (AbstractGameObject)removeObject( _containingObjects[i] );
			if( destroyFlag && obj != null )
			{
				obj.destroyObject();
			}
		}
	}

	abstract public void moveObject (AbstractGameObject obj_, IntegerPair dst_);

	virtual public bool contains( AbstractBoomscapeObject obj_ )
	{
		return _containingObjects.Contains( obj_ );
	}

	virtual public bool contains( AbstractGameObject obj_ )
	{
		return _containingObjects.Contains (obj_);
	}
}
