using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayerMovement : MonoBehaviour
{
    private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float horizontalSpeed;

    bool isRunning = false;
    private Animator animator;
    private Vector2 input;
    [SerializeField]  Rigidbody rigidbody;

    private bool jump = false, isGrounded = true;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float _jumpStrength = 8f;
    //bool canFall = true;
    [SerializeField] Camera cam;
    [SerializeField] int fovValue;
    int aiming = 0;

    //public float velocity;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("Aiming", true);
            aiming = 1;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("Aiming", false);
            aiming = 2;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("Aiming", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("Aiming", false);
        }

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
            animator.SetBool("OnGround", false);
            animator.SetBool("Roll", true);
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            //rigidbody.AddForce(Vector2.up * _jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        Vector3 desireRotation = new Vector3(0, input.x * horizontalSpeed * Time.deltaTime, 0);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(desireRotation));

        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);

        Vector3 movement = new Vector3(transform.forward.x * input.y * speed * Time.deltaTime, 0, transform.forward.z * input.y * speed * Time.deltaTime);
        rigidbody.MovePosition(transform.position + movement);

        if (aiming == 1)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue - fovValue * 40 / 100, 2f);
        }
        else if (aiming == 2)
        {
            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 2f);
            if (cam.fieldOfView == fovValue)
            {
                aiming = 0;
            }
        }
        else
        {
            if (input.sqrMagnitude != 0)
            {
                animator.SetFloat("Speed", speed);
                if (isRunning == true)
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 20 / 100, 0.5f);
                }
                else
                {
                    cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue + fovValue * 10 / 100, 0.5f);
                }
            }
            else
            {
                animator.SetFloat("Speed", 0);
                cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, fovValue, 0.5f);
            }
        }



        /*if (rigidbody.velocity.y > 0)
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
        }*/
        //velocity = rigidbody.velocity.y;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
            animator.SetBool("OnGround", true);
            //animator.SetBool("Jump", false);
            //animator.SetBool("Fall", false);
        }
    }

    public void ResetAttack()
    {
        //animator.SetBool("Attack", false);
    }
}
