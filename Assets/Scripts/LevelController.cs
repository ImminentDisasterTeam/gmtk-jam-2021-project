using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    int minValue = 1;
    int maxValue = 20;
    int erasersCount = 1;
    float eraserSpawnRate = 10f;
    float numberSpawnRate = 0.75f;
    int goal = 50;
    Vector2 numberRadius = new Vector2(2f, 2f);
    Player player;
    GameObject parentObject;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject numberPrefab;
    [SerializeField] GameObject eraserPrefab;
    [SerializeField] LevelSizeHolder levelSizeHolder;
    [SerializeField] LayerMask spawnCollisionLayer;

    Coroutine _spawnErasersCoro;
    Coroutine _spawnNumbersCoro;

    public void OnPlayerDeath()
    {
        Debug.Log(">пук");
        if (_spawnErasersCoro != null)
        {
            StopCoroutine(_spawnErasersCoro);
        }
        if (_spawnNumbersCoro != null)
        {
            StopCoroutine(_spawnNumbersCoro);
        }
    }

    public void InitializeLevel(int minNumber = 1, int maxNumber = 20, float numberSpawnRate = 0.75f, int goal = 50, int erasersCount = 1, float eraserSpawnInterval = 10f)
    {
        minValue = minNumber;
        maxValue = maxNumber;
        this.erasersCount = erasersCount;
        this.eraserSpawnRate = eraserSpawnInterval;
        this.numberSpawnRate = numberSpawnRate;
        this.goal = goal;

        parentObject = new GameObject();

        SpawnPlayer();
        _spawnNumbersCoro = StartCoroutine(nameof(SpawnNewNumber));
        _spawnErasersCoro = StartCoroutine(nameof(SpawnEraser));
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.onDeath = OnPlayerDeath;

        player.transform.SetParent(parentObject.transform);
    }

    IEnumerator SpawnNewNumber()
    {
        for (; ; )
        {
            Vector3 spawnPoint = GetRandomPosition();
            if (spawnPoint == Vector3.zero)
                yield return null;
            var number = Instantiate(numberPrefab, spawnPoint, Quaternion.identity).GetComponent<Number>();
            number.Initiate(Random.Range(minValue, maxValue));

            number.transform.SetParent(parentObject.transform);
            yield return new WaitForSeconds(numberSpawnRate);
        }
    }

    IEnumerator SpawnEraser()
    {
        for (var i = 0; i < erasersCount; ++i)
        {
            GameObject eraser = Instantiate(eraserPrefab, GetRandomPosition(), Quaternion.identity);

            eraser.transform.SetParent(parentObject.transform);
            yield return new WaitForSeconds(eraserSpawnRate);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // var numbersToSpawn = 20;
        // SpawnPlayer();
        // for (var i = 0; i < numbersToSpawn; i++) {
        //     SpawnNewNumber();
        // }
        // SpawnEraser();
        InitializeLevel();
    }

    public void ClearLevel()
    {
        Destroy(parentObject);
    }

    Vector2 GetRandomPosition()
    {
        RaycastHit2D hit;
        var position = Vector2.zero;

        var counter = 1000;
        do
        {
            if (counter-- == 0)
            {
                Debug.Log("FUCK YOU");
                break;
            }
            var x = Random.Range(-levelSizeHolder.levelSize.x / 2, levelSizeHolder.levelSize.x / 2);
            var y = Random.Range(-levelSizeHolder.levelSize.y / 2, levelSizeHolder.levelSize.y / 2);
            position = new Vector3(x, y);
            hit = Physics2D.BoxCast(position, numberRadius, 0, Vector2.up, 0, spawnCollisionLayer);

        } while (hit);

        return position;
    }
}
