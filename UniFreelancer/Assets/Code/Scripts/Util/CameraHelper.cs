using UnityEngine;
using System.Collections;

public class CameraHelper : MonoBehaviour
{
	void Start()
    {
        this.camera.orthographicSize = Screen.height / 2;
	}

	void Update()
    {
	}
}
