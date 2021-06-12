using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int maxValue = 1;
    int minValue = 9;
    Player player;
    GameObject playerObject;

    public void OnPlayerDeath()
    {

    }

    public void SetRange(int min, int max)
    {

    }

    void SpawnNewNumber()
    {
        
    }

    public static void SpawnNumber()
    {

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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
