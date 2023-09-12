using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject rangeObject;
    [SerializeField] GameObject handBoomerang;
    public GameObject closestTarget;
    [SerializeField] float range;
    [SerializeField] Transform boomerangParent;
    public List<GameObject> boomerangPool;
    int boomerangNumber;
    bool canAttack;
    bool enemyIsInRange;
    [SerializeField] float fireRate;
   
    
    void Start()
    {
        rangeObject.transform.localScale = new Vector3(range * 2, rangeObject.transform.localScale.y, range * 2);
        canAttack = true;
        for (int i = 0; i < boomerangParent.childCount; i++)
        {
            boomerangPool.Add(boomerangParent.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        FindClosestEnemy();
        AttackState();
    }
    public void AttackState()
    {
        if (canAttack && enemyIsInRange)
        {
            var target = closestTarget;
            if (closestTarget.GetComponent<Enemy>().isDead)
            {
                return;
            }
            handBoomerang.SetActive(false);
            canAttack = false;
            StartCoroutine(FireRateControl());
            var currentBoomerang = boomerangPool[boomerangNumber];
            currentBoomerang.SetActive(true);
            var boomerangChild = currentBoomerang.transform.GetChild(0);
            boomerangChild.DOLocalJump(Vector3.zero, Random.Range(-5f, 5f), 1, 0.5f);
           
            currentBoomerang.transform.DOJump(closestTarget.GetComponent<Enemy>().boomerangTarget.position, Random.Range(1f, 4f), 1, 0.5f).OnComplete(() => {
                currentBoomerang.SetActive(false);
                currentBoomerang.transform.position = boomerangParent.transform.position;
                target.GetComponent<Enemy>().Death();
                GameManager.instance.KillCountChange();
                
            });
            boomerangNumber += 1;
            if (boomerangNumber == boomerangPool.Count)
            {
                boomerangNumber = 0;
            }
        }
    }


    IEnumerator FireRateControl()
    {
        yield return new WaitForSeconds(fireRate);
        canAttack = true;
        handBoomerang.SetActive(true);
    }
  
    void FindClosestEnemy()
    {
        float distanceClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        List<Enemy> allEnemies = new List<Enemy>();
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            if (!enemy.GetComponent<Enemy>().isDead)
            {
                allEnemies.Add(enemy);
            }

        }
        if (allEnemies.Count != 0)
        {
            foreach (Enemy currentEnemy in allEnemies)
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceClosestEnemy)
                {
                    distanceClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                    closestTarget = closestEnemy.gameObject;

                    if (Vector3.Distance(closestTarget.transform.position, transform.position) < range)
                    {
                        enemyIsInRange = true;
                    }
                    else
                    {
                        enemyIsInRange = false;
                    }
                }
            }
        }
        



    }
}
