using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class UIHandler : MonoBehaviour {
	// Inventory related elements
	private InputAction invAction;
	private bool invOpen = false;

	// UI elements
	public TextMeshProUGUI fpsDisplay, hoverText;
	public GameObject invPanel;
	
	// Timer
	private int timer = 0;
	
	/*
	  * Start method
	  */
	private void Start() {
		// Ensure the EventSystem object and UI are preserved between scenes
		DontDestroyOnLoad(this);
		DontDestroyOnLoad(GameObject.Find("UI"));

		invAction = InputSystem.actions.FindAction("Player/Inventory");
		invPanel.transform.position = new Vector3(-960f, 540f, 0f);

		// Lock the cursor to the game window, and make it invisible
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	/*
	  * Update method
	  */
	private void Update() {
		ShowFPS();

		if (invAction.triggered) {
			HandleInventoryWindow();
		}
	}

	/*
	 * Handling of the FPS display
	 */
	private void ShowFPS() {
		if (timer == 30) {
			timer = 0;
			fpsDisplay.text = $"FPS: {1f / Time.deltaTime:n2}";
		} else {
			++timer;
		}
	}

	/*
	 * Handling the inventory window
	 */
	private void HandleInventoryWindow() {
		if (invOpen) {
			invPanel.transform.position = new Vector3(-960f, 540f, 0f);
		} else {
			invPanel.transform.position = new Vector3(960f, 540f, 0f);
		}

		invOpen = !invOpen;
	}

	/*
	 * Change the value of the hover text
	 */
	public void SetHoverText(string text) {
		hoverText.text = text;
	}
}
