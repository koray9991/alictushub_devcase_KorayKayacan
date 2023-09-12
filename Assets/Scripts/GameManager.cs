using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Lean.Pool.LeanGameObjectPool enemyPool;
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        enemyPool.Spawn(transform.position+new Vector3(Random.Range(-5f,5f),0,Random.Range(-5f,5f)));
    }
}
