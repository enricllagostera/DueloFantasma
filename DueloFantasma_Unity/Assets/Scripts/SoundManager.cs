using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public static SoundManager reg;
	
	public AudioClip musTheme;
	public AudioClip musPlay;
	public AudioClip musShowdown;
	public AudioClip musResult;
	
	
	// Use this for initialization
	void Awake () {
		reg = this;
	}
}
