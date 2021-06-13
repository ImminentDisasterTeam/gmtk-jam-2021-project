using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    int _minValue = 1;
    int _maxValue = 20;
    int _erasersCount = 1;
    float _eraserSpawnRate = 10f;
    float _numberSpawnRate = 0.75f;
    int _goal = 50;
    readonly Vector2 _numberRadius = new Vector2(2f, 2f);
    Player _player;
    GameObject _parentObject;
    [SerializeField] GameObject storagePrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject numberPrefab;
    [SerializeField] GameObject eraserPrefab;
    [SerializeField] LevelSizeHolder levelSizeHolder;
    [SerializeField] LayerMask spawnCollisionLayer;
    [SerializeField] Commentator commentator;

    public System.Action SwitchLevel;

    Coroutine _spawnErasersCoro;
    Coroutine _spawnNumbersCoro;
    Vector2 _levelSize;

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

        FinishLevel();
    }

    void OnStore(int value)
    {
        if (value >= _goal)
        {
            Debug.Log($"Goal achieved! {value}");
            FinishLevel();
            SwitchLevel();
        }
    }

    void FinishLevel()
    {
        Destroy(_parentObject);
    }

    public void InitializeLevel(GameSettings gameSettings, int level)
    {
        _minValue = gameSettings.MinValue;
        _maxValue = gameSettings.MaxValue;
        _erasersCount = gameSettings.ErasersCount;
        _eraserSpawnRate = gameSettings.EraserSpawnRate;
        _numberSpawnRate = gameSettings.NumberSpawnRate;
        _goal = gameSettings.Goal;
        _levelSize = gameSettings.levelSize;
        levelSizeHolder.SetSize(_levelSize);

        _parentObject = new GameObject();

        commentator.SetLevel(level);

        SpawnPlayer();
        SpawnStorage();

        _player.SummReplic = commentator.Summ;

        _spawnNumbersCoro = StartCoroutine(nameof(SpawnNewNumber));
        _spawnErasersCoro = StartCoroutine(nameof(SpawnEraser));
    }

    void SpawnPlayer()
    {
        _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        _player.onDeath = OnPlayerDeath;

        _player.transform.SetParent(_parentObject.transform);
    }
    void SpawnStorage()
    {
        var storage = Instantiate(storagePrefab, _parentObject.transform);
        var storageHeight = storage.GetComponent<SpriteRenderer>().size.y;
        var storageLocation = new Vector2(0, _levelSize.y / 2 - storageHeight / 2);
        storage.transform.position = storageLocation;
        storage.GetComponent<Storage>().OnStore += OnStore;

        // storage.transform.SetParent(parentObject.transform);
    }

    IEnumerator SpawnNewNumber()
    {
        for (; ; )
        {
            Vector3 spawnPoint = GetRandomPosition();
            if (spawnPoint == Vector3.zero)
                yield return null;
            var number = Instantiate(numberPrefab, spawnPoint, Quaternion.identity).GetComponent<Number>();
            number.Initiate(Random.Range(_minValue, _maxValue));
            number.mapObject = _parentObject.transform;
            number.transform.SetParent(_parentObject.transform);
            yield return new WaitForSeconds(_numberSpawnRate);
        }
    }

    IEnumerator SpawnEraser()
    {
        for (var i = 0; i < _erasersCount; ++i)
        {
            GameObject eraser = Instantiate(eraserPrefab, GetRandomPosition(), Quaternion.identity);
            commentator.EnemyAppear(i);

            eraser.transform.SetParent(_parentObject.transform);
            yield return new WaitForSeconds(_eraserSpawnRate);
        }
    }

    public void ClearLevel()
    {
        Destroy(_parentObject);
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
            var x = Random.Range(-_levelSize.x / 2, _levelSize.x / 2);
            var y = Random.Range(-_levelSize.y / 2, _levelSize.y / 2);
            position = new Vector3(x, y);
            hit = Physics2D.BoxCast(position, _numberRadius, 0, Vector2.up, 0, spawnCollisionLayer);

        } while (hit);

        return position;
    }
}
