using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    [SerializeField] float minWidth = 10;
    [SerializeField] float minHeight = 5;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (minWidth / _camera.aspect > minHeight) {
            _camera.orthographicSize = minWidth / _camera.aspect;
        }
        else {
            _camera.orthographicSize = minHeight;
        }
    }
}
