using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : MonoBehaviour {

	public int maxLives = 5;
	public float horizontalOffset = 4f;
	public float scale = 0.7f;
	public Transform iconPrefab;
	public List<Transform> icons;
	public int player;

	// Use this for initialization
	void Start () {
		icons = new List<Transform>();
		for (int i = 0; i < maxLives; i++) {
			icons.Add(
				Instantiate(iconPrefab, new Vector3(
					transform.position.x + i*horizontalOffset,
					transform.position.y,
					transform.position.z-10f),
			    	iconPrefab.rotation) as Transform);
		}
		icons.ForEach(item => {
			item.localScale = new Vector3(scale, scale, scale);
			item.parent = this.transform;
		});
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < icons.Count; i++) {
			if(player == 1){
				if(i < GameManager.reg.player1Score){
					Go.to(icons[i].transform, 0.5f, new GoTweenConfig()
					      .scale(new Vector3(0.0f,0.0f,0.0f), 
					       false)
					      .setEaseType(GoEaseType.ElasticInOut));
				}
				//icons[i].renderer.enabled = !(i < GameManager.reg.player1Score);
			}
			else {
				if(i < GameManager.reg.player2Score){
					Go.to(icons[icons.Count-1-i].transform, 0.5f, new GoTweenConfig()
					      .scale(new Vector3(0.0f,0.0f,0.0f), 
					       false)
					      .setEaseType(GoEaseType.ElasticInOut));
				}
				//icons[icons.Count-1-i].renderer.enabled = !(i < GameManager.reg.player2Score);
			}
		}
		CheckEndRound();

			
	}

	public void CheckEndRound(){
		if(player == 1){
			if(maxLives - GameManager.reg.player1Score == 0){
				GameManager.reg.endRound();
			}
		}
		else {
			if(maxLives - GameManager.reg.player2Score == 0){
				GameManager.reg.endRound();
			}
		}
	}
}
