// Using part of Frank Otto's AutoFocus script adapted for Unity's new PostProcessing Stack
// Original at: http://wiki.unity3d.com/index.php?title=DoFAutoFocus
// Adapted by Michael Hazani 
// For more info see: http://www.michaelhazani.com/autofocus-on-whats-important

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(Camera))]
public class ZoomView : MonoBehaviour
{
	public float zoomTime;
	private Camera cam;
	private float defaultFOV;
	private float targetFOV;

	private GameObject doFFocusTarget;
	private Vector3 lastDoFPoint;

	private PostProcessingProfile m_Profile;

	public DoFAFocusQuality focusQuality = ZoomView.DoFAFocusQuality.NORMAL;
	public LayerMask hitLayer = 1;
	public float maxDistance = 100.0f;
	public bool interpolateFocus = false;
	public float interpolationTime = 0.7f;

	public enum DoFAFocusQuality
	{
		NORMAL,
		HIGH
	}

	// Use this for initialization
	void Start()
	{
		cam = GetComponent<Camera>();
		defaultFOV = cam.fieldOfView;
		targetFOV = defaultFOV;

		doFFocusTarget = new GameObject("DoFFocusTarget");
		var behaviour = GetComponent<PostProcessingBehaviour>();
		m_Profile = behaviour.profile;
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

			cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetFOV, zoomTime);
		}

		// switch between Modes Test Focus every Frame
		if (focusQuality == ZoomView.DoFAFocusQuality.HIGH)
		{
			Focus();
		}
	}

	void FixedUpdate()
	{
		// switch between modes Test Focus like the Physicsupdate
		if (focusQuality == ZoomView.DoFAFocusQuality.NORMAL)
		{
			Focus();
		}
	}

	IEnumerator InterpolateFocus(Vector3 targetPosition)
	{

		Vector3 start = this.doFFocusTarget.transform.position;
		Vector3 end = targetPosition;
		float dTime = 0;

		// Debug.DrawLine(start, end, Color.green);
		var depthOfField = m_Profile.depthOfField.settings;
		while (dTime < 1)
		{
			yield return new WaitForEndOfFrame();
			dTime += Time.deltaTime / this.interpolationTime;
			this.doFFocusTarget.transform.position = Vector3.Lerp(start, end, dTime);
			depthOfField.focusDistance = Vector3.Distance(doFFocusTarget.transform.position, transform.position);
			m_Profile.depthOfField.settings = depthOfField;
		}
		this.doFFocusTarget.transform.position = end;
	}

	void Focus()
	{
		// our ray
		Ray ray = transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, this.maxDistance, this.hitLayer))
		{
			Debug.DrawLine(ray.origin, hit.point);

			// do we have a new point?					
			if (this.lastDoFPoint == hit.point)
			{
				return;
				// No, do nothing
			}
			else if (this.interpolateFocus)
			{ // Do we interpolate from last point to the new Focus Point ?
			  // stop the Coroutine
				StopCoroutine("InterpolateFocus");
				// start new Coroutine
				StartCoroutine(InterpolateFocus(hit.point));
			}
			else
			{
				this.doFFocusTarget.transform.position = hit.point;
				var depthOfField = m_Profile.depthOfField.settings;
				depthOfField.focusDistance = Vector3.Distance(doFFocusTarget.transform.position, transform.position);
				// print(depthOfField.focusDistance);
				m_Profile.depthOfField.settings = depthOfField;
			}
			// asign the last hit
			this.lastDoFPoint = hit.point;
		}
	}
}
