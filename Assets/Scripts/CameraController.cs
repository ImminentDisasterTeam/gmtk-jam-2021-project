using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (10 / _camera.aspect > 5) {
            _camera.orthographicSize = 10 / _camera.aspect;
        }
        else {
            _camera.orthographicSize = 5;
        }
    }
}
