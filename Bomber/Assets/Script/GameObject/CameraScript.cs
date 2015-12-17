using UnityEngine;
using System.Collections;

public class CameraScript : AbstractBoomscapeObject {

	[Range( 1f, 10f )]
	public float DragDivConst;
	private bool _cameraInitFlag = false;

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

			EventManager.getInstance().addEventListener( InputEvent.INPUT_EVENT_KEY, onDragEvent );
		}
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
                Vector3 deltaCamPos = new Vector3(dragEvent.dragDirection.x, dragEvent.dragDirection.y) / DragDivConst;

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

	protected override void updateObject ()
	{

	}
}
