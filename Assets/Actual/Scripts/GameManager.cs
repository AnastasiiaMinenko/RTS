using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform gameField;

    [SerializeField] private UIController uiController;

    public static GameData Data = new GameData();    

    public void Awake()
    {
        Data.CoroutineRunner = gameObject.AddComponent<CoroutineRunner>();
        Data.GameField = gameField;   
        Data.UIController = uiController;
        Data.LevelManager = new LevelManager();
    }
        
    void Start()
    {
        CommandExecutor.Execute(new SpawnPlayerData
        {
            ID = "Player0",
            IsBot = false
        });  
        CommandExecutor.Execute(new SpawnPlayerData 
        {         
            ID = "Bot",
            IsBot = true
        });

        Data.Players[0].Enemies.Add(Data.Players[1]);
        Data.Players[1].Enemies.Add(Data.Players[0]);
    }      
    
}
