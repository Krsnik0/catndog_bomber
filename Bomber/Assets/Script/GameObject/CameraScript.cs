using UnityEngine;
using System.Collections;

public class CameraScript : AbstractBoomscapeObject {

	[Range( 0f, 1f )]
	public float DragMultiplier;
	private bool _cameraInitFlag = false;
    private Vector2? _targetPos;
    private Vector2 _camVelocity;

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
		if (!_cameraInitFlag) {
			_cameraInitFlag = true;

            _targetPos = null;
			EventManager.getInstance().addEventListener( InputEvent.INPUT_EVENT_KEY, onDragEvent );
		}
	}

    protected override void updateObject()
    {
        if (_targetPos != null)
        {
            Vector2 target = (Vector2)_targetPos;
            if (Vector2.Distance((Vector2)transform.position, target) < _camVelocity.magnitude * 0.01f)
            {
                for (int i = 0; i < Camera.allCamerasCount; ++i)
                {
                    Camera.allCameras[i].transform.position = new Vector3(target.x, target.y, transform.position.z);
                }
                _targetPos = null;
            }
            else
            {
                for (int i = 0; i < Camera.allCamerasCount; ++i)
                {
                    Camera.allCameras[i].transform.position += (Vector3)_camVelocity * Time.deltaTime;
                }
            }
        }
    }

    public void moveCamTo(Vector2 targetPos_, float time_)
    {
        _targetPos = targetPos_;
        _camVelocity = ((Vector2)targetPos_ - (Vector2)transform.position) / time_;
    }

	private void onDragEvent( AbstractEvent event_ )
	{
		InputEvent inputEvent = event_ as InputEvent;

        if (inputEvent.inputType == InputEvent.InputType.DRAG)
        {
            DragInputEvent dragEvent = inputEvent as DragInputEvent;

            if( !(dragEvent.target is MarkerBlock) ||
                (dragEvent.target as MarkerBlock).markerType != MarkerBlock.MarkerType.BOMB_POSITION)
            {
                Vector3 deltaCamPos = new Vector3(dragEvent.dragDirection.x, dragEvent.dragDirection.y) * DragMultiplier;

                for (int i = 0; i < Camera.allCamerasCount; ++i)
                {
                    Camera.allCameras[i].transform.position -= deltaCamPos;
                }

                Rect mapColliderRect = new Rect(new Vector2(), GameMap.getInstance().boxCollider.size);
                if (!mapColliderRect.Contains(transform.position))
                {
                    for (int i = 0; i < Camera.allCamerasCount; ++i)
                    {
                        Camera.allCameras[i].transform.position += deltaCamPos;
                    }
                }
            }
        }
	}
}
