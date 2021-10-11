﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
	public struct DestroyUnitData : ICommandData
	{
		

	}

	public class DestroyUnitCommand : ICommand
	{
		private DestroyUnitData data;
		private Action onSuccess;
		private Action<string> onFail;
		

		public void Execute(ICommandData data, Action onSuccess, Action<string> onFail)
		{
			this.data = (DestroyUnitData)data;
			this.onSuccess = onSuccess;
			this.onFail = onFail;

			Do();
		}

		private void Do()
		{
				
		}		
	}

	
}