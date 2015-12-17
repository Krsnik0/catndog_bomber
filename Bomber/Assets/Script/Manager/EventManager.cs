using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	static private EventManager _instance;

	public delegate void EventListener( AbstractEvent event_ );

	private Dictionary< string, List<EventListener> > _listeners;

	private EventManager()
	{
		_listeners = new Dictionary<string, List<EventListener>> ();
	}

	static public EventManager getInstance()
	{
		if (_instance == null) {
			_instance = new EventManager();
		}

		return _instance;
	}

	public void addEventListener( string eventKey_, EventListener listener_ )
	{
		if (!_listeners.ContainsKey (eventKey_)) {
			_listeners.Add (eventKey_, new List<EventListener>());
		}
		_listeners[ eventKey_ ].Add( listener_ );
	}

	public void removeEventListener( string eventKey_, EventListener listener_ )
	{
		_listeners [eventKey_].Remove (listener_);
	}

	public void dispatchEvent( AbstractEvent event_ )
	{
        if (_listeners.ContainsKey(event_.eventKey))
        {
            for (int i = _listeners[event_.eventKey].Count - 1; i >= 0; --i)
            {
                _listeners[event_.eventKey][i](event_);
            }
        }
		
	}
}
