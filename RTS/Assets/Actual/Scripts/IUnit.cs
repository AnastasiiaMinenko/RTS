using Commands;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    public float Health { get; }
    public ActiveData<bool> IsAlive { get; }
    public UnitType Type { get; set; }
    public float MoveSpeed { get; }
    public void SetAnimBool(string name, bool isVal);
    public Vector2 Pos { get; }
    public Quaternion Rot { get; }
    public Transform Transform { get; }
    public Player Owner { get; set; }
    public void SetIsSelected(bool isSelected);
    //public void StartMove(Transform transform, Vector3 startPos, Vector3 endPos, float duration);
    public void SetHealthBar(float health, HealthBarUI healthBarUI);
    public void ReceiveDamage(float damage);
    public void DestroyUnit();
}
