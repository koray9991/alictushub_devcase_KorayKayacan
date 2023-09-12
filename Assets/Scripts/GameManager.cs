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
    
    public Camera cam;
    public bool isSpawnPointInsideCam;
    public Transform bulletParent;

    int coinCount;
    public TextMeshProUGUI coinText;
    int killCount;
    public TextMeshProUGUI killText;
    public Transform ground;
    public Transform enemySpawnPoints;
    float enemySpawnTimer;
    public float enemySpawnTime;
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
       

        PlaneCheck();
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer > enemySpawnTime)
        {
            enemySpawnTimer = 0;
            SpawnEnemy();
        }





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
            GetComponent<CapsuleCollider>().enabled = true;
            enemy.transform.position = enemySpawnPoints.GetChild(Random.Range(0,enemySpawnPoints.childCount)).position;
            enemy.GetComponent<Enemy>().targetPlayer = player;
            enemy.transform.parent = enemyPool.transform;
        }
       

    }
    public void PlaneCheck()
    {
        if (Vector3.Distance(player.position, ground.position) > 200)
        {
            ground.position = new Vector3(player.transform.position.x, ground.position.y, player.transform.position.z);
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

}
