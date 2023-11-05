using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Animator animator;
    public float jumpStrength = 5f;
    public float movementSpeed = 4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        animator.SetFloat("moveX", horizontalInput);
        animator.SetFloat("moveY", verticalInput);

         // refers to the Input Manager in Project Settings
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.y);
        }
        
        if (!IsGrounded() && rb.velocity.y > 0) {
            animator.SetBool("isJumping", true);
        } else if(IsGrounded() && rb.velocity.y < 0){
            animator.SetBool("isJumping", false);
        }
        if (!IsGrounded() && rb.velocity.y < 0) {
            animator.SetBool("isFalling", true);
        } else {
            animator.SetBool("isFalling", false);
        }
    }
    bool IsGrounded() {
        bool grounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);
        if (grounded)
        {
            animator.SetBool("hasFinishedJumping", true);
        }
        else { animator.SetBool("hasFinishedJumping", false); }
        animator.SetBool("isGrounded", grounded);
        return grounded;
     }
}
