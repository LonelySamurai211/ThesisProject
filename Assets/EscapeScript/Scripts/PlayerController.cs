using System;
using UnityEngine;

namespace EscapeScript.ThirdPersonCharacter
{
	[DefaultExecutionOrder(-1)]
	public class PlayerController : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField] private CharacterController characterController;
		[SerializeField] private Camera playerCamera;

		[Header("Base Movement Settings")]
		public float runAcceleration = 0.25f;
		public float runSpeed = 4f;
		public float drag = 0.1f;

		[Header("Camera Settings")]
		public float lookSenseH = 0.1f;
		public float lookSenseV = 0.1f;
		public float lookLimitV = 89f;

		private PlayerControllerInput playerControllerInput;
		private Vector2 cameraRotation = Vector2.zero;
		private Vector2 playerTargetRotation = Vector2.zero;

		private void Awake()
		{
			playerControllerInput = GetComponent<PlayerControllerInput>();
		}
		private void Update()
		{
			Vector3 cameraForwardXZ = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z).normalized;
			Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;
			Vector3 movementDirection = cameraRightXZ * playerControllerInput.MovementInput.x + cameraForwardXZ * playerControllerInput.MovementInput.y;

			Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
			Vector3 newVelocity = characterController.velocity + movementDelta;

			//Add drag
			Vector3 currentDrag = newVelocity.normalized * drag *Time.deltaTime;
			newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
			newVelocity = Vector3.ClampMagnitude(newVelocity,runSpeed);

			//movement once per tick
			characterController.Move(newVelocity * Time.deltaTime);

		}

		private void LateUpdate()
		{
			cameraRotation.x += lookSenseH * playerControllerInput.LookInput.x;
			cameraRotation.y = Math.Clamp(cameraRotation.y - lookSenseV * playerControllerInput.LookInput.y, -lookLimitV, lookLimitV);

			playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * playerControllerInput.LookInput.x;
			transform.rotation = Quaternion.Euler(0f, playerTargetRotation.x, 0f);

			playerCamera.transform.rotation = Quaternion.Euler(cameraRotation.y,cameraRotation.x, 0f);
		}
	}
}

