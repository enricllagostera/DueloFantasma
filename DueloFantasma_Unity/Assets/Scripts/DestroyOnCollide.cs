using UnityEngine;
using System.Collections;

public class DestroyOnCollide : MonoBehaviour {

	void OnCollisionEnter(){
		Debug.Log("Destroy " + this.name);
		//this.collider.enabled = false;
		//this.gameObject.SetActive(false);
		Destroy(this.gameObject, 0f);
	}
}
