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
    [SerializeField] private Material playerMaterial;

    private BackPack backPack;
    private GameObject currentTarget;
    private Rigidbody currentTargetRigidbody;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        backPack = GetComponent<BackPack>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
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
            currentTargetRigidbody = currentTarget.GetComponent<Rigidbody>();
            animator.SetTrigger("isAttack");
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.position = transform.position;
        }
    }
    public void AttackEffect()
    {
        if (currentTargetRigidbody != null)
        {
            currentTargetRigidbody.isKinematic = true;
            currentTargetRigidbody.useGravity = false;
            currentTarget.GetComponent<CapsuleCollider>().enabled = false;

            Vector3 direction = transform.position - currentTarget.transform.position;
            currentTarget.GetComponent<NPC>().TakeHit(direction);
        }
    }
    public void LevelUp()
    {
        backPack.UpgradeMax(2);

        if (playerMaterial != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            playerMaterial.color = randomColor;
        }
    }
}