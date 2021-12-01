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
			var selectedUnits = data.Player.SelectedUnit.Value;
			var targetUnit = data.Unit;
						
			/*for (var i = 0; i < selectedUnits.Count; i++)
			{
				DoUnitAction(selectedUnits[i], targetUnit);
			}*/
			selectedUnits.ForEach(item => DoUnitAction(item, targetUnit));
		}
		private void DoUnitAction(IUnit selectedUnit, IUnit targetUnit)
		{
			if (selectedUnit != null && selectedUnit.Owner == data.Player)
			{
				if (targetUnit != null)
				{
					if (selectedUnit is WorkerController && targetUnit is MineController)
					{
						((WorkerController)selectedUnit).SetMine((MineController)targetUnit);
					}
					if(selectedUnit is WarriorController)
                    {						
						var isEnemy = !data.Player.Units.Contains(targetUnit);
						if(isEnemy)
                        {
							selectedUnit.SetBeh(new MoveAndAttackBehData { Unit = selectedUnit, Enemy = targetUnit, AttackSpeed = selectedUnit.AttackSpeed, Damage = selectedUnit.Damage, Dist = selectedUnit.Dist, IsShot = selectedUnit.IsShot});
                        }
                    }
				}
				else
				{
					if (selectedUnit is WorkerController)
					{						
						((WorkerController)selectedUnit).SetMine(null);
					}
					var dir = ((Vector2)selectedUnit.Transform.position - data.Pos).magnitude;
					if (selectedUnit is WorkerController || selectedUnit is WarriorController)
					{						
						selectedUnit.SetBeh(new MoveBehData { transform = selectedUnit.Transform, startPos = selectedUnit.Transform.position, endPos = (Vector3)data.Pos, duration = dir/selectedUnit.MoveSpeed });						
					}
				}
			}
		}
	}
}