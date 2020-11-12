using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
public Transform onHand;

void OnMouseDown() {
 GetComponent<Rigidbody>().useGravity = false;
 this.transform.position = onHand.transform.position;
 this.transform.parent = GameObject.Find("ThirdPersonPlayer").transform;
}

void OnMouseUp (){
 this.transform.parent = null;
 GetComponent<Rigidbody>().useGravity = true;
}
}
