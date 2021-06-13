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

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        var minWidth = levelSizeHolder.levelSize.x / 2 + xOffset;
        var minHeight = levelSizeHolder.levelSize.y / 2 + yOffset;

        var minWidthInHeight = minWidth / _camera.aspect;
        _camera.orthographicSize = Math.Max(minWidthInHeight + xOffset, minHeight + yOffset);
    }
}
