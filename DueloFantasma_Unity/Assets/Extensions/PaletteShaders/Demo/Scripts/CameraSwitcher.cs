using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Palette Shaders/Demo/Camera Switcher")]
public class CameraSwitcher : MonoBehaviour {

	public List<Camera> Cameras;

	void Update() {
		bool useNext = false;
		if (Input.GetKeyDown(KeyCode.Space))
			for (int i = 0; i < Cameras.Count * 2; i++) {
				Camera camera = Cameras[i % Cameras.Count];
				if (useNext) {
					camera.enabled = true;
					return;
				}
				if (camera == Camera.mainCamera) {
					useNext = true;
					camera.enabled = false;
				}
			}

	}

}
