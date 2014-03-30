using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			if(!col.GetComponent<PlayerShip>().invincible) 
				col.GetComponent<PlayerShip>().KillShip();
		}
		else if(col.gameObject.CompareTag("Bullet")) {
			Debug.Log("Bullet + Asteroid");
			Destroy (col.gameObject);
		}
	}
}
