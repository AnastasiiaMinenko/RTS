using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClicker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1<<8);

            IUnit baseUnit = null;
            if (hit)
            {
                baseUnit = (IUnit)hit.transform.GetComponent<BaseUnit>();
            }
            CommandExecutor.Execute(new UpdateSelectionData { Player = GameManager.Data.CurrentPlayer, Unit = baseUnit});
        }
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << 8);

            IUnit baseUnit = null;
            var pos = Vector2.zero;

            if (hit)
            {                
                baseUnit = (IUnit)hit.transform.GetComponent<BaseUnit>();
                pos = hit.point;
            }
            else
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            CommandExecutor.Execute(new UnitActionData { Player = GameManager.Data.CurrentPlayer, Unit = baseUnit, Pos =  pos});
        }
    }
}

