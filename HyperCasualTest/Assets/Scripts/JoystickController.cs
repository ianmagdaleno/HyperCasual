using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{

    public float moveSpeed = 15f;
    private float _turnSpeed = 10f;
    public Transform playerModel, cameraTransform;
    private Animator _animator;
    private Vector3 _mouseDelta, _lastMousePosition, _difference, _dummyJoystick, _differenceClamped;
    private Vector3 _rawInputMovement, _smoothInputMovement, _movement;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = playerModel.GetComponent<Animator>();
    }

    void Update()
    {
        DoJoystick();
        DoMovement();
        TurnPlayer();
    }
    private void DoJoystick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = (Input.mousePosition / Screen.width);
        }
        if (Input.GetMouseButton(0))
        {
            _mouseDelta = (Input.mousePosition / Screen.width) - _lastMousePosition;
            _difference += (_mouseDelta.normalized * _mouseDelta.magnitude * 0.5f);
            _differenceClamped = Vector3.ClampMagnitude(_difference, 0.06f);
            _lastMousePosition = (Input.mousePosition / Screen.width);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _mouseDelta = Vector3.zero;
            _difference = Vector3.zero;
            _differenceClamped = Vector3.zero;
        }

        _rawInputMovement = new Vector3(_differenceClamped.x, 0, _differenceClamped.y);
        _smoothInputMovement = Vector3.Lerp(_smoothInputMovement, _rawInputMovement, Time.deltaTime * 20f);
        _movement = _smoothInputMovement * moveSpeed * Time.deltaTime * 10f;
    }
    private void DoMovement()
    {
        transform.Translate(_movement);
        _animator.SetFloat("Speed", _differenceClamped.magnitude);
    }

    private void TurnPlayer()
    {
        if (_smoothInputMovement.sqrMagnitude > 0.000001f)
        {
            Quaternion rotation = Quaternion.Slerp(playerModel.rotation, Quaternion.LookRotation(CameraDirection(_smoothInputMovement)), _turnSpeed);
            Vector3 eulerRotation = rotation.eulerAngles;
            playerModel.eulerAngles = eulerRotation;
        }
    }
    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = cameraTransform.forward;
        var cameraRight = cameraTransform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;
    }
}
