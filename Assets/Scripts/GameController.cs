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
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-5f, 5f);
        Vector3 vector = new Vector3(x, y);

        RaycastHit2D hit = Physics2D.BoxCast(vector, numberRadius, 0, Vector2.up);
        int counter = 20;
        while (counter > 0 && hit.collider != null)
        {
            x = Random.Range(-10f, 10f);
            y = Random.Range(-5f, 5f);
            vector = new Vector3(x, y);
            hit = Physics2D.BoxCast(vector, numberRadius, 0, Vector2.up);
            counter--;
            Debug.Log("aaaaaaaaaaaaaaaaaaaaa");
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
    void Start()
    {
        SpawnPlayer();
        for (int i = 0; i < 100; i++)
            SpawnNewNumber();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
