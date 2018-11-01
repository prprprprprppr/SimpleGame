using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private float lookRadius;
    [SerializeField]
    private float RotateSpeed;

    private NavMeshAgent agent;
    private Animator anim;
    private EnemyController controller;
    private Vector3 targetPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        targetPos = player.transform.position;
        float distance = Vector3.Distance(targetPos, transform.position);
        if (distance < lookRadius)
        {
            agent.SetDestination(targetPos);
            if (distance <= agent.stoppingDistance)
            {
                faceTarget();
                controller.canAttack = true;
            }
            else
            {
                controller.canAttack = false;
            }
        }
        float speedPer = agent.velocity.magnitude / agent.speed;
        anim.SetFloat("speed", speedPer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }

    private void faceTarget()
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotateSpeed);
    }

    //不加会报错..
    public void Hit()
    {
    }

    public void Shoot()
    {
    }

    public void FootR()
    {
    }

    public void FootL()
    {
    }

    public void Jump()
    {
    }

    public void Land()
    {
    }
}
