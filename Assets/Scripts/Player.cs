using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
	// Variables relating to player movement
	private InputAction moveAction, lookAction;
	private readonly float walkSpeed = 5f;
	private readonly float lookSpeed = 75f;

	/*
	 * Start method - initialize any non-readonly variables in here
	 */
	private void Start() {
		moveAction = InputSystem.actions.FindAction("Player/Move");
		lookAction = InputSystem.actions.FindAction("Player/Look");
	}
	
	/*
	 * FixedUpdate method - all player movement and similar physics-related
	 * code should be run from here
	 */
	private void FixedUpdate() {
		Look();
		Move();
	}

	/*
	 * Look method - this is for turning the player left/right, and the camera up/down
	 */
	private void Look() {
		Vector2 lookVector = lookAction.ReadValue<Vector2>();

		float playerLookRot = transform.localRotation.eulerAngles.y + (lookSpeed * lookVector.x * Time.fixedDeltaTime);
		float cameraLookRot = Camera.main.transform.localRotation.eulerAngles.x + (lookSpeed * -lookVector.y * Time.fixedDeltaTime);

		transform.localRotation = Quaternion.Euler(0f, playerLookRot, 0f);
		Camera.main.transform.localRotation = Quaternion.Euler(cameraLookRot, 0f, 0f);
	}

	/*
	 * Move method - this is for moving the player
	 */
	private void Move() {
		Vector2 moveVector = moveAction.ReadValue<Vector2>();

		float newX = transform.position.x + (walkSpeed * moveVector.x * Time.fixedDeltaTime);
		float newZ = transform.position.z + (walkSpeed * moveVector.y * Time.fixedDeltaTime);

		transform.position = new Vector3(newX, 1f, newZ);
	}
}
