using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	
	public float blinkInterval;
	public Color color1;
	public Color color2;
	
	private float timer;
	// Use this for initialization
	void Start () {
		renderer.material.color = color1;
		timer = blinkInterval;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0){
			timer = blinkInterval;
			if(renderer.material.color == color1){
				renderer.material.color = color2;
			}
			else {
				renderer.material.color = color1;
			}
		}
		
		if(GameManager.reg.state != GameState.TITLE){
			Destroy (this.gameObject);
		}
	}
}
