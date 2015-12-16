using UnityEngine;
using System.Collections;

public class RootUI : AbstractUI {

	static private RootUI _instance;

	private bool _rootUIInitFlag = false;

	private BombLister _bombLister;
	private UIDataValueObject _uiData;
	// Use this for initialization
	void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	static public RootUI getInstance()
	{
		return _instance;
	}

	protected override void initObject ()
	{
		base.initObject ();

		if (!_rootUIInitFlag) {
			_instance = this;

			_uiData = new UIDataValueObject();
			_rootUIInitFlag = true;
			_bombLister = GetComponentInChildren<BombLister>();
		}
	}

	protected override void updateObject ()
	{

	}

	public void loadStageUI( StageValueObject stageData_ )
	{
		_bombLister.loadAllowedBombsData (stageData_.allowedBombs);
	}

	public UIDataValueObject uiData {
		get{
			return _uiData;
		}
	}

	public override void onClick ()
	{

	}

	private void updateUIData()
	{
		_uiData.selectedBomb = _bombLister.selectedBomb;
	}

	public override void onChildClick (AbstractUI child_)
	{
		updateUIData ();
		EventManager.getInstance().dispatchEvent( new UpdateRequestEvent( typeof( GameMap ) ) );
	}
}
