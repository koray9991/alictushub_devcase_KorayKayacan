using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Lean.Pool.LeanGameObjectPool enemyPool;
    public GameObject enemyPrefab;

    public Lean.Pool.LeanGameObjectPool coinPool;
    public GameObject coinPrefab;


    public Transform player;
    public Transform spawnPoint;
    public Camera cam;
    public bool isSpawnPointInsideCam;
    public Transform bulletParent;

    int coinCount;
    public TextMeshProUGUI coinText;
    int killCount;
    public TextMeshProUGUI killText;
    
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        coinCount = PlayerPrefs.GetInt("coinCount");
        SetCoin(0);
        SpawnCoins();
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

    public void SpawnCoins()
    {
        for (int i = 0; i < coinPool.Capacity; i++)
        {
            var coin = Lean.Pool.LeanPool.Spawn(coinPrefab);
            coin.transform.position = new Vector3(Random.Range(-100f, 100f), 0, Random.Range(-100f, 100f));
            coin.transform.parent = coinPool.transform;
        }
    
    }
    public void SpawnEnemy()
    {
        
        if (enemyPool.Spawned < enemyPool.Capacity)
        {
            var enemy = Lean.Pool.LeanPool.Spawn(enemyPrefab);
            enemy.GetComponent<Enemy>().enabled = true;
            enemy.GetComponent<Enemy>().isDead = false;
            enemy.transform.position = spawnPoint.position;
            enemy.GetComponent<Enemy>().targetPlayer = player;
            enemy.transform.parent = enemyPool.transform;
        }
       

    }
    public void SetCoin(int value)
    {
        coinCount += value;
        coinText.text = coinCount.ToString();
        PlayerPrefs.SetInt("coinCount", coinCount);
    }
    public void KillCountChange()
    {
        killCount += 1;
        killText.text = killCount.ToString();
    }
    public void SetSpawnPoint()
    {
        spawnPoint.position = player.transform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        SpawnEnemy();
    }
}
