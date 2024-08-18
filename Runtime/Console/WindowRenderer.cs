using UnityEngine;

namespace HexFrogGames.Tools.Console
{
    public class WindowRenderer : MonoBehaviour
    {
        [SerializeField] protected GameObject _content;

        protected bool _isRendering;

        public virtual void RenderConsole()
        {
            if (_isRendering) return;

            _content.SetActive(true);
        }

        public virtual void HideConsole()
        {
            _content.SetActive(false);
            _isRendering = false;
        }
    }
}
