using System;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
    public static class CommandExecutor
    {
        private static Dictionary<Type, ICommand> commandDict = new Dictionary<Type, ICommand>
        {
            {typeof(SpawnUnitData),new SpawnUnitCommand()},
            {typeof(SpawnPlayerData),new SpawnPlayerCommand()},
            //{typeof(SpawnPortalData),new SpawnPortalCommand()},
            {typeof(UpdateSelectionData),new UpdateSelectionCommand()},
            {typeof(UnitActionData),new UnitActionCommand()},

        };

        public static void Execute(ICommandData data, Action onComplete = null, Action<string> onFail = null)
        {
            //Debug.Log(data.GetType());
            commandDict[data.GetType()].Execute(data, onComplete, onFail);

            //(new SpawnUnitCommand()).Execute(new SpawnUnitData(), onComplete, onFail);
        }

    }

    public interface ICommandData
    {

    }    
    public interface ICommand
    {
        void Execute(ICommandData data, Action onSuccess, Action<string> onFail);
    }
}