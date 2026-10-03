using UnityEngine;

namespace amonq20
{
    public class MazeRotationController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 20f;
        private Vector3 pivotPoint;
        private bool isRotating = false;

        public void SetPivot(Vector3 pivot)
        {
            pivotPoint = pivot;
        }

        public void ToggleRotation()
        {
            isRotating = !isRotating;
        }

        private void Update()
        {
            if (isRotating)
            {
                transform.RotateAround(pivotPoint, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
    }
}