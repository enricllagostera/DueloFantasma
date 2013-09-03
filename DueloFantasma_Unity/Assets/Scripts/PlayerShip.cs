using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour {
	
	public int player = 0;
	
	public Vector3 moveInput;
	private string horizontalAxis;
	private string verticalAxis;
	private string shootInput;
	
	public float moveAnimationTimer;
	public float moveAnimationValue;
	public float shootAnimationTimer;
	public float shootAnimationValue;
	
	public float speed;
	
	public bool isShooting = false;
	public GameObject bulletPrefab;
	public Transform gunPoint;
	public float bulletPower;
	
	private GameObject warpZoneTop;
	private GameObject warpZoneBottom;
	
	private float rotX;
	private float rotY;
	private float rotZ;
	
	public float respawnTimer;
	
	public bool invincible = false;
	
	// Use this for initialization
	void Start () {
		
		warpZoneTop = GameObject.Find("WarpZoneTop");
		warpZoneBottom = GameObject.Find("WarpZoneBottom");
		
		rotX = transform.localRotation.eulerAngles.x;
		rotY = transform.localRotation.eulerAngles.y;
		rotZ = transform.localRotation.eulerAngles.z;
		
	}
	
	void SetupControls(){
		if(player == 1){
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			shootInput = "shoot1";
		}
		else {
			horizontalAxis = "Horizontal2";
			verticalAxis = "Vertical2";
			shootInput = "shoot2";
		}
	}
	
	// Update is called once per frame
	void Update () {
		SetupControls();
		moveInput = new Vector3(Input.GetAxis(horizontalAxis), 
			Input.GetAxis(verticalAxis), 0);
		
		transform.localRotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));
		if(moveInput.y > 0){
			
			if(player == 1)
				transform.localRotation = Quaternion.Euler(new Vector3(
					rotX, rotY, rotZ + moveAnimationValue));
			else 
				transform.localRotation = Quaternion.Euler(new Vector3(
					rotX, rotY, rotZ - moveAnimationValue));
		}
		if(moveInput.y < 0){
			if(player == 1)
				transform.localRotation = Quaternion.Euler(new Vector3(
					rotX, rotY, rotZ - moveAnimationValue));
			else 
				transform.localRotation = Quaternion.Euler(new Vector3(
					rotX, rotY, rotZ + moveAnimationValue));
		}
		
		
		if(Input.GetButtonUp(shootInput)){
			isShooting = true;
		}
		
		if(invincible)
			renderer.material.color = Color.green;
		else
			renderer.material.color = Color.white;
		
	}
	
	void FixedUpdate() {
		transform.rigidbody.AddForce(moveInput.normalized*speed, ForceMode.VelocityChange);
		
		if(isShooting){
			isShooting = false;
			GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, 
				gunPoint.rotation) as GameObject;
			//bullet.transform.parent = this.transform;
			bullet.transform.rigidbody.AddRelativeForce(Vector3.forward * bulletPower, 
				ForceMode.Impulse);
			bullet.transform.rigidbody.AddTorque(0,100,100,ForceMode.Impulse);
		}
	}
	
	void OnCollisionEnter(Collision col){
		
		if(col.gameObject.name != "CenterBlock" 	&& 
			col.gameObject.name != "SpawnZoneLeft" 	&& 
			col.gameObject.name != "SpawnZoneRight" &&
			col.gameObject.name != "WarpZoneTop" &&
			col.gameObject.name != "WarpZoneBottom"){
			//Debug.Log("Destroyed: " + this.name);
			if(!invincible)
				killShip();
			
		}
		else if (col.gameObject == warpZoneTop){
			transform.position = new Vector3(
				transform.position.x,
				warpZoneBottom.transform.position.y + 3,
				0);
		}
		else if (col.gameObject == warpZoneBottom){
			transform.position = new Vector3(
				transform.position.x,
				warpZoneTop.transform.position.y - 3,
				0);
		}
		
		//GameObject.DestroyObject(this);
	}
	
	void killShip(){
		
		Debug.Log (transform.GetComponentInChildren<ParticleSystem>().name);
		transform.GetComponentInChildren<ParticleSystem>().Stop();
		transform.GetComponentInChildren<ParticleSystem>().Clear();
		transform.GetComponentInChildren<ParticleSystem>().Play();
		
		if(player == 1){
			GameManager.reg.player2Score++;
		}
		else {
			GameManager.reg.player1Score++;
		}
		//Destroy(this.gameObject);
		GameManager.reg.StartCoroutine("respawnShip", player);
	}

}
