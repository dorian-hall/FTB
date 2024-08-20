using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class Camera_Controller : MonoBehaviour
{
    [Header("Movement Controls")]

        [Range(1,10)] 
        [SerializeField] float Movement_Speed = 1;
        [Tooltip("Sets The the Space That the Camera Can Move in")]
        [SerializeField] float maxX;
        [SerializeField] float minX;
        [SerializeField] float maxY;
        [SerializeField] float minY;

    [Header("Zoom Controls")]

        [Range(10, 20)] 
        [SerializeField] float Max_Zoom = 10;
        [Range(1, 10)]
        [SerializeField] float Min_Zoom = 1;
        [Range(1, 10)]
        [SerializeField] float Zoom_Speed = 1;
        

    
    Camera _camera;
    Controls _Controls;
    
    void Awake()
    {
        _Controls = new Controls();
        _camera = Camera.main;
    }
    void move(Vector2 input)
    {
        Vector3 offset =  new Vector3(input.x, input.y *2,-input.x)* Movement_Speed * Time.deltaTime;
        if (transform.localPosition.y + offset.y > maxY) { offset.y = 0; }
        if (transform.localPosition.y + offset.y < minY) { offset.y = 0; }
        
        if (transform.localPosition.x + offset.x > maxX) { offset.x = 0; offset.z = 0;  }
        if (transform.localPosition.x + offset.x < minX) { offset.x = 0; offset.z = 0; }
        transform.localPosition += offset;
    }
    void zoom(float Input)
    {
        _camera.orthographicSize += Input * Zoom_Speed * Time.deltaTime;
        if(_camera.orthographicSize > Max_Zoom) {_camera.orthographicSize = Max_Zoom;}
        if(_camera.orthographicSize < Min_Zoom) {_camera.orthographicSize = Min_Zoom;}
     }
    void Update()
    {
        move(_Controls.actionmap.Movement.ReadValue<Vector2>());
        zoom(_Controls.actionmap.Zoom.ReadValue<float>());
    }

    private void OnEnable()
    {
        _Controls.Enable();
    }

    private void OnDisable()
    {
        _Controls.Disable();
    }
}
