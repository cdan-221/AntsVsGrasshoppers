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
	private Animator anim;
	//private GameController gameControllerObj;

	public Transform AntHome;
	public Transform GrasshopperHome;
	public Transform rangePoint;
	public float attackRange = 10f;


	void Start () {
		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponentInChildren<Animator> ();

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
		float birdDistance = Vector3.Distance(transform.position, rangePoint.position);
		if ((antDistance >= attackRange + 1) && (ghDistance >= attackRange + 1)){currentTarget = rangePoint; anim.SetBool("Walk", true); anim.SetBool("Chase", false);}
		else if ((antDistance >= attackRange + 1) && (ghDistance >= attackRange + 1) && (birdDistance <= 5)){anim.SetBool("Walk", false); anim.SetBool("Chase", false);}
		else if ((antDistance <= attackRange) && (ghDistance <= attackRange)){
			if (antDistance > ghDistance){currentTarget = AntTarget;}
			else if (ghDistance > antDistance){currentTarget = GrasshopperTarget; anim.SetBool("Chase", true); anim.SetBool("Walk", false);}
			}
		else if ((antDistance <= attackRange) && (ghDistance >= attackRange + 1)){currentTarget = AntTarget; anim.SetBool("Chase", true);anim.SetBool("Walk", false);}
		else if ((antDistance >= attackRange +1) && (ghDistance <= attackRange)){currentTarget = GrasshopperTarget; anim.SetBool("Chase", true);anim.SetBool("Walk", false);}

		
		Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y , currentTarget.position.z);
		transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		transform.LookAt(targetPos);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "AntPlayer"){
			//make player drop all food
			anim.SetBool("Chase", false);
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
