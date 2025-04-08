using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeScript.ThirdPersonCharacter
{
	[DefaultExecutionOrder(-2)]
	public class PlayerControllerInput : MonoBehaviour, PlayerControls.IPlayerControllerIMapActions
	{
		public PlayerControls PlayerControls { get; private set; }
		public Vector2 MovementInput { get; private set; }
		public Vector2 LookInput { get; private set; }

		private void OnEnable()
		{
			PlayerControls = new PlayerControls();
			PlayerControls.Enable();

			PlayerControls.PlayerControllerIMap.Enable();
			PlayerControls.PlayerControllerIMap.SetCallbacks(this);
		}
		private void OnDisable()
		{
			PlayerControls.PlayerControllerIMap.Disable();
			PlayerControls.PlayerControllerIMap.RemoveCallbacks(this);
		}
		public void OnMovement(InputAction.CallbackContext context)
		{
			MovementInput = context.ReadValue<Vector2>();
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			LookInput = context.ReadValue<Vector2>();
		}
	}
}

