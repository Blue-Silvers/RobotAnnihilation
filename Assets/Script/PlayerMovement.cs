using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private BoxCollider swordCollid;

    bool isRunning = false;
    private Animator animator;
    private Vector2 input;
    Rigidbody rigidbody;

    private bool jump = false, isGrounded = true;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float _jumpStrength = 8f;
    bool canFall = true;
    [SerializeField] Camera cam;
    [SerializeField] int fovValue;

    public float velocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            isRunning = true;
            
        }
        else
        {
            speed = walkSpeed;
            isRunning = false;
        }

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            isGrounded = false;
            animator.SetBool("Jump", true);
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rigidbody.AddForce(Vector2.up * _jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        Vector3 desireRotation = new Vector3(0, input.x* horizontalSpeed * Time.deltaTime, 0);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(desireRotation));

        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + movement);


        if (input.sqrMagnitude != 0)
        {
            animator.SetFloat("Speed", speed);
            if (isRunning == true)
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + 10, 0.5f);
            }
            else
            {
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + 5, 0.5f);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 0.5f);
        }


        if (rigidbody.velocity.y > 0)
        {
            canFall = true;

        }
        if (isGrounded)
        {
            canFall = false;
        }


        if (rigidbody.velocity.y < 0 && canFall == true)
        {
            animator.SetBool("Fall", true);
        }
        velocity = rigidbody.velocity.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }

    public void ResetAttack()
    {
        animator.SetBool("Attack", false);
        swordCollid.enabled = false;
    }
}
