using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct HitUnitData : ICommandData
	{
		public IUnit Enemy;
		public float Damage;
	}
	public class HitUnitCommand : ICommand
	{
		private HitUnitData data;
		private Action onSuccess;
		private Action<string> onFail;


		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (HitUnitData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}
		private void Do()
		{
			data.Enemy.ReceiveDamage(data.Damage);
			if (data.Enemy.Health < 0)
			{
				CommandExecutor.Execute(new DestroyUnitData
				{
					Unit = data.Enemy
				});
			}
		}
	}
}