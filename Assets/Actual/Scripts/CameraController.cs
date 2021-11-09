using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 dragOrigin;
    private Vector3 difference;

    [SerializeField] private SpriteRenderer map;
    private float mapMaxX, mapMinX;   

    private void Awake()
    {
        mapMaxX = map.transform.position.x + map.bounds.size.x / 2f;
        mapMinX = map.transform.position.x - map.bounds.size.x / 2f;
    }


    private void Update()
    {
        MovementCamera();
    }
    private void MovementCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition/Screen.dpi / 50;
        }
        
        if(Input.GetMouseButton(0))
        {
            difference = dragOrigin - Input.mousePosition / Screen.dpi /50;            

            cam.transform.position = ClampCamera(cam.transform.position + difference);            
        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {       
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;        

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        
        return new Vector3(newX, 0, targetPosition.z);
    }
    
}
