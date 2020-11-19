using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	CharacterController controller;

	void Awake(){
		controller = GetComponent<CharacterController>();
	}

	void Update(){
		if (controller.velocity.y < 0) {
			controller.Move(Vector3.up * Physics.gravity.y * (fallMultiplier) * Time.deltaTime);

		} else if (controller.velocity.y > 0 && !Input.GetButton ("Jump")){  
			controller.Move(Vector3.up * Physics.gravity.y * (lowJumpMultiplier) * Time.deltaTime);
		}
	}
}