using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameField;

    [SerializeField] private UIController uiController;

    public static GameData Data = new GameData();
    
    public void Awake()
    {
        Data.GameField = gameField;   
        Data.UIController = uiController;           
    }
        
    void Start()
    {
        CommandExecutor.Execute(new SpawnPlayerData 
        {         
            ID = "Player0"
        });                
    }      
    
}
