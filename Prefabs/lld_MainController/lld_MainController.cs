using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class lld_MainController : MonoBehaviour
{
	public Transform world;
	public Transform player;
	public float tiltRatio = 0.04f;
	public float cameraMaxLen = 20f;
	/**************************/
	private float zoomval = 0.3f;
	private float gravityAlpha = 0f;
	private Vector3 currentup;
	private Vector3 currentlook;
	private Vector3 currentlookoffset;
	/************************/
	private Vector3 accFilter = Vector3.zero;
	/************************/
	private bool onefinger = false;
	private float localEulerX;
	private float localEulerY;
	private Transform junction;
	private Vector2 startOneTouch;
	private float startLocalEulerX;
	private float startLocalEulerY;
	/***************************/
	private bool twofingers = false;
	private float lastZoomval;
	private float lastZTwoTouchDist;
	private float startGravityDelta;

	void Awake ()
	{
		Input.gyro.enabled = true;
		Input.gyro.updateInterval = 1f / 30f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		currentup = world.forward;
		currentlook = world.position;
		currentlookoffset = cameraMaxLen * world.up;
		junction = transform.FindChild ("Junction");
	}

	void FixedUpdate ()
	{
		Vector3 desireUp = world.forward * Mathf.Cos (gravityAlpha) + world.right * Mathf.Sin (gravityAlpha);
		if (!desireUp.Equals (currentup)) {
			currentup = Vector3.RotateTowards (currentup, desireUp, 0.07f, 0f);
		}
		Vector3 desirelook = world.position + zoomval * (player.position - world.position);
		if (!desirelook.Equals (currentlook)) {
			currentlook += Vector3.ClampMagnitude (desirelook - currentlook, 0.5f);
		}
		Vector3 desirelookoffset = (1.01f - zoomval) * cameraMaxLen * world.up;
		if (!desirelookoffset.Equals (currentlookoffset)) {
			currentlookoffset = Vector3.RotateTowards (currentlookoffset, desirelookoffset, 0.05f, 0.35f);
		}
	}

	void LateUpdate ()
	{
		switch (Input.touchCount) {
		case 1:
			Vector2 oneTouch = Input.GetTouch (0).position;
			if (onefinger) {
				localEulerX = startLocalEulerX + 0.2f * (startOneTouch.y - oneTouch.y);
				localEulerY = startLocalEulerY - 0.2f * (startOneTouch.x - oneTouch.x);
				localEulerX = Mathf.Max (-60f, Mathf.Min (60f, localEulerX));
				localEulerY = Mathf.Max (-60f, Mathf.Min (60f, localEulerY));
			} else {
				onefinger = true;
				startOneTouch = oneTouch;
				startLocalEulerX = junction.localEulerAngles.x;
				startLocalEulerY = junction.localEulerAngles.y;
				startLocalEulerX = (startLocalEulerX < 180f) ? startLocalEulerX : (startLocalEulerX - 360f);
				startLocalEulerY = (startLocalEulerY < 180f) ? startLocalEulerY : (startLocalEulerY - 360f);
			}
			twofingers = false;
			break;
		case 2:
			Vector2 twoTouchs = Input.GetTouch (0).position - Input.GetTouch (1).position;
			float twoTouchDist = twoTouchs.magnitude;
			if (twofingers) {
				float cache = (twoTouchDist - lastZTwoTouchDist) * 0.001f;
				cache = (cache > 0f) ? Mathf.Max (0f, cache - 0.2f) : Mathf.Min (0f, cache + 0.2f);
				zoomval = Mathf.Max (0f, Mathf.Min (1f, lastZoomval + cache));
				cache = startGravityDelta + Mathf.Atan2 (twoTouchs.y, twoTouchs.x);
				cache = (cache + Mathf.PI + Mathf.PI < 0f) ? (cache + Mathf.PI + Mathf.PI) : cache;
				cache = (cache - Mathf.PI - Mathf.PI > 0f) ? (cache - Mathf.PI - Mathf.PI) : cache;
				gravityAlpha = cache;
			} else {
				twofingers = true;
				lastZoomval = zoomval;
				lastZTwoTouchDist = twoTouchDist;
				startGravityDelta = gravityAlpha - Mathf.Atan2 (twoTouchs.y, twoTouchs.x);
			}
			onefinger = false;
			break;
		default:
			onefinger = false;
			twofingers = false;
			localEulerX *= 0.96f;
			localEulerY *= 0.96f;
			break;
		}

		Vector3 realgravity, realacc;
#if UNITY_EDITOR
		realgravity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f) * 19.6f;
		realacc = Vector3.zero;
#else
		realgravity = Input.gyro.gravity * 19.6f;
		realacc = Input.gyro.userAcceleration * 9.8f;
#endif
		Vector3 currentright = Vector3.Cross (world.up, currentup);
		realgravity = Vector3.ClampMagnitude (currentright * realgravity.x + currentup * realgravity.y, 9.8f) - world.up;
		realacc -= Vector3.ClampMagnitude (realacc, 5f);
		accFilter = accFilter * 0.5f + (currentright * realacc.x + currentup * realacc.y) * 25f;

		transform.position = currentlook + currentlookoffset - (1.01f - zoomval) * cameraMaxLen * tiltRatio * realgravity;
		transform.LookAt (currentlook, currentup);
		junction.localEulerAngles = new Vector3 ((localEulerX < 0f) ? (localEulerX + 360f) : localEulerX,
		                                         (localEulerY < 0f) ? (localEulerY + 360f) : localEulerY,
		                                         0f);
		Physics.gravity = realgravity * 2f + accFilter;
	}
}