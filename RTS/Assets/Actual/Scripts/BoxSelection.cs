using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    private LineRenderer lineRend;
    private Vector2 initMousePos, currMousePos;
    private BoxCollider2D boxColl;

    private void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            lineRend.positionCount = 4;
            initMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(1, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(2, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(3, new Vector2(initMousePos.x, initMousePos.y));

            // This BoxSelection game object gets a box collider which is set as a trigger
            // Center of this collider is at BoxSelection position

            boxColl = gameObject.AddComponent<BoxCollider2D>();
            boxColl.isTrigger = true;
            boxColl.offset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        // While mouse button is being held down I can draw a rectangle
        // Those four points get corresponding coordinates depending on
        // mouse initial position when button was pressed for the first time
        // and its current position

        if (Input.GetMouseButton(0)  )
        {
            currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(1, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(2, new Vector2(initMousePos.x, initMousePos.y));
            lineRend.SetPosition(3, new Vector2(initMousePos.x, initMousePos.y));

            // BoxSelection gameobjects position is at the middle of the box drawn

            transform.position = (currMousePos + initMousePos) / 2;

            // Box collider boundaries outline that box drawn

            boxColl.size = new Vector2(
                Mathf.Abs(initMousePos.x - currMousePos.x),
                Mathf.Abs(initMousePos.y - currMousePos.y));
        }

        // When mouse button is released box is wiped, collider is destroyed
        // and BoxSelection gameobject goes back to the center of the scene

        if (Input.GetMouseButtonUp(0))
        {
            lineRend.positionCount = 0;
            Destroy(boxColl);
            transform.position = Vector3.zero;
        }
    }
}

