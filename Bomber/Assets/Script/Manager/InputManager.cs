using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Boomscape.Data.Event.Input;
using Boomscape.Util;

namespace Boomscape.Manager
{
    public class InputManager
    {

        static private InputManager _instance;

        private const float DRAG_THRESHOLD = 0.05f;

        private bool _isDragging;
        private Vector2 _dragOrigin;
        private Vector2 _prevMousePos;
        private AbstractBoomscapeObject _objAtDragOrigin;

        private InputManager()
        {
            _isDragging = false;
        }

        static public InputManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new InputManager();
            }

            return _instance;
        }

        public void triggerInput()
        {
            Vector2 currentInputPos = new Vector2();
            bool isInputDown;
            bool isInputUp;
            bool isInput;

            int touchID = 0;

#if UNITY_EDITOR || UNITY_WEBPLAYER
            currentInputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isInputDown = Input.GetMouseButtonDown(0);
            isInput = Input.GetMouseButton(0);
            isInputUp = Input.GetMouseButtonUp(0);

            touchID = -1;       // mouse touch id
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            currentInputPos = Camera.main.ScreenToWorldPoint(t.position);
            isInputDown = t.phase == TouchPhase.Began;
            isInput = (t.phase != TouchPhase.Canceled && t.phase != TouchPhase.Ended);
            isInputUp = (t.phase == TouchPhase.Ended);

            touchID = t.fingerId;
        }
        else
        {
            isInputDown = false;
            isInput = false;
            isInputUp = false;
        }
#endif

            int clickableLayer = LayerUtil.clickableLayer;

            if (isInputDown)
            {
                _dragOrigin = currentInputPos;
                RaycastHit2D hit = Physics2D.Raycast(currentInputPos, Vector2.zero, 0f, clickableLayer);
                if (hit.collider != null)
                {
                    _objAtDragOrigin = hit.collider.gameObject.GetComponentInParent<AbstractBoomscapeObject>();
                }
            }
            else if (isInput)
            {
                if (!_isDragging)
                {
                    if (Vector2.Distance(_dragOrigin, currentInputPos) >= DRAG_THRESHOLD)
                    {
                        _isDragging = true;
                        Debug.Log("start dragging : " + _objAtDragOrigin);

                        if (_objAtDragOrigin != null)
                        {
                            DragInputEvent drag = new DragInputEvent(_objAtDragOrigin, DragInputEvent.DragState.STARTED, _dragOrigin, currentInputPos - _dragOrigin);
                            EventManager.getInstance().dispatchEvent(drag);
                        }
                        else
                        {
                            DragInputEvent drag = new DragInputEvent(_dragOrigin, DragInputEvent.DragState.STARTED, _dragOrigin, currentInputPos - _dragOrigin);
                            EventManager.getInstance().dispatchEvent(drag);
                        }
                    }
                }
                else
                {
                    if (_objAtDragOrigin != null)
                    {
                        DragInputEvent drag = new DragInputEvent(_objAtDragOrigin, DragInputEvent.DragState.DRAGGING, _prevMousePos, currentInputPos - _prevMousePos);
                        EventManager.getInstance().dispatchEvent(drag);
                    }
                    else
                    {
                        DragInputEvent drag = new DragInputEvent(_dragOrigin, DragInputEvent.DragState.DRAGGING, _prevMousePos, currentInputPos - _prevMousePos);
                        EventManager.getInstance().dispatchEvent(drag);
                    }
                }
            }
            else if (isInputUp)
            {

                if (!_isDragging)
                {
                    RaycastHit2D hit = Physics2D.Raycast(currentInputPos, Vector2.zero, 0f, clickableLayer);

                    if (hit.collider != null)
                    {
                        AbstractBoomscapeObject gameObj = hit.collider.gameObject.GetComponentInParent<AbstractBoomscapeObject>();
                        TouchInputEvent touch = new TouchInputEvent(gameObj);
                        EventManager.getInstance().dispatchEvent(touch);
                    }
                    else
                    {
                        TouchInputEvent touch = new TouchInputEvent(currentInputPos);
                        EventManager.getInstance().dispatchEvent(touch);
                    }
                }
                else
                {

                    if (_objAtDragOrigin != null)
                    {
                        DragInputEvent drag = new DragInputEvent(_objAtDragOrigin, DragInputEvent.DragState.ENDED, _prevMousePos, currentInputPos - _prevMousePos);
                        EventManager.getInstance().dispatchEvent(drag);
                    }
                    else
                    {
                        DragInputEvent drag = new DragInputEvent(_dragOrigin, DragInputEvent.DragState.ENDED, _prevMousePos, currentInputPos - _prevMousePos);
                        EventManager.getInstance().dispatchEvent(drag);
                    }

                    Debug.Log("end dragging");
                    _isDragging = false;
                    _objAtDragOrigin = null;
                }
            }

            _prevMousePos = currentInputPos;
        }
    }
}