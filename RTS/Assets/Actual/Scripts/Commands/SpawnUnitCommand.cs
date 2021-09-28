using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct SpawnUnitData : ICommandData
	{
		public Vector2 Pos;
		public Vector3 Rot;
		public UnitType Type;

		public Player Player;
	}

	public class SpawnUnitCommand : ICommand
	{
		private SpawnUnitData data;
		private Action onSuccess;
		private Action<string> onFail;
		private static Dictionary<UnitType, string> UnitPath = new Dictionary<UnitType, string>
		{
			{
				UnitType.CASTLE, "Buildings/Castle"				
			},
            {
				UnitType.MINE, "Buildings/Mine"
            }, 
			{
				UnitType.BARRACK, "Buildings/Barrack"
            },  
			{
				UnitType.PORTAL, "Buildings/EnemyPortal"
            }, 
			{
				UnitType.WORKER, "Units/Worker"
            }, 
			{
				UnitType.WARRIOR, "Units/Warrior"
            }, 
			{
				UnitType.ENEMY, "Units/Enemy"
            }
		};

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (SpawnUnitData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}

		private void Do()
		{
			//Debug.Log(UnitPath[data.Type]);
			var prefab = Resources.Load<BaseUnit>("Prefabs/" + UnitPath[data.Type]);
			//Debug.Log(prefabCastle);
			var gameObject = GameObject.Instantiate(prefab, data.Pos, Quaternion.Euler(data.Rot), GameManager.Data.GameField);

			gameObject.Type = data.Type;
			
			data.Player.Units.Add(gameObject);

			if(data.Type == UnitType.CASTLE)
            {
				data.Player.AddCastle((CastleController)gameObject);
			}
			else if(data.Type == UnitType.WORKER)
            {
				var worker = (WorkerController)gameObject;
				worker.SetCastle((CastleController)data.Player.GetUnitByType(UnitType.CASTLE));

				var mine = data.Player.GetUnitByType(UnitType.MINE);
				if (mine != null)
                {
					worker.SetMine((MineController)mine);
				}
			}					
		}		
	}

	public enum UnitType
	{
		CASTLE,
		MINE,
		BARRACK,
		PORTAL,
		WORKER,
		WARRIOR,
		ENEMY,
	}
}