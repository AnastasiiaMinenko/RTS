using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct UpdateSelectionData : ICommandData
	{		
		public Player Player;
		public IUnit Unit;
	}

	public class UpdateSelectionCommand : ICommand
	{
		private UpdateSelectionData data;
		private Action onSuccess;
		private Action<string> onFail;		

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (UpdateSelectionData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}

		private void Do()
		{

			if (data.Player.SelectedUnit.Value != null)
			{
				data.Player.SelectedUnit.Value.SetIsSelected(false);
			}

			data.Player.SelectedUnit.Value = data.Unit;

			

			data.Unit?.SetIsSelected(true);
		}       
    }
}