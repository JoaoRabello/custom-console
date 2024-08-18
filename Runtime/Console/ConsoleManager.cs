using System;
using UnityEngine;

namespace HexFrogGames.Tools.Console
{
    public class ConsoleManager : MonoBehaviour
    {
        public static ConsoleManager Instance;

        [SerializeField] private ConsoleActions _consoleActions;
        [SerializeField] private ConsoleRenderer _consoleRenderer;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (Instance is null)
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            _consoleRenderer.OnTextUpdate += CheckForConsoleActions;
            _consoleRenderer.OnTextSubmit += SubmitCommand;

            // You should use your own input reading methods to setup the Open and Close events.
            // Here's our own example:
            
            // InputManager.Instance.OnOpenConsolePerformed += OpenConsole;
            // InputManager.Instance.OnClosePerformed += CloseConsole;
        }

        private void OnDisable()
        {
            _consoleRenderer.OnTextUpdate -= CheckForConsoleActions;
            _consoleRenderer.OnTextSubmit -= SubmitCommand;

            // InputManager.Instance.OnOpenConsolePerformed -= OpenConsole;
            // InputManager.Instance.OnClosePerformed -= CloseConsole;
        }

        private void OpenConsole()
        {
            _consoleRenderer.RenderConsole();
            // InputManager.Instance.LockAllInputsExceptConsole(true);
        }

        private void CloseConsole()
        {
            _consoleRenderer.HideConsole();
            // InputManager.Instance.LockAllInputsExceptConsole(false);
        }

        private void SubmitCommand(string text)
        {
            var parameters = text.Split(' ');

            if (!_consoleActions.TryGetConsoleActionBySubString(parameters[0], out var result))
            {
                Debug.LogError($"[ConsoleManager] SubmitCommand | Couldn't find the submitted command");
                return;
            }

            if (result[0].Action != null)
            {
                result[0].Action?.Invoke();
            }
            else if (result[0].ActionString != null)
            {
                result[0].ActionString?.Invoke(parameters[1]);
            }
            else if (result[0].ActionInt != null)
            {
                if (!int.TryParse(parameters[1], out int parameter))
                {
                    Debug.LogError("[ConsoleManager] SubmitCommand | Couldn't read the first parameter as int");
                    return;
                }

                result[0].ActionInt?.Invoke(parameter);
            }
            else if (result[0].ActionFloat != null)
            {
                if (!float.TryParse(parameters[1], out float parameter))
                {
                    Debug.LogError("[ConsoleManager] SubmitCommand | Couldn't read the first parameter as float");
                    return;
                }

                result[0].ActionFloat?.Invoke(parameter);
            }

            CloseConsole();
        }

        private void CheckForConsoleActions(string text)
        {
            if (!_consoleActions.TryGetConsoleActionBySubString(text, out var consoleActions)) return;

            _consoleRenderer.ListIntellisenseCommands(consoleActions);
        }

        public bool TrySubscribeActionByName(string commandName, Action methodToSubscribe)
        {
            return _consoleActions.TrySubscribeActionByName(commandName, methodToSubscribe);
        }

        public bool TrySubscribeActionByName(string commandName, Action<string> methodToSubscribe)
        {
            return _consoleActions.TrySubscribeActionByName(commandName, methodToSubscribe);
        }

        public bool TrySubscribeActionByName(string commandName, Action<int> methodToSubscribe)
        {
            return _consoleActions.TrySubscribeActionByName(commandName, methodToSubscribe);
        }

        public bool TrySubscribeActionByName(string commandName, Action<float> methodToSubscribe)
        {
            return _consoleActions.TrySubscribeActionByName(commandName, methodToSubscribe);
        }
    }
}
