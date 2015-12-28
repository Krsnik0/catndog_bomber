using UnityEngine;
using System.Collections;

public class MarkerBlock : AbstractBlock {

    public enum MarkerType { NONE, THROW_RANGE, EXPLOSION_RANGE, BOMB_POSITION };

    private bool _markerBlockInitFlag = false;
    public MarkerType markerType { get; set; }

	public bool isTouchable { get; set; }
    // Use this for initialization
    void Start () {
		initObject ();
	}
	
	// Update is called once per frame
	void Update () {
		updateObject ();
	}

	public override GameMap.GameMapLayer layer {
		get {
			return GameMap.GameMapLayer.MARKER;
		}
	}

	protected override void initObject ()
	{
		if (!_markerBlockInitFlag) {
			_markerBlockInitFlag = true;

			EventManager.getInstance().addEventListener( GameStateEvent.STATE_EVENT_KEY, onStateChanged );
		}
	}

	protected override void updateObject ()
	{
	}

	private void onStateChanged( AbstractEvent event_ )
	{
		GameStateEvent stateEvent = event_ as GameStateEvent;

		if (stateEvent.prevState == GameManager.GameState.THROWING) {
			destroyObject();
		}
	}
	
	public override void destroyObject ()
	{
		EventManager.getInstance().removeEventListener( GameStateEvent.STATE_EVENT_KEY, onStateChanged );
		EventManager.getInstance().dispatchEvent( new ObjectRemovedEvent( this ) );
		Destroy (gameObject);
	}
}

