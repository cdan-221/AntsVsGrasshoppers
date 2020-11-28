using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCharacterMover : MonoBehaviour
{
	public GameHandlerAntsGrasshoppers gameHandler;
	Rigidbody rb;
	//Animator anim;
	public Transform cam;

	public float speed = 6f;
	public float turnSmoothTime = 0.1f;
	private float turnSmoothVelocity;

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
		//anim = gameObject.GetComponentInChildren<Animator>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update () {
		float horiz = Input.GetAxisRaw("P1Horiz");
		float vert = Input.GetAxisRaw("P1Vert");
		Vector3 direct = new Vector3(horiz, 0f, vert).normalized;

		if (direct.magnitude >= 0.1f) {
			float targetAngle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
			//controller.Move(moveDir.normalized * speed * Time.deltaTime);
			rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
		}
	}

	public void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 10){
			CurrentFood = other.gameObject;
			PickUpFood();
		}

		if (other.gameObject.tag == "AntBase"){
			PutDownFood();
		}
	}

	//by keypress: if inside of object trigger collider, then activate display of "hit [] to pickup"
	//object has weight variable and activates PickUpFood() function in characetr script  
	//then if the player picks it up, turn off colliders so bumping the other chracter does not re-trigger 

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
				Debug.Log("slot 1 filled! \n current load weight = " + currentLoad);
			}
			else if (slot2isOpen == true){
				currentLoad += objWeightTemp;
				CurrentFood.transform.parent = foodSlot2;
				CurrentFood.transform.position = foodSlot2.position;
				CurrentFood.GetComponent<Rigidbody>().isKinematic = true;
				CurrentFood.GetComponent<Collider>().enabled = false;
				slot2Food = CurrentFood;
				slot2isOpen = false;
				Debug.Log("slot 2 filled! \n current load weight = " + currentLoad);
			}
			else if (slot3isOpen == true){
				currentLoad += objWeightTemp;
				CurrentFood.transform.parent = foodSlot3;
				CurrentFood.transform.position = foodSlot3.position;
				CurrentFood.GetComponent<Rigidbody>().isKinematic = true;
				CurrentFood.GetComponent<Collider>().enabled = false;
				slot3Food = CurrentFood;
				slot3isOpen = false;
				Debug.Log("slot 3 filled! \n current load weight = " + currentLoad);
			}
		}
		else {
			Debug.Log("Not enough room for this food! \n Current weight: " + currentLoad);
		}

		//check to see if slot 1 is open. if it is, add object weigth to current load, move the object to that slot, parent it, and turn off RB and collider.
		//if not, check slot 2 and slot 3
		// if no slot available, indicate that the player is at maximum capacity!
	}

	public void PutDownFood(){
		if (slot3isOpen == false){
			Destroy(slot3Food.gameObject);
			slot3isOpen = true;
			Debug.Log("slot 3 emptied! \n current load weight = " + currentLoad);
		}
		if (slot2isOpen == false){
			Destroy(slot2Food.gameObject);
			slot2isOpen = true;
			Debug.Log("slot 2 emptied! \n current load weight = " + currentLoad);
		}
		if (slot1isOpen == false){
			Destroy(slot1Food.gameObject);
			slot1isOpen = true;
			Debug.Log("slot 1 emptied! \n current load weight = " + currentLoad);
		}

		gameHandler.GetComponent<GameHandlerAntsGrasshoppers>().updateScore("ant", currentLoad);
		Debug.Log("Current load weight = " + currentLoad);
		currentLoad = 0;
	}

	public void LoseFood(){
		if (slot3isOpen == false){
			//CHANGE THIS so instead of destroying food, it unparents it: slot3Food.gameObject.parent=null;
			Destroy(slot3Food.gameObject);
			slot3isOpen = true;
			Debug.Log("slot 3 food lost!");
		}
		if (slot2isOpen == false){
			Destroy(slot2Food.gameObject);
			slot2isOpen = true;
			Debug.Log("slot 2 food lost!");
		}
		if (slot1isOpen == false){
			Destroy(slot1Food.gameObject);
			slot1isOpen = true;
			Debug.Log("slot 1 food lost!");
		}

		currentLoad = 0;
	}

} 


//trigger:
// simply hitting object collider: OnCollisionEnter(){if layer == 10}
//by keypress: if inside of object trigger collider, then activate display of "hit [] to pickup"
//object has weight variable and activates PickUpFood() function in characetr script  
//then if the player picks it up, turn off colliders so bumping the other chracter does not re-trigger 

//public void PickUpFood(){
	//need a public variable for the gameobject in collision
	//make a boolean for each slot: slot1isOpen
	//make an int for capacity and an int for current load (maximum for ant =3, gh=4?)
	//check current weight first: check availabel weight of player capacity, check objt weight (temp int availabelCapacity = capacity - load, if objWeight <= availablecapacity)
	//check to see if slot 1 is open. if it is, add object weigth to current load, move the object to that slot, parent it, and turn off RB and collider.
	//if not, check slot 2 and slot 3
	// if no slot available, indicate that the player is at maximum capacity!
//}

//public void PutDownFood(){
	//triggered by coliding with homebase
	//delete all objects in the slots (need gameobject variables for each slot, to then delete them)
	// send current load value to add to score GameObject
	//set current load value to zero

//}