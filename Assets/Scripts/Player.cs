using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	// External scripts
	private UIHandler uiHandler;

	// Variables relating to player movement
	private Rigidbody rb;
	private InputAction moveAction, lookAction;
	private readonly float walkSpeed = 5f;
	private readonly float lookSpeed = 10f;

	// Player inventory
	private List<string> inventory = new List<string>();

	/*
	 * Start method
	 */
	private void Start() {
		uiHandler = GameObject.Find("EventSystem").GetComponent<UIHandler>();

		rb = GetComponent<Rigidbody>();
		moveAction = InputSystem.actions.FindAction("Player/Move");
		lookAction = InputSystem.actions.FindAction("Player/Look");
	}

	/*
	 * Update method - non-physics-related code should run in here
	 */
	private void Update() {
		Look();
	}
	
	/*
	 * FixedUpdate method - all player movement and similar physics-related
	 * code should be run from here
	 */
	private void FixedUpdate() {
		Move();
	}

	/*
	 * Look method - this is for turning the player left/right, and the camera up/down
	 */
	private void Look() {
		Vector2 inputVector = lookAction.ReadValue<Vector2>();

		float playerLookRot = transform.localRotation.eulerAngles.y + (lookSpeed * inputVector.x * Time.fixedDeltaTime);
		float cameraLookRot = Camera.main.transform.localRotation.eulerAngles.x + (lookSpeed * -inputVector.y * Time.fixedDeltaTime);

		transform.rotation = Quaternion.Euler(0f, playerLookRot, 0f);
		Camera.main.transform.localRotation = Quaternion.Euler(cameraLookRot, 0f, 0f);

		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out RaycastHit hit, 3f)) {
			if (!hit.collider.CompareTag("Untagged")) {
				uiHandler.SetHoverText(hit.collider.tag);
			} else {
				uiHandler.SetHoverText("");
			}
		} else {
			uiHandler.SetHoverText("");
		}
	}

	/*
	 * Move method - this is for moving the player
	 */
	private void Move() {
		Vector2 inputVector = moveAction.ReadValue<Vector2>();
		Vector3 moveVector = (transform.forward * inputVector.y) + (transform.right * inputVector.x);

		if (moveVector.magnitude > 0f) {
			rb.linearVelocity = walkSpeed * moveVector.normalized;
		} else {
			rb.linearVelocity = Vector3.zero;
		}
	}
}
