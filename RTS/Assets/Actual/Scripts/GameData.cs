using Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    public CoroutineRunner CoroutineRunner;

    public Transform GameField;

    public Player CurrentPlayer;

    public List<Player> Players = new List<Player>();

    public UIController UIController;
}
