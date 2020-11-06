using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCharacterMover : MonoBehaviour
{
	//public CharacterController controller;
	Rigidbody rb;
	//Animator anim;
	public Transform cam;

	public bool useRB = false;

	public float speed = 6f;

	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

	//public float gravity = -9.81f;
	//public float jumpHeight = 3;
	//private Vector3 velocity;
	//private bool isGrounded;
	//public Transform groundCheck;
	//public float groundDistance = 0.4f;
	//public LayerMask groundMask;

	void Start(){
		//anim = gameObject.GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update () {
		float horiz = Input.GetAxisRaw("AntHoriz");
		float vert = Input.GetAxisRaw("AntVert");
		Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

		if (direct.magnitude >= 0.1f) {
			float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			//controller.Move(moveDir.normalized * speed * Time.deltaTime);
			rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
		}

		//JUMP
		//isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		//if (isGrounded && velocity.y < 0){
		//	velocity.y = -2f;
		//}

		//if (Input.GetButtonDown("Jump") && isGrounded){
		//	velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		//}

		//GRAVITY
		//velocity.y += gravity * Time.deltaTime;
		//controller.Move(velocity * Time.deltaTime);
	}
} 