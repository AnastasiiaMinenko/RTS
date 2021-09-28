using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct UnitActionData : ICommandData
	{		
		public Player Player;
		public IUnit Unit;
		public Vector2 Pos;
	}

	public class UnitActionCommand : ICommand
	{
		private UnitActionData data;
		private Action onSuccess;
		private Action<string> onFail;		

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (UnitActionData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}

		private void Do()
		{

			var selectedUnit = data.Player.SelectedUnit.Value;
			if (selectedUnit != null)
			{
				if (data.Unit!=null)
                {
					if(selectedUnit is WorkerController && data.Unit is MineController)
                    {
						((WorkerController)selectedUnit).SetMine((MineController)data.Unit);
                    }
                }
                else 
                {
					if(selectedUnit is WorkerController)
                    {
						((WorkerController)selectedUnit).SetMine(null);
					}
					var dir = ((Vector2)selectedUnit.Transform.position - data.Pos).magnitude;
					selectedUnit.StartMove(selectedUnit.Transform, selectedUnit.Transform.position, (Vector3)data.Pos, dir);
				}
			}		
			
		}   
		
		
    }
}