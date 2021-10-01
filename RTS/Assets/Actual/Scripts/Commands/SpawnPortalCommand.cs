using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	/*public struct SpawnPortalData : ICommandData
	{		
		public string ID;
	}

	public class SpawnPortalCommand : ICommand
	{
		private SpawnPortalData data;
		private Action onSuccess;
		private Action<string> onFail;		

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (SpawnPortalData)data;
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
				Type = UnitType.PORTAL,
				Pos = new Vector2(28.2f, 2.64f), 
				Rot = new Vector3(0, 0, 0),
				Player = player
			});

		}       
    }*/
}