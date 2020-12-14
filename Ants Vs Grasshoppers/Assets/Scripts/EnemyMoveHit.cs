using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

	public float speed = 4f;
	public Transform currentTarget;
	public Transform AntTarget;
	public Transform GrasshopperTarget;

	//public int EnemyLives = 3;
	private Renderer rend;
	//private GameController gameControllerObj;

	public Transform AntHome;
	public Transform GrasshopperHome;
	public Transform rangePoint;
	public float attackRange = 10f;


	void Start () {
		rend = GetComponentInChildren<Renderer> ();

		//if (GameObject.FindGameObjectWithTag ("Player") != null) {
		//	target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		//}
		//GameObject gameControllerLocation = GameObject.FindWithTag ("GameController");
		//if (gameControllerLocation != null) {
		//	gameControllerObj = gameControllerLocation.GetComponent<GameController> ();
		//}
		
	}

	void Update () {
		float antDistance = Vector3.Distance(AntTarget.position, rangePoint.position);
		float ghDistance = Vector3.Distance(GrasshopperTarget.position, rangePoint.position);
		if ((antDistance >= attackRange + 1) && (ghDistance >= attackRange + 1)){currentTarget = rangePoint;}
		else if ((antDistance <= attackRange) && (ghDistance <= attackRange)){
			//float antRange = Vector3.Distance(AntTarget.position, gameObject.transform.position);
			//float ghRange = Vector3.Distance(GrasshopperTarget.position, gameObject.transform.position);
			//int AntRand = Random.Range (1, 5);
			//int GHRand = Random.Range (1, 5);
			if (antDistance > ghDistance){currentTarget = AntTarget;}
			else if (ghDistance > antDistance){currentTarget = GrasshopperTarget;}
			}
		else if ((antDistance <= attackRange) && (ghDistance >= attackRange + 1)){currentTarget = AntTarget;}
		else if ((antDistance >= attackRange +1) && (ghDistance <= attackRange)){currentTarget = GrasshopperTarget;}

		
		Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y , currentTarget.position.z);
		transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "AntPlayer"){
			//make player drop all food
			other.gameObject.GetComponent<AntCharacterMover>().LoseFood();
			other.gameObject.transform.position = AntHome.position;
			StartCoroutine(colorChange());
		}
		if (other.gameObject.tag == "GrasshopperPlayer"){
			//make player drop all food
			other.gameObject.GetComponent<GrasshopperCharacterMover>().LoseFood();
			other.gameObject.transform.position = GrasshopperHome.position;
			StartCoroutine(colorChange());
		}
	}

	//IEnumerator HitEnemy(){
	//	// color values are R, G, B, and alpha, each divided by 100
	//	rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
	//	if (EnemyLives < 1){
	//		gameControllerObj.AddScore (1);
	//		Destroy(gameObject);
	//	}
	//	else yield return new WaitForSeconds(0.5f);
	//	rend.material.color = Color.white;
	//}

   void OnDrawGizmosSelected(){
        if (rangePoint == null) return;
        Gizmos.DrawWireSphere(rangePoint.position, attackRange);
   }
   
   IEnumerator colorChange(){
	   rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
	   yield return new WaitForSeconds(1.0f);
	   rend.material.color = Color.white;
   }

}
