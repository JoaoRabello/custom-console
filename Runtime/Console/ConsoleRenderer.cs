using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexFrogGames.Tools.Console
{
    public class ConsoleRenderer : WindowRenderer
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private List<TMP_Text> _intellisenseLabels = new List<TMP_Text>();
        [SerializeField] private List<TMP_Text> _commandHistory = new List<TMP_Text>();

        public Action<string> OnTextUpdate;
        public Action<string> OnTextSubmit;

        public override void RenderConsole()
        {
            base.RenderConsole();

            if (_isRendering) return;

            _inputField.onValueChanged.AddListener(TypeText);
            _inputField.onSubmit.AddListener(SubmitText);
            _inputField.SetTextWithoutNotify("");

            ClearIntellisenseCommands();

            _inputField.Select();

            _isRendering = true;
        }

        public override void HideConsole()
        {
            base.HideConsole();

            _inputField.onValueChanged.RemoveAllListeners();
            _inputField.onSubmit.RemoveAllListeners();
            _inputField.SetTextWithoutNotify("");

            ClearIntellisenseCommands();

            _isRendering = false;
        }

        private void TypeText(string text)
        {
            OnTextUpdate?.Invoke(text);
        }

        private void SubmitText(string text)
        {
            OnTextSubmit?.Invoke(text);
        }

        private void ClearIntellisenseCommands()
        {
            foreach (var intellisenseLabel in _intellisenseLabels)
            {
                intellisenseLabel.gameObject.SetActive(false);
            }
        }

        public void ListIntellisenseCommands(List<ConsoleAction> consoleActionsList)
        {
            ClearIntellisenseCommands();
            for (var index = 0; index < consoleActionsList.Count; index++)
            {
                var consoleAction = consoleActionsList[index];

                _intellisenseLabels[index].gameObject.SetActive(true);
                _intellisenseLabels[index].SetText(consoleAction.Command);
            }
        }
    }

}