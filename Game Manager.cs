using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameObject meanDogPrefab;
    private Transform spawnPoint;
    private float spawnDelay = 7f;
    private float timer;

    public int score = 0;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        if (Instance == null)
        {
            GameObject gm = new GameObject("GameManager");
            Instance = gm.AddComponent<GameManager>();
            DontDestroyOnLoad(gm);
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        timer = spawnDelay;

       
        GameObject sp = new GameObject("SpawnPoint");
        spawnPoint = sp.transform;

      
        meanDogPrefab = Resources.Load<GameObject>("meanDogPrefab");
        if (meanDogPrefab == null)
        {
            Debug.LogError("EnemyPrefab not found in Resources folder!");
        }
    }

    void Update()
    {
        if (meanDogPrefab == null || spawnPoint == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            timer = spawnDelay;
        }


    }
    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(meanDogPrefab, spawnPoint.position, Quaternion.identity);

      
        EnemyChase chase = newEnemy.GetComponent<EnemyChase>();
        if (chase != null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                chase.player = playerObj; 
            }
            else
            {
                Debug.LogWarning("Player object not found in scene. Make sure it has the tag 'Player'.");
            }
        }

        Debug.Log("Enemy spawned and assigned player!");
    }

   
    public static void Setup(GameObject prefab, Transform point, float delay = 5f)
    {
        if (Instance != null)
        {
            Instance.meanDogPrefab = prefab;
            Instance.spawnPoint = point;
            Instance.spawnDelay = delay;
            Instance.timer = delay;
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }

   
    public int GetScore()
    {
        return score;
    }
}
 


