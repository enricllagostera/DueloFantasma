using UnityEngine;
using System.Collections;

public class SpawnGhostZone : MonoBehaviour {
	
	public GameObject ghostPrefab;
	public float ghostSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.layer == LayerMask.NameToLayer("BulletsPlayer1") ||
			col.gameObject.layer == LayerMask.NameToLayer("BulletsPlayer2")){
			SpawnGhost(col.gameObject);
		}
	}
	
	void SpawnGhost(GameObject origin){
		
		Vector3 tempPos = origin.transform.position;
		//Quaternion tempRot = origin.transform.rotation;
		
		GameObject.Destroy(origin);
		
		Debug.Log("Spawn Ghost at " + this.gameObject.name);
		
		GameObject tempGhost = Instantiate(ghostPrefab, origin.transform.position, 
			this.transform.rotation) as GameObject;
		
		tempGhost.transform.rigidbody.AddRelativeForce(Vector3.forward*ghostSpeed, 
			ForceMode.VelocityChange);
		
		
	}
}
