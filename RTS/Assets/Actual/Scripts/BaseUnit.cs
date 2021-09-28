using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    public UnitType Type { get; set; }
    public Vector2 Pos { get; }
    public Quaternion Rot { get; }
    public Transform Transform { get; }
    public void SetIsSelected(bool isSelected);
    public void StartMove(Transform transform, Vector3 startPos, Vector3 endPos, float duration);
}

public class BaseUnit : MonoBehaviour, IUnit
{
    public UnitType Type { get; set; }
    public Vector2 Pos { get { return transform.position;} }
    public Quaternion Rot { get { return transform.rotation; } }
    public Transform Transform { get { return transform; } }
    public void SetIsSelected(bool isSelected)
    {
        GetComponent<Renderer>().material.color = isSelected ? Color.grey : Color.white;
    }

    private IEnumerator Move(Transform transform, Vector3 startPos, Vector3 endPos, float duration)
    {
        var time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }
    }
    public void StartMove(Transform transform, Vector3 startPos, Vector3 endPos, float duration)
    {
        StartCoroutine(Move(transform, startPos, endPos, duration));
    }
}
