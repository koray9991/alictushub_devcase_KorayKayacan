using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Lean.Pool.LeanGameObjectPool enemyPool;
    public GameObject enemyPrefab;
    [SerializeField]
    LayerMask layerMask;
    public Transform player;
    public Transform spawnPoint;
    public Camera cam;
    public bool isSpawnPointInsideCam;
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetSpawnPoint();
        }
        //if (spawnPoint != null)
        //{
            
        //    Vector3 viewportPoint = cam.WorldToViewportPoint(spawnPoint.position);

           
        //    bool kameraTarafindanGoruluyor = (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
        //                                       viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
        //                                       viewportPoint.z > 0);

        //    if (kameraTarafindanGoruluyor)
        //    {
        //        isSpawnPointInsideCam = true;
                
        //    }
        //    else
        //    {
        //        isSpawnPointInsideCam = false;
        //    }
        //}





    }
    public void SpawnEnemy()
    {
        
        var enemy = Lean.Pool.LeanPool.Spawn(enemyPrefab);
        enemy.transform.position = spawnPoint.position;
        enemy.GetComponent<Enemy>().targetPlayer = player;
    }
    public void SetSpawnPoint()
    {
        spawnPoint.position = player.transform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        SpawnEnemy();
    }
}
