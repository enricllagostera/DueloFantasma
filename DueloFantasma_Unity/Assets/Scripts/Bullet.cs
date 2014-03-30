using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, Vector3.zero) > distance){
			Destroy(this);
		}
	}

	void OnDestroy(){
		Debug.Log ("Destroy Bullet: " + name);
		ObjectPool.Recycle(this);
	}
}
