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
		public PortalController PortalController;
		public HealthBarUI valueHealth;

	}

	public class SpawnUnitCommand : ICommand
	{
		private SpawnUnitData data;
		private Action onSuccess;
		private Action<string> onFail;
		private static Dictionary<UnitType, UnitBuildData> UnitPath = new Dictionary<UnitType, UnitBuildData>
		{
			{
				UnitType.CASTLE, new UnitBuildData { Path = "Buildings/Castle", OffsetY = 200}
			},
            {
				UnitType.MINE, new UnitBuildData { Path = "Buildings/Mine", OffsetY = 150}
			},             
			{
				UnitType.BARRACK, new UnitBuildData { Path = "Buildings/Barrack", OffsetY = 180}
			},  
			{
				UnitType.PORTAL, new UnitBuildData { Path = "Buildings/Portal", OffsetY = 200}
			}, 
			{
				UnitType.WORKER, new UnitBuildData { Path = "Units/Worker", OffsetY = 60}
			}, 
			{
				UnitType.WARRIOR, new UnitBuildData { Path = "Units/Warrior", OffsetY = 60}
			}, 
			{
				UnitType.ENEMY, new UnitBuildData { Path =  "Units/Enemy", OffsetY = 60}
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
			var prefab = Resources.Load<BaseUnit>("Prefabs/" + UnitPath[data.Type].Path);			
			var gameObject = GameObject.Instantiate(prefab, data.Pos, Quaternion.Euler(data.Rot), GameManager.Data.GameField);
			gameObject.Owner = data.Player;
			gameObject.Type = data.Type;			
			data.Player.Units.Add(gameObject);	


			var prefabHP = Resources.Load<HealthBarUI>("Prefabs/UI/HealthBar");				
			var healthBarObject = GameObject.Instantiate(prefabHP, GameManager.Data.UIController.UI);		
			var followController = healthBarObject.gameObject.AddComponent<UIFollowController>();						
			followController.Init(healthBarObject.GetComponent<RectTransform>(), gameObject.transform, UnitPath[data.Type].OffsetY, GameManager.Data.UIController.UI.rect.height);
			gameObject.SetHealthBar(10, healthBarObject);

			if (data.Type == UnitType.CASTLE)
            {
				var castle = (CastleController)gameObject; 
				data.Player.AddCastle(castle);
				castle.Init();

			}	
			else if(data.Type == UnitType.PORTAL)
            {
				var portal = (PortalController)gameObject;
				portal.Init();
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
			else if(data.Type == UnitType.WARRIOR || data.Type == UnitType.ENEMY)
            {
				var war = (WarriorController)gameObject;
				war.Init();
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

	public struct UnitBuildData
    {
		public string Path;
		public float OffsetY;
    }
}