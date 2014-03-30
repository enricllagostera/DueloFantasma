using UnityEngine;
using System.Collections;

public class SpawnGhostZone : MonoBehaviour {
	
	public GhostShip ghostPrefab;
	public float ghostSpeed;
	
	// Use this for initialization
	void Start () {
		ObjectPool.CreatePool(ghostPrefab);
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
		
		GameObject.Destroy(origin);
		
		Debug.Log("Spawn Ghost at " + this.gameObject.name);
		
		GameObject tempGhost = ObjectPool.Spawn(ghostPrefab, origin.transform.position, 
			ghostPrefab.transform.rotation).gameObject;
		
		tempGhost.transform.rigidbody.AddRelativeForce(Vector3.forward*ghostSpeed, 
			ForceMode.VelocityChange);
	}
}
