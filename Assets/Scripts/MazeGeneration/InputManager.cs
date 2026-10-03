using UnityEngine;

namespace amonq20
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private MazeRotationController mazeRotationController;
        private InputSystem_Actions inputScheme;

        private void Awake()
        {
            inputScheme = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            var rotationHandler = new RotationHandler(inputScheme.Player.Rotate, mazeRotationController);
            inputScheme.Enable();
        }
    }
}