using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    // Animation Hash
    public readonly static int ANISTS_Stand = Animator.StringToHash("Base Layer.Stand");
    public readonly static int ANISTS_Idle_A = Animator.StringToHash("Base Layer.Idle_A");
    public readonly static int ANISTS_Idle_B = Animator.StringToHash("Base Layer.Idle_B");
    public readonly static int ANISTS_Walk_Front = Animator.StringToHash("Base Layer.Walk_Front");
    public readonly static int ANISTS_Walk_Right = Animator.StringToHash("Base Layer.Walk_Right");
    public readonly static int ANISTS_Walk_Left = Animator.StringToHash("Base Layer.Walk_Left");
    public readonly static int ANISTS_Run_Front = Animator.StringToHash("Base Layer.Run_Front");
    public readonly static int ANISTS_Run_Left = Animator.StringToHash("Base Layer.Run_Right");
    public readonly static int ANISTS_Run_Right = Animator.StringToHash("Base Layer.Run_Left");

    Animator animator;
    CharacterController controller;
    float _time = 0;

    public float moveSpeed = 0;
    public float turnSpeed = 0;
    public float jumpPower = 0;
    public float gravity = 0;

    Vector3 moveDirection = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpPower;

            if(Input.GetAxis("Vertical")!= 0)
            {
                animator.SetFloat("DoRun", Input.GetAxis("Vertical"));
            }
        }

        this.transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if(_time >= 15f)
        {
            animator.SetTrigger("DoStand");
        }
    }

    public void SupportStand()
    {
        _time = 0;
    }
}
