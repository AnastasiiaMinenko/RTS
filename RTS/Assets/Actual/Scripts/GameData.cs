using Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Tools.Data;

public class GameData 
{
    public CoroutineRunner CoroutineRunner;

    public Transform GameField;

    public Player CurrentPlayer;

    public ActiveListData<Player> Players = new ActiveListData<Player>();

    public UIController UIController;

    public LevelManager LevelManager;
}
