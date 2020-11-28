using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveHit : MonoBehaviour {

	public float speed = 4f;
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
		Vector3 targetPos = new Vector3(AntTarget.position.x, transform.position.y , AntTarget.position.z);
		if (AntTarget != null){
			transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "AntPlayer"){
			other.gameObject.transform.position = AntHome.position;
			//make player drop all food
			other.gameObject.GetComponent<AntCharacterMover>().LoseFood();
			rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
		}
		if (other.gameObject.tag == "GrasshopperPlayer"){
			other.gameObject.transform.position = GrasshopperHome.position;
			//make player drop all food
			other.gameObject.GetComponent<AntCharacterMover>().LoseFood();
			rend.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
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

}
