using UnityEngine;

namespace _Project.Scripts.Interaction
{
    public class CameraMover : MonoBehaviour
    {
        [Header("Transform")]
        [SerializeField]
        private Transform _root;

        [SerializeField]
        private Transform _cameraTransform;

        [Header("Movement Stats")]
        [SerializeField]
        private float _movementSpeed = 1.0f;

        [SerializeField]
        private float _movementTime = 1.0f;

        [Header("Zoom Stats")]
        [SerializeField]
        private Vector3 _zoomAmount;

        private Vector3 _newPosition;
        private Vector3 _newZoom;

        private void OnValidate()
        {
            if (_root == null)
            {
                _root = GetComponent<Transform>();
            }
        }

        private void Start()
        {
            _newPosition = _root.position;
            _newZoom = _cameraTransform.localPosition;
        }

        private void Update()
        {
            HandlerMovementInput();
        }

        private void HandlerMovementInput()
        {
            var deltaTime = Time.deltaTime * _movementTime;

            _root.position = Vector3.Lerp(_root.position, _newPosition, deltaTime);
            _cameraTransform.localPosition = Vector3.Lerp(_cameraTransform.localPosition, _newZoom, deltaTime);
        }

        public void OnMoveUp()
        {
            _newPosition += _root.forward * _movementSpeed;
        }

        public void OnMoveDown()
        {
            _newPosition += _root.forward * -_movementSpeed;
        }

        public void OnMoveRight()
        {
            _newPosition += _root.right * _movementSpeed;
        }

        public void OnMoveLeft()
        {
            _newPosition += _root.right * -_movementSpeed;
        }
        
        public void OnZoomIn()
        {
            _newZoom += _zoomAmount;
        }
        
        public void OnZoomOut()
        {
            _newZoom -= _zoomAmount;
        }
    }
}