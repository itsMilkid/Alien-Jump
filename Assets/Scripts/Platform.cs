using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	private PlatformManager platformManager;
	private ScoreManager scoreManager;

	private BoxCollider2D triggerCollider;

	private float cameraPosition;

	private void Start(){
		platformManager = GameObject.FindGameObjectWithTag ("PlatformManager").gameObject.GetComponent<PlatformManager> ();
		scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").gameObject.GetComponent<ScoreManager> ();

		triggerCollider = GetComponent<BoxCollider2D> ();
	}

	private void Update(){
		cameraPosition = Camera.main.transform.position.x;

		if (transform.position.x < cameraPosition - 40.0f) {
			triggerCollider.enabled = true;
			platformManager.activePlatforms.Remove (gameObject);
			platformManager.pooledPlatforms.Add (gameObject);
			gameObject.SetActive (false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if (collider.gameObject.tag == "Player") {
			platformManager.InitiatePlatformSpawn (transform.position.x);
			scoreManager.score += 1;
			triggerCollider.enabled = false;
		}
	}
}
