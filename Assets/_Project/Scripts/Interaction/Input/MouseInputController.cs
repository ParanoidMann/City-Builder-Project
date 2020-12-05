using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Interaction.Input
{
    public class MouseInputController : AInputController
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private LayerMask _groundMask;

        private void Update()
        {
            CheckMovementEvents();
            CheckZoomEvents();
            CheckClickEvents();
        }

        private void CheckMovementEvents()
        {
            if (UnityEngine.Input.mousePosition.y >= Screen.height - 1)
            {
                InvokeMoveUp();
            }

            if (UnityEngine.Input.mousePosition.y <= 0)
            {
                InvokeMoveDown();
            }

            if (UnityEngine.Input.mousePosition.x >= Screen.width - 1)
            {
                InvokeMoveRight();
            }

            if (UnityEngine.Input.mousePosition.x <= 0)
            {
                InvokeMoveLeft();
            }
        }

        private void CheckZoomEvents()
        {
            if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                InvokeZoomIn();
            }

            if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                InvokeZoomOut();
            }
        }

        private void CheckClickEvents()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0)
                && EventSystem.current.IsPointerOverGameObject() == false)
            {
                InvokeClickDownAfterRaycast();
            }

            if (UnityEngine.Input.GetKey(KeyCode.Escape))
            {
                InvokeCancelClick();
            }
        }

        protected override Vector3Int? RaycastGround()
        {
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _groundMask))
            {
                return Vector3Int.RoundToInt(hit.point);
            }

            return null;
        }
    }
}