using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct SpawnPlayerData : ICommandData
	{		
		public string ID;
	}

	public class SpawnPlayerCommand : ICommand
	{
		private SpawnPlayerData data;
		private Action onSuccess;
		private Action<string> onFail;		

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (SpawnPlayerData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}

		private void Do()
		{
			var player = new Player();			
			player.Init();
			GameManager.Data.CurrentPlayer = player;
			GameManager.Data.Players.Add(player);  //spawn player
			CommandExecutor.Execute(new SpawnUnitData 
			{ 
				Type = UnitType.CASTLE,
				Pos = new Vector2(-1.45f, 2.33f), 
				Rot = new Vector3(0, 180, 0),
				Player = player
			});

            player.Gold.UpdateEvent += Gold_UpdateEvent;
		}

        private void Gold_UpdateEvent(int val)
        {
			GameManager.Data.UIController.GoldAmount.text = "Gold: " + val.ToString();
        }
    }
}