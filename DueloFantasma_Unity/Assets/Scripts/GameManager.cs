using UnityEngine;
using System.Collections;

public enum GameState {
	TITLE = 0,
	PLAY = 1,
	SCORE = 2
}

public class GameManager : MonoBehaviour {
	
	public static GameManager reg;
	
	public GameState state = GameState.TITLE;
	public bool stateChanged;
	
	public GameObject titleGUIPrefab;
	public GameObject titleGUI;
	public GameObject ingameGUIPrefab;
	public GameObject ingameGUI;
	public GameObject scoreGUIPrefab;
	public GameObject scoreGUI;
	
	public GameObject levelPrefab;
	public GameObject level;
	
	public GameObject player1Prefab;
	public GameObject player1;
	public Color player1Color;
	public GameObject player2Prefab;
	public GameObject player2;
	public Color player2Color;
	public GameObject asteroidPrefab;
	
	public float asteroidSpeed = 10;
	public float asteroidTimerBase = 10;
	public float asteroidTimerRandom = 10;
	public float asteroidTimer = 3;
	
	public int player1Score = 0;
	public int player2Score = 0;
	public int winner = 0;
	public float respawnTimer = 5;
	public float roundTime = 60;
	
	void Awake(){
		reg = this;
		stateChanged = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(state){
		case GameState.TITLE :
			if(stateChanged){
				stateChanged = false;
				transform.audio.clip = SoundManager.reg.musTheme;
				transform.audio.Play();
				level = Instantiate(levelPrefab)  as GameObject;
				titleGUI = Instantiate (titleGUIPrefab)  as GameObject;
				asteroidTimer = asteroidTimerBase + asteroidTimerRandom*Random.value;
			}
			
			break;
		case GameState.PLAY :
			if(stateChanged) {
				stateChanged = false;
				transform.audio.clip = SoundManager.reg.musPlay;
				transform.audio.Play();
				level = Instantiate(levelPrefab)  as GameObject;
				player1 = Instantiate(player1Prefab) as GameObject;
				player2 = Instantiate(player2Prefab) as GameObject;
				
				ingameGUI = Instantiate(ingameGUIPrefab) as GameObject;
				
				asteroidTimer = asteroidTimerBase + asteroidTimerRandom*Random.value;
			}
			
			break;
		case GameState.SCORE :
			if(stateChanged) {
				stateChanged = false;
				if(player1Score > player2Score){
					winner = 1;
				}
				else {
					winner = 2;
				}
				transform.audio.clip = SoundManager.reg.musResult;
				transform.audio.Play();
				level = Instantiate(levelPrefab)  as GameObject;
				scoreGUI = Instantiate(scoreGUIPrefab) as GameObject;
				Go.to (scoreGUI.transform, 0.43f/2, new GoTweenConfig()
					.scale(new Vector3(1f, 1f, 0), true)
					.setIterations(-1));
				asteroidTimer = Mathf.Infinity;
			}
			
			break;
		}
		
		
		HandleInputs();
		
		UpdateAsteroids();
	}
		
	IEnumerator respawnShip(int player){
		GameObject ship;
		if(player == 1){
			player1.transform.position = player1Prefab.transform.position;
			player1.transform.rotation = player1Prefab.transform.rotation;
			//player1 = Instantiate(player1Prefab) as GameObject;
			ship = player1;
		}
		else{
			player2.transform.position = player2Prefab.transform.position;
			player2.transform.rotation = player2Prefab.transform.rotation;
			ship = player2;
		}
		ship.transform.audio.Play();
		ship.GetComponent<PlayerShip>().invincible = true;
		yield return new WaitForSeconds(respawnTimer);
		if(ship != null)
			ship.GetComponent<PlayerShip>().invincible = false;
	}
	
	IEnumerator endRound(){
		yield return new WaitForSeconds(0.1f);
		GameManager.reg.ChangeState((int)GameState.SCORE, false);
	}
	
	void UpdateAsteroids(){
		asteroidTimer -= Time.deltaTime;
		if(asteroidTimer <= 0){
			asteroidTimer = asteroidTimerBase + asteroidTimerRandom*Random.value;
			Vector3 astPos = new Vector3(Random.Range(-22,22),
				asteroidPrefab.transform.position.y, 0);
			GameObject temp = Instantiate(asteroidPrefab, astPos, Quaternion.identity) as GameObject;
			temp.transform.rigidbody.AddForce(Vector3.up*asteroidSpeed, ForceMode.VelocityChange);
			temp.transform.renderer.material.color = new Color(Random.Range(0,1f),
				Random.Range(0,1f), Random.Range(0,1f));
			float tempScale = Random.Range(10f, 15f);
			temp.transform.localScale = new Vector3(tempScale, tempScale, tempScale);
			temp.transform.rigidbody.mass = tempScale*140;
		}
	}
	
	public void ChangeState(int newState, bool relative){
		
		stateChanged = true;
		
		ClearState();
		
		if(relative){
			state += newState;
			if(state > GameState.SCORE || state < GameState.TITLE)
				state = 0;
		}
		else
			state = (GameState)newState;
	}
	
	void ClearState(){
		
		if(state == GameState.TITLE)
			ResetScore();
		
		Destroy (level);
		Destroy (titleGUI);
		Destroy (ingameGUI);
		Destroy (player1);
		Destroy (player2);
		
		GameObject[] allGhosts = GameObject.FindGameObjectsWithTag("Ghost");
		GameObject[] allBullets = GameObject.FindGameObjectsWithTag("Bullet");
		
		foreach (var ghost in allGhosts){
			Destroy(ghost.gameObject);
		}
		foreach (var bullet in allBullets){
			Destroy(bullet.gameObject);
		}
		
	}
	
	void ResetScore(){
		player1Score = 0;
		player2Score = 0;
		winner = 0;
	}
	
	void HandleInputs(){
		if(Input.GetButtonUp("shoot1") || Input.GetButtonUp("shoot2")){
			if(state == GameState.TITLE){
				ChangeState(1, true);
			}
		}
		if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape) || 
			Input.GetButtonUp("start1")){
			ChangeState(0, false);
		}
		if(Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();
		}
	}
}

