using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] public SelectionUI SelectionUI;
    [SerializeField] public Text GoldAmount;
    [SerializeField] public Text EnemyAmount;
    [SerializeField] public Text PlayerAmount;    
    [SerializeField] public Text Warning;
    [SerializeField] public RectTransform UI;
}
