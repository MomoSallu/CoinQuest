using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerControls : MonoBehaviour { 
    private Animator animator;
    public Camera camTransform;
    public GameObject player;
    public float jumpStrength = 5f;
    public float fallMultipler = 2f;
    public float movementSpeed = 4f;
    public float gravityMultiplierY = Physics.gravity.y;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        camTransform = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");    
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        animator.SetFloat("moveX", horizontalInput);
        animator.SetFloat("moveY", verticalInput);


          if (Input.GetKey(KeyCode.Mouse0))
        {
            Quaternion cameraRotation = Camera.main.transform.rotation;
            transform.rotation = cameraRotation;
        }

        // refers to the Input Manager in Project Settings
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
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
    private void FixedUpdate()
    {
         if (rb.velocity.y < 0) {
            rb.velocity += gravityMultiplierY * fallMultipler * Time.deltaTime * Vector3.up;
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
