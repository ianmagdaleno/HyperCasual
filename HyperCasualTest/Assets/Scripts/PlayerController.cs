using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickToMove;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;

    private void FixedUpdate()
    {
        Vector2 moveDir = joystickToMove.action.ReadValue<Vector2>();

        if (moveDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(moveDir.x, moveDir.y) * Mathf.Rad2Deg;

            player.transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            animator.SetFloat("speed", moveDir.magnitude);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
    }
}