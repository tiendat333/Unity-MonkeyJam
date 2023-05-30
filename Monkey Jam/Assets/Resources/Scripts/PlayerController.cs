using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region
    public static Transform instance;

    private void Awake()
    {
        instance = this.transform;
    }

    #endregion


    //VARIABLES
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    private bool isHitting;

    [SerializeField] private float jumpHeight;

    //REFERENCES  
    private CharacterController controller;
    private Animator anim;
    private PlayerStats stats;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        if (!stats.IsDead())
        {
            Move(); // Move() per frame

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (anim.GetLayerWeight(1) == 0 && anim.GetBool("isRoaring") == false && anim.GetBool("isAttacking") == false)
                {
                    StartCoroutine(Attack());
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (anim.GetLayerWeight(1) == 0 && anim.GetBool("isRoaring") == false && anim.GetBool("isBlocking") == false)
                {
                    StartCoroutine(Block());
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (anim.GetBool("isRoaring") == false)
                {
                    StartCoroutine(Roar());
                }
            }
        }

        
        
        
            
    }

    private void Move()
    {
        moveSpeed = 1;
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        // draw a sphere at player bottom with (position of player, radius size of the ground, layer that use to check if grounded or not)

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //Check if grounded then remove gravity
        }

        float moveZ = Input.GetAxis("Vertical"); //set moveZ = Input get from Vertical in Unity Asset
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX * moveSpeed, moveDirection.y, moveZ * moveSpeed); // if press W mean set moveDirection go foward 1 unit per second
        moveDirection = transform.TransformDirection(moveDirection); //use player foward direction
        
        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                if (anim.GetBool("isRoaring") == false && anim.GetBool("isBlocking") == false)
                    Run();
                else
                    Walk();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed; // multiple the modeDirection by moveSpeed base on idle/walk/run

            //if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }

        controller.Move(moveDirection * Time.deltaTime); // help moving with the same amount

        velocity.y += gravity * Time.deltaTime; //caculate gravity
        controller.Move(velocity * Time.deltaTime); //set gravity to player, need to * Time.deltaTime 2 time to caculate gravity
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    //private void Jump() { velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); }
        
    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");
        anim.SetBool("isAttacking", true);

        isHitting = false;
        yield return new WaitForSeconds(0.4f);

        isHitting = true;
        yield return new WaitForSeconds(0.4f);

        isHitting = false;
        yield return new WaitForSeconds(0.4f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        anim.SetBool("isAttacking", false);

    }

    void OnTriggerStay(Collider other)
    {
        if (anim.GetLayerWeight(1) != 0 && anim.GetBool("isRoaring") == false && anim.GetBool("isBlocking") == false && anim.GetBool("isAttacking") == true && isHitting == true)
        {
            if (other.gameObject.tag == "Enemy")
            {
                CharacterStats enemyStats = other.transform.GetComponent<CharacterStats>();
                enemyStats.TakeDamage(25);
            }
        }
        
    }

    private IEnumerator Block()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Block Layer"), 1);
        anim.SetTrigger("Block");
        anim.SetBool("isBlocking", true);

        yield return new WaitForSeconds(5.0f);     
        
        anim.SetTrigger("unBlock");

        yield return new WaitForSeconds(1.2f);
        anim.SetLayerWeight(anim.GetLayerIndex("Block Layer"), 0);
        anim.SetBool("isBlocking", false);
    }       

    private IEnumerator Roar()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Roar Layer"), 1);
        anim.SetTrigger("Roar");
        anim.SetBool("isRoaring", true);

        yield return new WaitForSeconds(5.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("Roar Layer"), 0);
        anim.SetBool("isRoaring", false);
    }

    private void GetReferences()
    {
        controller = GetComponent<CharacterController>(); //select Character Controller from Player
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<PlayerStats>();
    }

}
