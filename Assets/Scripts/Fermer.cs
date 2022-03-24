using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fermer : Enemy
{
    private Rigidbody2D rig;
    public Transform target;
    public Animator anim;

    bool isMove = true;

    private NavMeshAgent agent;

    [SerializeField]
    Transform[] waypoints;

    int waypointIndex = 0;

    public float chaseRadius;
    public float attackRadius;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateUpAxis = false;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        agent.transform.position = waypoints[waypointIndex].transform.position;
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            isMove = false;
            anim.SetBool("isAngry", true);
            CheckDistance();
            
        }

        if (collision.CompareTag("Bomb"))
        {
            StartCoroutine(Explode());
           
            moveSpeed = 0f;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isMove = true;
        anim.SetBool("isAngry", false);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        
    }


    void Move()
    {
        if (isMove)
        {
            
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            Vector3 temp = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            changeAnim(temp - transform.position);
            if (agent.transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

            if (waypointIndex == waypoints.Length)
                waypointIndex = 0;
        }
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            moveSpeed = 2f;
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);
            changeAnim(temp - transform.position);
            rig.MovePosition(temp);
                
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void changeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x)> Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x)  < Mathf.Abs(direction.y)) {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }

        }
    }

    IEnumerator Explode()
    {
        

        yield return new WaitForSeconds(3);
      
        anim.SetBool("bombed", true);

        moveSpeed = 0f;
        yield return new WaitForSeconds(2);

        Destroy(gameObject);


    }


}
