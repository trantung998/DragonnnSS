using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCursor : MonoBehaviour
{
    private CameraRaycaster cameraRaycaster;

    private void Awake()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }

    // Update is called once per frame
	void Update () {
		print(cameraRaycaster.layerHit);
	}
}
