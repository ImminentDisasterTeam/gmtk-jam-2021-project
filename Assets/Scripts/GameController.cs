using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int minValue = 1;
    int maxValue = 20;
    Vector2 numberRadius = new Vector2(2f, 2f);
    Player player;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject numberObject;
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
        Vector3 spawnPoint = GetRandomPoint();
        if (spawnPoint == Vector3.zero)
            return;
        Number number = Instantiate(numberObject, spawnPoint, Quaternion.identity).GetComponent<Number>();
        number.Initiate(Random.Range(minValue, maxValue));
    }

    Vector3 GetRandomPoint()
    {
        var x = Random.Range(-levelSizeHolder.levelSize.x / 2, levelSizeHolder.levelSize.x / 2);
        var y = Random.Range(-levelSizeHolder.levelSize.y / 2, levelSizeHolder.levelSize.y / 2);
        Vector3 vector = new Vector3(x, y);

        RaycastHit2D hit = Physics2D.BoxCast(vector, numberRadius, 0, Vector2.up, 0, spawnCollisionLayer);
        int counter = 1000;
        while (counter > 0 && hit.collider != null)
        {
            x = Random.Range(-levelSizeHolder.levelSize.x / 2, levelSizeHolder.levelSize.x / 2);
            y = Random.Range(-levelSizeHolder.levelSize.y / 2, levelSizeHolder.levelSize.y / 2);
            vector = new Vector3(x, y);
            hit = Physics2D.BoxCast(vector, numberRadius, 0, Vector2.up, 0, spawnCollisionLayer);
            counter--;
            if (counter == 0) {
                Debug.Log("FUCK YOU");
            }
        }

        if (counter == 0)
            return Vector3.zero;

        return vector;
    }

    void SpawnPlayer()
    {
        player = Instantiate(playerObject, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.onDeath = OnPlayerDeath;
    }

    void SpawnEraser()
    {

    }

    // Start is called before the first frame update
    void Start() {
        var numbersToSpawn = 20;
        SpawnPlayer();
        for (var i = 0; i < numbersToSpawn; i++)
            SpawnNewNumber();
    }
}
