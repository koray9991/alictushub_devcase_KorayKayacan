using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public Transform boomerangTarget;
    public Transform targetPlayer;
    [HideInInspector] public NavMeshAgent navmesh;
    [HideInInspector] public Animator anim;

    [HideInInspector] public bool isInRange;
    [SerializeField] float range;
    [SerializeField] float moveSpeed;
    public Transform bulletSpawnPos;
    public GameObject bullet;
    [HideInInspector] public bool isDead;
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
        bullet.transform.parent = GameManager.instance.bulletParent;
       
    }
   
    private void Update()
    {
        RangeControl();
       
        
    }
    public void RangeControl()
    {
        if (Vector3.Distance(targetPlayer.position, transform.position) > range)
        {
            Walk();
        }
        else
        {
            Attack();
        }
    }

    public void Walk()
    {
        if (animState != AnimationState.walk)
        {
            animState = AnimationState.walk;
            anim.SetTrigger("Walk");
            navmesh.isStopped = false;

        }
        navmesh.SetDestination(targetPlayer.position);
    }
    public void Attack()
    {
        if (animState != AnimationState.attack)
        {
            animState = AnimationState.attack;
            anim.SetTrigger("Attack");
            navmesh.isStopped = true;
        }
        var lookPos = targetPlayer.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20);
    }
    public void SkeletonThrowing()
    {
        bullet.GetComponent<TrailRenderer>().time = 0;
        bullet.transform.position = bulletSpawnPos.position;
        var Vector = targetPlayer.position - bulletSpawnPos.position;
        Vector.y = 0;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().direction = Vector;
        DOVirtual.DelayedCall(0.2f, () =>
        {
            bullet.GetComponent<TrailRenderer>().time = 0.2f;
        });
       
    }
    public void Death()
    {
        isDead = true;
        anim.SetTrigger("Death");
        if(targetPlayer.GetComponent<PlayerAttack>().closestTarget == transform)
        {
            targetPlayer.GetComponent<PlayerAttack>().closestTarget = null;
        }
        this.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        DOVirtual.DelayedCall(3, () =>
        {
            Lean.Pool.LeanPool.Despawn(gameObject);
        });
    }
}
