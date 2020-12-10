using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHomeArrow : MonoBehaviour
{

	public Transform bugHome;
	public float arrowZpos = 3.6f;
	public Transform arrowMesh;

	void Start(){
		Vector3 arrowMeshPos = new Vector3 (arrowMesh.position.x,arrowMesh.position.y, arrowMesh.position.z + arrowZpos);
		arrowMesh.position = arrowMeshPos;
	}

    void Update()
    {
	transform.LookAt(bugHome);
    }
}
