using UnityEngine;
using System.Collections;

public class AutomaticPositioning : MonoBehaviour
{
	
	public bool IsProportionatePositioningEnabled = true;
	private Vector2 BaseDeviceAspect;
	public Camera UICamera;
	float OrigPos = -99999f;
	bool IsObjectPositionSet = false;
	
	void Start ()
	{
		SetPos ();
	}
	
	void OnEnable ()
	{
		if (OrigPos == -99999f) {
			SetPos ();        
		}
	}
	
	// Use this for initialization
	void SetPos ()
	{
		if (IsObjectPositionSet) {
			return;        
		}
		
		if (UICamera == null) {
			UICamera = GameObject.FindGameObjectWithTag ("MainCamera").camera;        
		}
		if (!IsProportionatePositioningEnabled) {
			return;        
		}
		if (UICamera.aspect < 1) {
			BaseDeviceAspect = new Vector2 (9, 16);        
		} else {
			BaseDeviceAspect = new Vector2 (4, 3);        
		}
		float baseAspect = ((float)BaseDeviceAspect.x) / ((float)BaseDeviceAspect.y);
		float BaseWorldWidth = 0;
		if (UICamera.aspect < 1) {
			if (baseAspect < 0.6f) {
				BaseWorldWidth = 5.622817f;
			} else if (baseAspect < 0.685f) {
				BaseWorldWidth = 6.670547f;
			} else if (baseAspect < 0.8f) {
				BaseWorldWidth = 7.49709f;
			}
		} else {
			if (baseAspect < 1.45f) {
				BaseWorldWidth = 13.32068f;
			} else if (baseAspect < 1.55f) {
				BaseWorldWidth = 15f;
			} else {
				BaseWorldWidth = 17.77215f;
			}
		}
		float CameraSizeDifferencePercentage = (5f - UICamera.orthographicSize) / 5f;
		BaseWorldWidth = BaseWorldWidth - (CameraSizeDifferencePercentage * BaseWorldWidth);
		float ScreenWidthInWorld = UICamera.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 1)).x - UICamera.ScreenToWorldPoint (new Vector3 (0, 0, 1)).x;
		float diff = 0f;
		if (transform.parent == null) {
			if (transform.position.x < 0) {
				diff = transform.position.x - (-BaseWorldWidth / 2f);
				transform.position = new Vector3 ((-ScreenWidthInWorld / 2f) + diff, transform.position.y, transform.position.z);
			} else if (transform.position.x > 0) {
				diff = (BaseWorldWidth / 2f) - transform.position.x;
				transform.position = new Vector3 ((ScreenWidthInWorld / 2f) - diff, transform.position.y, transform.position.z);
			}
		} else {
			if (transform.localPosition.x < 0) {
				diff = transform.localPosition.x - (-BaseWorldWidth / 2f);
				transform.localPosition = new Vector3 ((-ScreenWidthInWorld / 2f) + diff, transform.localPosition.y, transform.localPosition.z);
			} else if (transform.localPosition.x > 0) {
				diff = (BaseWorldWidth / 2f) - transform.localPosition.x;
				transform.localPosition = new Vector3 ((ScreenWidthInWorld / 2f) - diff, transform.localPosition.y, transform.localPosition.z);
			}
		}
		OrigPos = diff;
		
		IsObjectPositionSet = true;
		
		
	}    
	
	
	
}