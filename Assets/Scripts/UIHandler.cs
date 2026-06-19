using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour {
	// UI elements
	public TextMeshProUGUI fpsDisplay;
	
	// Timer
	private int timer = 0;
	
	/*
	  * Start method - use this to ensure the UI is not destroyed between scenes
	  */
	private void Start() {
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(GameObject.Find("UI"));
	}
	
	/*
	  * Update method
	  */
	private void Update() {
		if (timer == 30) {
			timer = 0;
			fpsDisplay.text = $"FPS: {1f / Time.deltaTime:n2}";
		} else {
			++timer;
		}
	}
}
