using UnityEngine;

public class Camera3DLERP : MonoBehaviour {

	public Transform target; // drag the intended target object into the Inspector slot
	public float smoothSpeed = 10f;
	public Vector3 offset; // set the offset in the editor


	// Update is called once per frame
	void FixedUpdate () {
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothPosition;

		transform.LookAt (target);
	}
}