using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour {
	// UI elements
	public TextMeshProUGUI fpsDisplay;
	
	// Timer
	private int timer = 0;
	
	/*
	  * Start method
	  */
	private void Start() {
		// Ensure the EventSystem object and UI are preserved between scenes
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(GameObject.Find("UI"));

		// Lock the cursor to the game window, and make it invisible
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
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
