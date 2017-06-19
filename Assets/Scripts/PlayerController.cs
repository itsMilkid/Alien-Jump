using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	[Header("Jump Force:")]
	public float thresholdX = 5.5f;
	public float thresholdY = 10.0f;

	[Header("UI - POWERBAR:")]
	public Slider powerBar;

	[Header("LayerMaks Platforms:")]
	public LayerMask platformLayer;
	
	[Header("Groundcheck Raycast Position:")]
	public Transform leftRay;
	public Transform rightRay;

	private Rigidbody2D rb2d;
	private Animator anim;

	private float forceX, forceY;

	private bool setPower;
	public bool isGrounded;

	private float powerBarValue = 0.0f;

	private void Awake(){
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		powerBar.minValue = 0.0f;
		powerBar.maxValue = thresholdY;
		powerBar.value = powerBarValue;
	}

	private void Update(){
		CheckForInput ();
		SetJumpStrength ();
		PlayAnimation ();
		GroundCheck ();
	}

	private void CheckForInput(){
		if (Input.GetMouseButtonDown (0)) {
			if (isGrounded == true) {
				SettingPower (true);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (isGrounded == true) {
				SettingPower (false);
			}
		}
	}

	private void SettingPower(bool _setting){
		setPower = _setting;

		if (setPower == false) {
			Jump ();
		}
	}

	private void SetJumpStrength(){
		if (setPower == true) {
			forceX += thresholdX * Time.deltaTime;
			forceY += thresholdY * Time.deltaTime;

			if (forceX > 30.0f) {
				forceX = 30.0f;
			}

			if (forceY > 50.0f) {
				forceY = 50.0f;
			}

			powerBarValue = forceX;
			powerBar.value = powerBarValue;
		}
	}

	private void Jump(){
		rb2d.velocity = new Vector2 (forceX, forceY);
		forceX = 0;
		forceY = 0;

		anim.SetBool ("isJumping", true);

		powerBarValue = 0.0f;
		powerBar.value = powerBarValue;
	}

	private void PlayAnimation(){
		if (isGrounded == true) {
			anim.SetBool ("isJumping", false);
		} else {
			anim.SetBool ("isJumping", true);
		}
			
	}

	private void GroundCheck(){
		Debug.DrawRay (leftRay.position, Vector2.down * 10 , Color.red);
		Debug.DrawRay (rightRay.position, Vector2.down * 10 , Color.red);

		if (Physics2D.Raycast (leftRay.position, Vector2.down, 5, platformLayer) || Physics2D.Raycast (rightRay.position, Vector2.down, 5, platformLayer)) {
			Debug.Log ("Grounded");
			isGrounded = true;
		} else {
			isGrounded = false;
		}
	}
		
}
