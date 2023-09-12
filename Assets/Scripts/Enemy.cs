using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Transform boomerangTarget;
    public Transform targetPlayer;
    public NavMeshAgent navmesh;
    public Animator anim;

    public bool isInRange;
    [SerializeField] float range;
    public float moveSpeed;
    public Transform bulletSpawnPos;
    public GameObject bullet;
    enum AnimationState
    {
        walk,
        attack,
        death
    }
    AnimationState animState;
    private void Start()
    {
        anim = GetComponent<Animator>();
        navmesh = GetComponent<NavMeshAgent>();
        navmesh.SetDestination(targetPlayer.position);

    }
    private void Update()
    {

        if (Vector3.Distance(targetPlayer.position, transform.position) > range)
        {
            if (animState != AnimationState.walk)
            {
                animState = AnimationState.walk;
                anim.SetTrigger("Walk");
                navmesh.isStopped = false;
               
            }
            navmesh.SetDestination(targetPlayer.position);
        }
        else
        {
            if (animState != AnimationState.attack)
            {
                animState = AnimationState.attack;
                anim.SetTrigger("Attack");
                navmesh.isStopped = true;
            }
        }
        
    }
    public void SkeletonThrowing()
    {
        bullet.transform.position = bulletSpawnPos.position;
        var Vector = targetPlayer.position - transform.position;
        Vector.y = 0;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().direction = Vector;
    }
    
}
