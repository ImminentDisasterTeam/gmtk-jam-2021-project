using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    int minValue = 1;
    int maxValue = 20;
    Vector2 numberRadius = new Vector2(2f, 2f);
    Player player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject numberPrefab;
    [SerializeField] GameObject eraserPrefab;
    [SerializeField] LevelSizeHolder levelSizeHolder;
    [SerializeField] LayerMask spawnCollisionLayer;
    

    public void OnPlayerDeath()
    {

    }

    public void SetNumbersRange(int min, int max)
    {

    }

    void SpawnNewNumber()
    {
        Vector3 spawnPoint = GetRandomPosition();
        if (spawnPoint == Vector3.zero)
            return;
        var number = Instantiate(numberPrefab, spawnPoint, Quaternion.identity).GetComponent<Number>();
        number.Initiate(Random.Range(minValue, maxValue));
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.onDeath = OnPlayerDeath;
    }

    void SpawnEraser() {
        Instantiate(eraserPrefab, GetRandomPosition(), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start() {
        var numbersToSpawn = 20;
        SpawnPlayer();
        for (var i = 0; i < numbersToSpawn; i++) {
            SpawnNewNumber();
        }
        SpawnEraser();
    }

    Vector2 GetRandomPosition() {
        RaycastHit2D hit;
        var position = Vector2.zero;

        var counter = 1000;
        do {
            if (counter-- == 0) {
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
