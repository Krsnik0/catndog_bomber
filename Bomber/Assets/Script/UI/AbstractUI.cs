using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractUI : AbstractBoomscapeObject {

	private bool _uiInitFlag = false;

	private AbstractUI _parentUI;
	private List<AbstractUI> _childrenUI;

	private Vector3 _prevScale;

	public RectTransform rTransform
	{
		get
		{
			return (RectTransform)transform;
		}
	}

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	public AbstractUI parentUI {
		get {
			return _parentUI;
		}
	}

	protected override void initObject ()
	{
		if (!_uiInitFlag) {
			_parentUI = transform.parent.GetComponent<AbstractUI> ();

			updateChildrenList();
		}
	}

	public void updateChildrenList()
	{
		_childrenUI = new List<AbstractUI>();
		AbstractUI childUI;
		for( int i = 0; i < transform.childCount; ++ i )
		{
			childUI = transform.GetChild (i).GetComponent<AbstractUI> ();

			if( childUI != null )
			{
				_childrenUI.Add( childUI );
			}
		}
	}

	public override void onStateStart (GameManager.GameState gameState_)
	{
		if (_childrenUI != null) {
			for (int i = _childrenUI.Count - 1; i >= 0; -- i) {
				_childrenUI [i].onStateEnd (gameState_);
			}
		}
	}

	public override void onStateEnd (GameManager.GameState gameState_)
	{
		if (_childrenUI != null) {
			for (int i = _childrenUI.Count - 1; i >= 0; -- i) {
				_childrenUI [i].onStateEnd (gameState_);
			}
		}
	}

	public void setVisibility( bool value_ )
	{
		gameObject.SetActive (value_);
	}

	public virtual void onClick ()
	{
		if (parentUI != null) {
			parentUI.onChildClick( this );
		}
	}

	public virtual void onChildClick ( AbstractUI child_ )
	{
		if (parentUI != null) {
			parentUI.onChildClick( child_ );
		}
	}
}
