using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickToMove;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;

    [Space(10), Header("Player Attributes")]
    [SerializeField] private float speed;

    private GameObject currentTarget;

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 moveDirection = joystickToMove.action.ReadValue<Vector2>();

        if (moveDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

            player.transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            animator.SetFloat("speed", moveDirection.magnitude);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentTarget = collision.gameObject;
            animator.SetTrigger("isAttack");
        }
    }
    public void AttackEffect()
    {
        currentTarget.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        currentTarget.gameObject.GetComponent<Rigidbody>().useGravity = false;
        currentTarget.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        Vector3 direction = transform.position - currentTarget.transform.position;
        currentTarget.gameObject.GetComponent<NPC>().TakeHit(direction);
    }
}