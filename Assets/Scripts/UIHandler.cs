using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIHandler : MonoBehaviour {
	// External scripts
	private Player playerScript;

	// Inventory related elements
	private InputAction invAction;
	private bool invOpen = false;

	// UI elements
	public TextMeshProUGUI fpsDisplay, hoverText;
	public TextMeshProUGUI batteryDisplay; // NOTE! This is TEMPORARY and will be replaced with a custom graphic in future!
	public GameObject invPanel;

	// Timer
	private int timer = 0;

	/*
	 * Start method
	 */
	private void Start() {
		playerScript = GameObject.Find("Player").GetComponent<Player>();

		// Set target frame rate (TEMPORARY - will be replaced by a menu option in future!)
		Application.targetFrameRate = 165;

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
		// Some things don't need to update every frame, so limit them to once per second with this timer
		if (timer == Application.targetFrameRate) {
			ShowFPS();
			timer = 0;
		} else {
			++timer;
		}

		HandleBatteryDisplay();

		if (invAction.triggered) {
			HandleInventoryWindow();
		}
	}

	/*
	 * Handling of the FPS display
	 */
	private void ShowFPS() {
		fpsDisplay.text = $"FPS: {1f / Time.deltaTime:n2}";
	}

	/*
	 * Handling of the battery level displaye
	 * 
	 * NOTE! This WILL need to be modified in future!
	 * The text object WILL be replaced with a custom graphic!
	 */
	private void HandleBatteryDisplay() {
		batteryDisplay.text = $"Battery: {playerScript.GetBatteryLevel() * 100:n2}%";
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
