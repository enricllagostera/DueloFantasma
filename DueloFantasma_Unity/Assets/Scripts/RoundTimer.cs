using UnityEngine;
using System.Collections;

public class RoundTimer : MonoBehaviour {
	
	public float roundTime;
	public float timer;
	public TextMesh label;
	public bool endOfRound = false;
	public bool countdown = false;
	
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
		
		if(timer <= 10 && !endOfRound && !countdown){
			countdown = true;
			GameManager.reg.transform.audio.clip = SoundManager.reg.musShowdown;
			GameManager.reg.transform.audio.Play();
			Go.to (transform, 0.5f, new GoTweenConfig()
				.scale (new Vector3(0.25f, 0.25f, 1), true)
				.setIterations(-1));
			label.color = Color.green;	
		}
		
		if(timer <= 0 && !endOfRound) {
			timer = 0;
			if(GameManager.reg.player2Score != GameManager.reg.player1Score){
				GameManager.reg.endRound();
				endOfRound = true;
			} 
			label.text = "Extra\n" + timer.ToString("00") + "";
			label.color = Color.yellow;
		}
	}
	
	
}

