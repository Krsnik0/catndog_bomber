using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombLister : AbstractUI {

	private bool _bombListerInitFlag = false;

	private BombCursor _cursor;
	private BombValueObject _selectedBomb;

	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	protected override void initObject ()
	{
		base.initObject ();
		if (!_bombListerInitFlag) {
			_bombListerInitFlag = true;
			_cursor = GetComponentInChildren<BombCursor> ();
			_cursor.setVisibility( false );
		}
	}

	public override void onStateEnd (GameManager.GameState gameState_)
	{
		base.onStateEnd (gameState_);

		switch (gameState_) {
		case GameManager.GameState.THROWING:
			_cursor.setVisibility( false );
			break;
		}
	}

	public void loadAllowedBombsData( BombValueObject[] allowedBombs_ )
	{
		transform.Find ("BombListerBackground").localScale = new Vector3( allowedBombs_.Length, 1f, 1f );

		GameObject icon;
		RectTransform iconRectTransform;

		for (int i = 0; i < allowedBombs_.Length; ++ i) {
			icon = GameObjectFactory.getInstance().generateObject( allowedBombs_[i].iconPath.Value );
			icon.GetComponent<BombIconUI>().bombData = allowedBombs_[i];
			icon.transform.SetParent( transform );
			iconRectTransform = (RectTransform)icon.transform;
			iconRectTransform.localPosition = new Vector3( 100f * i + 50f, 50f, 0 );
		}

		updateChildrenList ();
	}

	protected override void updateObject ()
	{
	}

	public override void onChildClick (AbstractUI child_)
	{
		GameManager.getInstance ().changeState (GameManager.GameState.THROWING);

		_cursor.setVisibility (true);
		_cursor.rTransform.localPosition = child_.rTransform.localPosition;
		_selectedBomb = child_.GetComponent<BombIconUI> ().bombData;

		base.onChildClick (child_);
	}

	public void cancelBombSelection()
	{
		_selectedBomb = null;
	}

	public BombValueObject selectedBomb
	{
		get {
			return _selectedBomb;
		}
	}
}
