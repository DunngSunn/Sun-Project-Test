using UnityEngine;
using UnityEngine.InputSystem;

namespace NewInputByReference
{
    public static class NewInput
    {
        private static PlayerInput _playerInput;
        
        public static void SetPlayerInput(PlayerInput newPlayerInput) => _playerInput = newPlayerInput;

        #region Action Map Functions
        
        /// <summary>
        /// Change the current Action Map to the one indicated by "actionMap".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void SwitchActionMap(string actionMap)
        {
            _playerInput.SwitchCurrentActionMap(actionMap);
        }
        
        /// <summary>
        /// Enable the Action Map indicated by "actionMap". Other Action Maps will not be disabled if they are currently active".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void EnableActionMap(string actionMap)
        {
            _playerInput.actions.FindActionMap(actionMap).Enable();
        }
        
        /// <summary>
        /// Disable the Action Map indicated by "actionMap". Other Action Maps will not be disabled if they are currently active".
        /// </summary>
        /// <param name="actionMap">Can be found in the Input Action Asset, under the "Action Maps" column.</param>
        public static void DisableActionMap(string actionMap)
        {
            _playerInput.actions.FindActionMap(actionMap).Disable();
        }
        
        #endregion
        
        #region Action Name Functions

        /// <summary>
        /// Returns true if the user pressed the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButtonDown(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            if (!CheckInputActionPhase(inputAction))
                return false;
            
            bool buttonClicked = inputAction.triggered && inputAction.ReadValue<float>() > 0;
            return buttonClicked; 
        }
        
        /// <summary>
        /// Returns true if the user had pressed and did not release the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButton(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            return CheckInputActionPhase(inputAction);
        }
        
        /// <summary>
        /// Returns true if the user released the virtual Button indicated by "actionName" during the current frame.
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Button)</param>
        public static bool GetButtonUp(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            
            bool buttonReleased = inputAction.triggered && inputAction.ReadValue<float>() <= 0;
            return buttonReleased; 
        }
        
        /// <summary>
        /// Returns the value of the virtual Axis indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Axis)</param>
        public static float GetAxis(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<float>() : 0f; 
        }

        /// <summary>
        /// Returns the value of the virtual Vector2 indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Vector 2)</param>
        public static Vector2 GetVector2(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<Vector2>() : Vector2.zero; 
        }
        
        /// <summary>
        /// Returns the value of the virtual Vector3 indicated by "actionName".
        /// </summary>
        /// <param name="actionName">Can be found in the Input Action Asset, under the "Actions" column. (Action Type = Value, Control Type = Vector 3)</param>
        public static Vector3 GetVector3(string actionName)
        {
            var inputAction = _playerInput.actions[actionName];
            return CheckInputActionPhase(inputAction) ? inputAction.ReadValue<Vector3>() : Vector3.zero; 
        }
        
        #endregion
        
        private static bool CheckInputActionPhase(InputAction inputAction)
        {
            var inputActionPhase = inputAction.phase;
            return inputActionPhase != InputActionPhase.Disabled && inputActionPhase != InputActionPhase.Waiting;
        }
    }
}