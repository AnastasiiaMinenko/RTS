using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : BaseUnit
{
    private float maxValue;
    [SerializeField]private Gradient gradient;
    [SerializeField]private Image fill;   

    public void SetMaxHealth(float health)
    {
        maxValue = health;

        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        Debug.Log(health);
        Debug.Log(maxValue);
        Debug.Log(health / maxValue);
        Debug.Log(fill.fillAmount);
        Debug.Log(fill.color);

        fill.fillAmount = health / maxValue;

        fill.color = gradient.Evaluate(fill.fillAmount);
    }
}
