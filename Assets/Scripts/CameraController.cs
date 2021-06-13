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

    void Awake()
    {
        _camera = GetComponent<Camera>();
        levelSizeHolder.OnChange += OnChangeSize;
    }

    // Update is called once per frame
    void OnChangeSize(Vector2 levelSize) {
        var minWidth = levelSize.x / 2 + xOffset;
        var minHeight = levelSize.y / 2 + yOffset;

        var minWidthInHeight = minWidth / _camera.aspect;
        _camera.orthographicSize = Math.Max(minWidthInHeight + xOffset, minHeight + yOffset);
    }
}
