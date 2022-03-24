using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rigid;
    private Vector3 change;
    public Joystick joystick;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = joystick.Horizontal;
        change.y = joystick.Vertical;
       
        if(change != Vector3.zero)
        {
            
            MovePlayer();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        }
    }
    void MovePlayer()
    {
        rigid.MovePosition(transform.position + change * speed*Time.deltaTime);
    }
}
