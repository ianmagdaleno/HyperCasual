using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference joystickToMove;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;

    [Space(10),Header("Player Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float rangeToAttack;

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

        if(collision.gameObject.CompareTag("Enemy")) 
        {
            //collision.gameObject.GetComponent<Animator>().enabled = false;
            //collision.gameObject.GetComponent<Ragdoll>().ActivateRagdoll();
            collision.gameObject.GetComponent<NPC>().TakeHit();
            animator.SetTrigger("isAttack");
        }
    }
}