using UnityEngine;
using System.Collections;

public class UpdateIngameGUI : MonoBehaviour {
	
	public int player;
	private TextMesh text;
	
	// Use this for initialization
	void Start () {
		text = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player == 1)
			text.text = GameManager.reg.player1Score + "";
		else 
			text.text = GameManager.reg.player2Score + "";
	}
}
