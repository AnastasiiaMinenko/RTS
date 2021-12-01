using Common.Tools.Data;
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
			var selectedUnits = data.Player.SelectedUnit;
			var targetUnit = data.Unit;
			if (targetUnit == null) 
            {				
				ClearSelection(selectedUnits);
            }
            else
            {				
				if(selectedUnits.Contains(targetUnit))
                {					
					targetUnit.SetIsSelected(false);
					selectedUnits.Remove(targetUnit);
				}
                else
                {
					targetUnit.SetIsSelected(true);
					selectedUnits.Add(targetUnit);
                }				
            }			
		}   
		private void ClearSelection(ActiveListData<IUnit> selectedUnits)
        {			
			for (var i = 0; i < selectedUnits.Count; i++)
			{
				selectedUnits[i].SetIsSelected(false);
			}
			selectedUnits.Clear();
		}
    }
}