using UnityEngine;
using System.Collections;

public class RoundTimer : MonoBehaviour {
	
	public float roundTime;
	public float timer;
	public TextMesh label;
	
	// Use this for initialization
	void Start () {
		roundTime = GameManager.reg.roundTime;
		timer = roundTime;
		label = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		label.text = "Tempo\n" + timer.ToString("00") + "";
		if(timer <= 0){
			if(GameManager.reg.player2Score != GameManager.reg.player1Score){
				GameManager.reg.StartCoroutine("endRound");
			} 
			timer = 0;
			label.text = "Extra\n" + timer.ToString("00") + "";
			label.color = Color.yellow;
		}
	}
	
	
}
