using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ZoomView : MonoBehaviour
{
	public float zoomTime;
	private Camera camera;
	private float defaultFOV;
	private float targetFOV;


	// Use this for initialization
	void Start()
	{
		camera = GetComponent<Camera>();
		defaultFOV = camera.fieldOfView;
		targetFOV = defaultFOV;
	}

	// Update is called once per frame
	void Update()
	{
		if (!CrossPlatformInputManager.GetButton("Sprint"))
		{
			if (CrossPlatformInputManager.GetButtonDown("Zoom"))
			{
				targetFOV = defaultFOV / 2;
			}
			else if (CrossPlatformInputManager.GetButtonUp("Zoom"))
			{
				targetFOV = defaultFOV;
			}

			camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, targetFOV, zoomTime);
		}
	}
}
