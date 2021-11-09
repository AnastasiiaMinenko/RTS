using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager 
{
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

        if(winPlayerIDs.Contains(GameManager.Data.CurrentPlayer.ID))
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            Debug.Log(123);
            SceneManager.LoadScene("LoseScene");
        }
    }
}
