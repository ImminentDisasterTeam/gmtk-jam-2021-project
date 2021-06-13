using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    [SerializeField] float xOffset = 1;
    [SerializeField] float yOffset = 1;
    [SerializeField] LevelSizeHolder levelSizeHolder;
    Vector2 _levelSize;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        levelSizeHolder.OnChange += OnChangeSize;
    }

    void Update() {
        var minWidth = _levelSize.x / 2 + xOffset;
        var minHeight = _levelSize.y / 2 + yOffset;

        var minWidthInHeight = minWidth / _camera.aspect;
        _camera.orthographicSize = Math.Max(minWidthInHeight + xOffset, minHeight + yOffset);
    }

    // Update is called once per frame
    void OnChangeSize(Vector2 levelSize) {
        _levelSize = levelSize;
    }
}
