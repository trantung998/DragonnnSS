using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    private CameraRaycaster cameraRaycaster;
    [SerializeField] private Texture2D moveCursor = null;
    [SerializeField] private Texture2D atkCursor = null;
    [SerializeField] private Texture2D unkownCursor = null;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    private Layer currentlayer = Layer.RaycastEndStop;
    private Texture2D currentCursorTexture = null;

	private CompositeDisposable disposable;
	
    private void Awake()
    {
	    disposable =  new CompositeDisposable();
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }

	private void OnEnable()
	{
		MessageBroker.Default.Receive<OnLayerhitChanged>().Subscribe(OnLayerHitChanged).AddTo(disposable);
	}

	private void OnDisable()
	{
		disposable.Clear();
	}

	// Update is called once per frame
	void OnLayerHitChanged (OnLayerhitChanged @event)
	{
		print("On Layer Hit Changed");
	    var layerHit = @event.layer;
        
	    switch (layerHit)
	    {
	        case Layer.Enemy:
                currentCursorTexture = atkCursor;
	            break;
	        case Layer.Walkable:
                currentCursorTexture = moveCursor;
	            break;
	        case Layer.RaycastEndStop:
	            currentCursorTexture = unkownCursor;
	            break;
	        default:
	            currentCursorTexture = unkownCursor;
	            throw new ArgumentOutOfRangeException();
	    }

        Cursor.SetCursor(currentCursorTexture, hotspot, CursorMode.Auto);
	}
}
