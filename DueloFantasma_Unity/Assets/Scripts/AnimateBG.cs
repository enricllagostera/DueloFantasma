using UnityEngine;
using System.Collections;

public class AnimateBG : MonoBehaviour {

	public float scrollSpeed = 0.5f;
	
	void Update () {
		
		float offset = Time.time * scrollSpeed;
		
		transform.renderer.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
	}
}
