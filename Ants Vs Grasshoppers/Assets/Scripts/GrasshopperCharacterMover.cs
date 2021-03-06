﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperCharacterMover : MonoBehaviour
{
	public GameHandlerAntsGrasshoppers gameHandler;
	//public CharacterController controller;
	public Rigidbody rb;
	public Animator anim;
	public Transform cam;

	public float speed = 3f;
	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

	public float gravity = -9.81f;
	public float jumpHeight = 10;
	private Vector3 velocity;
	//private bool isGrounded;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	private Vector3 playerVelocity;
	private bool groundedPlayer;

	public int carryCapacity = 3;
	public int currentLoad;
	public Transform foodSlot1;
	public Transform foodSlot2;
	public Transform foodSlot3;
	private GameObject slot1Food;
	private GameObject slot2Food;
	private GameObject slot3Food;
	private GameObject CurrentFood;
	private bool slot1isOpen = true; 
	private bool slot2isOpen = true; 
	private bool slot3isOpen = true; 


	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
	}


	void Update () {
		float horiz = Input.GetAxisRaw("P2Horiz");
		float vert = Input.GetAxisRaw("P2Vert");
		Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

		if ((horiz != 0f) || (vert != 0f)){anim.SetBool("Walk", true);}
		else {anim.SetBool("Walk", false);}

		if (direct.magnitude >= 0.1f) {
			float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			//controller.Move(moveDir.normalized * speed * Time.deltaTime);
			rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
			//anim.SetBool("Walk", true);
		}
		
		if (direct.magnitude < 0.1f) {
			//anim.SetBool("Walk", false);
		}
		

		//JUMP
		if (Input.GetKeyDown(KeyCode.Space)){
			anim.SetTrigger("Jump");
			
			rb.AddForce(Vector3.up * jumpHeight);
			rb.AddRelativeForce(Vector3.forward * jumpHeight);
		}
		
		//Vector3 localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);

		//groundedPlayer = controller.isGrounded;
		//if (Input.GetButtonDown("Jump") && groundedPlayer)
		//if (Input.GetButtonDown("Jump")){
		//	playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
		//}

		//playerVelocity.y += gravity * Time.deltaTime;
		//controller.Move(playerVelocity * Time.deltaTime);
		//jump: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html?_ga=2.64109231.1055814972.1604523466-1492216195.1601062187

//		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
//		if (isGrounded && velocity.y < 0){
//			velocity.y = -2f;
//		}
//
//		if (Input.GetButtonDown("Jump") && isGrounded){
//			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
//		}
//
//		//GRAVITY
//		velocity.y += gravity * Time.deltaTime;
//		controller.Move(velocity * Time.deltaTime);
	}


	public void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 10){
			CurrentFood = other.gameObject;
			PickUpFood();
		}

		if (other.gameObject.tag == "GrasshopperBase"){
			PutDownFood();
			Debug.Log("Grasshopper is home");
		}
	}

	public void PickUpFood(){
		int availCapacity = carryCapacity - currentLoad;
		int objWeightTemp = CurrentFood.GetComponent<AntGrasshopperObjectPickUp>().objWeight;
		if (availCapacity >= objWeightTemp){
			if (slot1isOpen == true){
				currentLoad += objWeightTemp;
				CurrentFood.transform.parent = foodSlot1;
				CurrentFood.transform.position = foodSlot1.position;
				CurrentFood.GetComponent<Rigidbody>().isKinematic = true;
				CurrentFood.GetComponent<Collider>().enabled = false;
				slot1Food = CurrentFood;
				slot1isOpen = false;
				Debug.Log("GH: slot 1 filled! \n current load weight = " + currentLoad);
			}
			else if (slot2isOpen == true){
				currentLoad += objWeightTemp;
				CurrentFood.transform.parent = foodSlot2;
				CurrentFood.transform.position = foodSlot2.position;
				CurrentFood.GetComponent<Rigidbody>().isKinematic = true;
				CurrentFood.GetComponent<Collider>().enabled = false;
				slot2Food = CurrentFood;
				slot2isOpen = false;
				Debug.Log("GH: slot 2 filled! \n current load weight = " + currentLoad);
			}
			else if (slot3isOpen == true){
				currentLoad += objWeightTemp;
				CurrentFood.transform.parent = foodSlot3;
				CurrentFood.transform.position = foodSlot3.position;
				CurrentFood.GetComponent<Rigidbody>().isKinematic = true;
				CurrentFood.GetComponent<Collider>().enabled = false;
				slot3Food = CurrentFood;
				slot3isOpen = false;
				Debug.Log("GH: slot 3 filled! \n current load weight = " + currentLoad);
			}
		}
		else {
			Debug.Log("GH: Not enough room for this food! \n Current weight: " + currentLoad);
		}
	}

	public void PutDownFood(){
		if (slot3isOpen == false){
			Destroy(slot3Food.gameObject);
			slot3isOpen = true;
			Debug.Log("GH: slot 3 emptied! \n current load weight = " + currentLoad);
		}
		if (slot2isOpen == false){
			Destroy(slot2Food.gameObject);
			slot2isOpen = true;
			Debug.Log("GH: slot 2 emptied! \n current load weight = " + currentLoad);
		}
		if (slot1isOpen == false){
			Destroy(slot1Food.gameObject);
			slot1isOpen = true;
			Debug.Log("GH: slot 1 emptied! \n current load weight = " + currentLoad);
		}

		gameHandler.GetComponent<GameHandlerAntsGrasshoppers>().updateScore("grasshopper", currentLoad);
		Debug.Log("GH: Current load weight = " + currentLoad);
		currentLoad = 0;
	}

	public void LoseFood(){
		anim.SetTrigger("Hurt");
		if (slot3isOpen == false){
			slot3Food.GetComponent<Rigidbody>().isKinematic = false;
			slot3Food.GetComponent<Collider>().enabled = true;
			slot3Food.gameObject.transform.parent = null;
			slot3isOpen = true;
			Debug.Log("GH: slot 3 food lost!");
		}
		if (slot2isOpen == false){
			slot2Food.GetComponent<Rigidbody>().isKinematic = false;
			slot2Food.GetComponent<Collider>().enabled = true;
			slot2Food.gameObject.transform.parent = null;
			slot2isOpen = true;
			Debug.Log("GH: slot 2 food lost!");
		}
		if (slot1isOpen == false){
			slot1Food.GetComponent<Rigidbody>().isKinematic = false;
			slot1Food.GetComponent<Collider>().enabled = true;
			slot1Food.gameObject.transform.parent = null;
			slot1isOpen = true;
			Debug.Log("GH: slot 1 food lost!");
		}
		currentLoad = 0;
	}

} 