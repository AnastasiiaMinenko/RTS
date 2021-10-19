using Commands;
using Scripts.Core.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IUnit
{
    private float maxHealth;
    private HealthBarUI healthBarUI;

    private ActiveData<float> health = new ActiveData<float>();
    public float Health => health.Value;
    private ActiveData<bool> isAlive = new ActiveData<bool>(true);
    public ActiveData<bool> IsAlive => isAlive;
    public UnitType Type { get; set; }
    public Vector2 Pos { get { return transform.position; } }
    public Quaternion Rot { get { return transform.rotation; } }
    public Transform Transform { get { return transform; } }
    public Player Owner { get; set; }
    protected Coroutine beh;
    public void SetIsSelected(bool isSelected)
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach(var item in renderers)
        {
           item.material.color = isSelected ? Color.grey : Color.white;
        }        
    }

    public void StartMove(Transform transform, Vector3 startPos, Vector3 endPos, float duration)
    {
        GameManager.Data.CoroutineRunner.StopCor(beh);
        beh = GameManager.Data.CoroutineRunner.StartCor(CoroutineRunnerExt.Move(transform, startPos, endPos, duration));
    }

    public void SetHealthBar(float health, HealthBarUI healthBarUI)
    {
        this.health.Value = health;
        this.maxHealth = health;
        this.healthBarUI = healthBarUI;
        this.health.UpdateEvent += Health_UpdateEvent;

        healthBarUI.SetMaxHealth(maxHealth);
        this.health.Value = maxHealth;
    }
    private void Health_UpdateEvent(float obj)
    {
        healthBarUI.SetHealth(health.Value);
    }
    public void ReceiveDamage(float damage)
    {
        health.Value -= damage;
    }
    public virtual void DestroyUnit()
    {
        health.UpdateEvent -= Health_UpdateEvent;
        GameObject.Destroy(healthBarUI.gameObject);
    }
}