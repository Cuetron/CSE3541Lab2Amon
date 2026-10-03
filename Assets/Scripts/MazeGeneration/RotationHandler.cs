using amonq20;
using UnityEngine.InputSystem;

namespace amonq20
{
    public class RotationHandler
    {
        private readonly MazeRotationController rotationController;

        public RotationHandler(InputAction rotateAction, MazeRotationController rotationController)
        {
            this.rotationController = rotationController;
            rotateAction.performed += OnRotateToggled;
            rotateAction.Enable();
        }

        private void OnRotateToggled(InputAction.CallbackContext context)
        {
            rotationController.ToggleRotation();
        }
    }
}