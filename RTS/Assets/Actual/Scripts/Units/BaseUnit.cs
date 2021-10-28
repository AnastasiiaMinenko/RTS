using Commands;
using Scripts.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour, IUnit
{
    
    private float maxHealth;
    private HealthBarUI healthBarUI;
    private float moveSpeed = 0.8f;
    public float MoveSpeed => moveSpeed;
    private Animator anim;    
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

    private static Dictionary<Type, IBeh> behDict = new Dictionary<Type, IBeh>
    {
        {typeof(MiningBehData),new MiningBeh()},
        {typeof(MoveBehData),new MoveBeh()},
        {typeof(NoneBehData),new NoneBeh()}
        
    };
    private IBeh currentBeh;
    public void SetBeh(IBehData data)
    {
        currentBeh?.Stop();
        currentBeh = behDict[data.GetType()];
        currentBeh.Start(data);
    }
    public void Init()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void SetAnimBool(string name, bool isVal)
    {
        anim.SetBool(name, isVal);
    }
    public virtual void SetIsSelected(bool isSelected)
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
        GameManager.Data.CoroutineRunner.StopCor(beh);
        health.UpdateEvent -= Health_UpdateEvent;
        GameObject.Destroy(healthBarUI.gameObject);

    }
}