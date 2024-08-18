using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexFrogGames.Tools.Console
{
    [CreateAssetMenu(fileName = "Console Actions", menuName = "ScriptableObjects/Console/Actions")]

    public class ConsoleActions : ScriptableObject
    {
        [SerializeField] private List<ConsoleAction> _consoleActions = new List<ConsoleAction>();

        public bool TryGetConsoleActionBySubString(string substring, out List<ConsoleAction> result)
        {
            result = _consoleActions.Where(action => action.Command.ToLower().Contains(substring.ToLower())).ToList();
            return result.Count > 0;
        }

        public bool TrySubscribeActionByName(string commandName, Action method)
        {
            var foundConsoleAction =
                _consoleActions.FirstOrDefault(action => action.Name.ToLower().Contains(commandName.ToLower()));

            if (foundConsoleAction is null) return false;

            ref var result = ref foundConsoleAction.Action;

            result += method;
            return true;
        }

        public bool TrySubscribeActionByName(string commandName, Action<string> method)
        {
            var foundConsoleAction =
                _consoleActions.FirstOrDefault(action => action.Name.ToLower().Contains(commandName.ToLower()));

            if (foundConsoleAction is null) return false;

            ref var result = ref foundConsoleAction.ActionString;

            result += method;
            return true;
        }

        public bool TrySubscribeActionByName(string commandName, Action<int> method)
        {
            var foundConsoleAction =
                _consoleActions.FirstOrDefault(action => action.Name.ToLower().Contains(commandName.ToLower()));

            if (foundConsoleAction is null) return false;

            ref var result = ref foundConsoleAction.ActionInt;

            result += method;
            return true;
        }

        public bool TrySubscribeActionByName(string commandName, Action<float> method)
        {
            var foundConsoleAction =
                _consoleActions.FirstOrDefault(action => action.Name.ToLower().Contains(commandName.ToLower()));

            if (foundConsoleAction is null) return false;

            ref var result = ref foundConsoleAction.ActionFloat;

            result += method;
            return true;
        }
    }

    [Serializable]
    public class ConsoleAction
    {
        public string Name;
        public string Command;
        public string Description;
        public Action Action;
        public Action<string> ActionString;
        public Action<int> ActionInt;
        public Action<float> ActionFloat;
    }

}