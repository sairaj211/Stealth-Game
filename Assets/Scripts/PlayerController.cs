using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public event System.Action OnReachedEndLevel;

	public float moveSpeed = 8;
	public float smoothMoveTime = .1f;
	public float turnSpeed = 8;

	float smoothInputMagnitude; 
	float smoothMoveVelocity;
	float angle;
	Vector3 velocity;

	Rigidbody rigidbody;
	bool disabled;

	void Start(){
		rigidbody = GetComponent<Rigidbody> ();
		Guard.onGuardHasSpottedPlayer += Disable;
	}


	void Update () {
		Vector3 inputDirection = Vector3.zero;
		if(!disabled){
			inputDirection = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
		}
		float inputMagnitude = inputDirection.magnitude;
		smoothInputMagnitude = Mathf.SmoothDamp (smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

		float targetAngle = Mathf.Atan2 (inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
		angle = Mathf.LerpAngle (angle, targetAngle, Time.deltaTime * turnSpeed *inputMagnitude);
		//transform.eulerAngles = Vector3.up * angle;

		//transform.Translate (transform.forward * moveSpeed * Time.deltaTime * inputMagnitude, Space.World);

		velocity = transform.forward * moveSpeed * smoothInputMagnitude;
	}

	void FixedUpdate(){
		rigidbody.MoveRotation (Quaternion.Euler (Vector3.up * angle));
		rigidbody.MovePosition (rigidbody.position + velocity * Time.deltaTime);
	}

	void OnTriggerEnter(Collider hitCollider){
		if (hitCollider.tag == "Finish") {
			Disable ();
			if (OnReachedEndLevel != null) {
				OnReachedEndLevel ();
			}
		}
	}


	void Disable(){
		disabled = true;
	}

	void OnDestroy(){
		Guard.onGuardHasSpottedPlayer -= Disable;
	}
}
