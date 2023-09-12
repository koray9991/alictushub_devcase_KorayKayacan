using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject rangeObject;
    public float range;
    public Transform boomerangParent;
    public List<GameObject> boomerangPool;
    int boomerangNumber;
    bool canAttack;
    public bool enemyIsInRange;
    [SerializeField] float fireRate;
    public GameObject closestTarget;
    [SerializeField] GameObject handBoomerang;
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
       

        if (canAttack && enemyIsInRange)
        {
            handBoomerang.SetActive(false);
            canAttack = false;
            StartCoroutine(FireRateControl());
            var currentBoomerang = boomerangPool[boomerangNumber];
            currentBoomerang.SetActive(true);
            var boomerangChild = currentBoomerang.transform.GetChild(0);
            boomerangChild.DOLocalJump(Vector3.zero, Random.Range(2f, 7f), 1, 0.5f);
            currentBoomerang.transform.DOJump(closestTarget.GetComponent<Enemy>().boomerangTarget.position, Random.Range(2f,7f), 1, 0.5f).OnComplete(() => {
                currentBoomerang.SetActive(false);
                currentBoomerang.transform.position = boomerangParent.transform.position;
            });
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
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();
        if (allEnemies.Length != 0)
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
