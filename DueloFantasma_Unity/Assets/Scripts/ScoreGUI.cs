using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public float fxInterval;
	public Material winner1;
	public Material winner2;
	
	public float scrollSpeed = 1;
	
	private float timer;
	// Use this for initialization
	void Start () {
		
		if(GameManager.reg.winner == 1){
			renderer.material = winner1;
		}
		else {
			renderer.material = winner2;
		}
		
		timer = fxInterval;
	}
	
	// Update is called once per frame
	void Update () {
		
		timer -= Time.deltaTime;
		if(timer <= 0){
			timer = fxInterval;
		}
		
		if(GameManager.reg.state != GameState.SCORE){
			Destroy (this.gameObject);
		}
	}
}
