using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(_camera.transform);
        transform.eulerAngles = new Vector3(0f, 90f, 0f);
    }
}
