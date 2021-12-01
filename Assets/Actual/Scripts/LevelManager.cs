using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager 
{
    private bool isEnd;
    public LevelManager()
    {
        GameManager.Data.Players.AddEvent += Players_AddEvent;
        GameManager.Data.Players.RemoveEvent += Players_RemoveEvent;
    }
    private void Players_AddEvent(Player player)
    {        
        player.Units.UpdateEvent += Units_UpdateEvent;
    }
    private void Players_RemoveEvent(Player player)
    {
        player.Units.UpdateEvent -= Units_UpdateEvent;
    }

    private void Units_UpdateEvent(List<IUnit> units)
    {
        if(units.Count==0)
        {
            CheckPlayers();
        }
    }
    private void CheckPlayers()
    {
        var winPlayerIDs = new List<string>();
        var players = GameManager.Data.Players.Value;

        foreach(var player in players)
        {
            if (player.Units.Count!=0)
            {
                winPlayerIDs.Add(player.ID);
            }
        }
        OpenLevel(winPlayerIDs.Contains(GameManager.Data.CurrentPlayer.ID) ? "WinScene" : "LoseScene");        
    }
    private void OpenLevel(string name)
    {
        if(!isEnd)
        {
            isEnd = true;
            while (GameManager.Data.Players.Count > 0)
            {
                GameManager.Data.Players[0].DeInit();
                GameManager.Data.Players.Remove(GameManager.Data.Players[0]);
            }
            SceneManager.LoadScene(name);
        }
               
        
        
    }
}
