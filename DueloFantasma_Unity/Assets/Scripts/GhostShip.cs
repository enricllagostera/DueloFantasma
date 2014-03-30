using UnityEngine;
using System.Collections;

public class GhostShip : MonoBehaviour {
	
	public int player;
	public bool invincible = false;
	public float spawnTimer;
	
	public bool isShooting = false;
	public Bullet bulletPrefab;
	public Transform gunPoint;
	public float bulletPower;
	
	public float bulletInterval;
	private float bulletTimer;
	
	void Start(){
		invincible = true;
		bulletTimer = bulletInterval/2;
		ObjectPool.CreatePool<Bullet>(bulletPrefab);
	}
	
	void Update(){
		// invincible at spawn
		spawnTimer -= Time.deltaTime;
		if(spawnTimer <= 0)
			invincible = false;
		if(invincible){
			collider.enabled = false;
			//renderer.material.color = Color.yellow;
		}
		else {
			collider.enabled = true;
			//renderer.material.color = Color.white;
		}
		
		// shoot
		bulletTimer -= Time.deltaTime;
		if(bulletTimer <= 0){
			isShooting = true;
			bulletTimer = bulletInterval;
		}
			
	}
	
	void OnCollisionEnter(Collision col){
		if((player == 1 && col.gameObject.name == "SpawnZoneLeft") || 
			(player == 2 && col.gameObject.name == "SpawnZoneRight")){
			return;
		}
		Destroy (this.gameObject);
			
	}
	
	void FixedUpdate(){
		if(isShooting){
			isShooting = false;
			GameObject bullet = ObjectPool.Spawn(bulletPrefab, gunPoint.position, 
				gunPoint.rotation).gameObject;
			//bullet.transform.parent = this.transform;
			bullet.transform.rigidbody.AddRelativeForce(Vector3.forward * bulletPower, 
				ForceMode.Impulse);
			bullet.transform.rigidbody.AddTorque(0,100,100,ForceMode.Impulse);
		}
	}
}
