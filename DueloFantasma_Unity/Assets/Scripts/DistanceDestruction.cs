using UnityEngine;
using System.Collections;

public class DistanceDestruction : MonoBehaviour {

	public float distance = 40f;
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, Vector3.zero) > distance){
			Destroy(this.gameObject);
		}
	}
	
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere(Vector3.zero, distance);
	}
}
