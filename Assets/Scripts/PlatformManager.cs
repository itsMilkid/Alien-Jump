using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject platformPrefab;

	[Header("Platform Placement")]
	public float minYCoord;
	public float maxYCoord;

	[Header("Pooling Settings:")]
	public int poolSize;

	private float maxXCoord;

	private bool lerpCamera;
	private float lerpTime = 1.5f;
	private float lerpXValue;

	public List<GameObject> pooledPlatforms = new List<GameObject>();
	public List<GameObject> activePlatforms = new List<GameObject>();

	private void Awake(){
		pooledPlatforms.Clear ();
		activePlatforms.Clear ();

		EvaluateScreensize ();
		PopulatePlatformPool ();
	}

	private void Update(){
		//RestackPool ();

		if (lerpCamera == true)
			LerpCameraPosition ();
	}

	private void EvaluateScreensize(){
		float distanceToScreen = transform.position.z - Camera.main.transform.position.z;
		maxXCoord = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToScreen)).x - 1.0f;
	}

	private void PopulatePlatformPool(){
		for (int i = 0; i < poolSize; i++) {
			GameObject obj = Instantiate (platformPrefab, new Vector3 (-20, -20, 0), Quaternion.identity);
			obj.SetActive (false);
			pooledPlatforms.Add (obj);
		}
	}

	public void InitiatePlatformSpawn(float _newPos){
		ReplaceAndActivatePlatform ();

		lerpXValue = _newPos + (maxXCoord - 5.0f);
		lerpCamera = true;
	}

	private void ReplaceAndActivatePlatform(){
		float cameraXPos = Camera.main.transform.position.x;
		float newMaxXCoord = (maxXCoord * 2) + cameraXPos;

		float platformPosX = Random.Range (newMaxXCoord, newMaxXCoord - 3f);
		float platformPosY = Random.Range (minYCoord, maxYCoord);
		Vector3 newPlatformPos = new Vector3 (platformPosX, platformPosY, 0);

		GameObject newPlatform = pooledPlatforms [0];
		newPlatform.transform.position = newPlatformPos;
		newPlatform.SetActive (true);
		pooledPlatforms.Remove (newPlatform);
		activePlatforms.Add (newPlatform);
	}

	private void LerpCameraPosition(){
		float currentCameraPositionX = Camera.main.transform.position.x;
		float lerpPosition = Mathf.Lerp (currentCameraPositionX, lerpXValue, lerpTime * Time.deltaTime);

		Camera.main.transform.position = new Vector3 (lerpPosition, Camera.main.transform.position.y, Camera.main.transform.position.z);
		if (Camera.main.transform.position.x == lerpXValue) {
			lerpCamera = false;
		}
	}

	/*private void RestackPool(){
		for (int i = 0; i < activePlatforms.Count; i++) {
			GameObject platform = activePlatforms [i];
			if (platform.activeSelf == false) {
				activePlatforms.Remove (platform);
				pooledPlatforms.Add (platform);
			}
		}
	}*/



}
