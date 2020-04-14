using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabRock;

    [SerializeField] private Sprite greenRock;
    [SerializeField] private Sprite magentaRock;
    [SerializeField] private Sprite whiteRock;

    private const float MinSpawnDelay = 1;

    private const float MaxSpawnDelay = 1;

    private Timer spawnTimer;

    private const int SpawnBorderSize = 100;

    private int minSpawnX;

    private int maxSpawnX;

    private int minSpawnY;
    
    private int maxSpawnY;
    
    
    // Start is called before the first frame update
    void Start()
    {
        minSpawnX = SpawnBorderSize;
        maxSpawnX = Screen.width - SpawnBorderSize;
        minSpawnY = SpawnBorderSize;
        maxSpawnY = Screen.height - SpawnBorderSize;

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Finished)
        {
            if (GameObject.FindGameObjectsWithTag("Rock(Clone)").Length < 3)
            {
                SpawnRock();
            }
            spawnTimer.Duration = Random.Range(MinSpawnDelay, MaxSpawnDelay);
            spawnTimer.Run();
        }
    }

    void SpawnRock()
    {
        Vector3 location = new Vector3(Random.Range(minSpawnX,maxSpawnX),
            Random.Range(minSpawnY,maxSpawnY),-Camera.main.transform.position.z);
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(location);
        GameObject rock = Instantiate(prefabRock) as GameObject;
        rock.transform.position = worldLocation;

        SpriteRenderer spriteRenderer = rock.GetComponent<SpriteRenderer>();
        int spriteNumber = Random.Range(0, 3);
        switch (spriteNumber)
        {
            case 1:
                spriteRenderer.sprite = greenRock;
                break;
            case 2:
                spriteRenderer.sprite = magentaRock;
                break;
            default:
                spriteRenderer.sprite = whiteRock;
                break;
        }
    }
}
