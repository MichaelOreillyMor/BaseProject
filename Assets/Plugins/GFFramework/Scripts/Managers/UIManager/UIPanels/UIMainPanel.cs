using UnityEngine;
using UnityEngine.UI;

namespace GFF.UIsMan.Panels
{
    /// <summary>
    /// Main UIpanel that contains some UI utilities that all games need
    /// </summary>
    public class UIMainPanel : MonoBehaviour
    {
        [SerializeField]
        private Image fadePanel;

        [SerializeField]
        private RectTransform content;

        public void FadeIn()
        {
            fadePanel.gameObject.SetActive(true);
        }

        public void FadeOut()
        {
            fadePanel.gameObject.SetActive(false);
        }

        public void AddContent(Transform panelTr)
        {
            panelTr.SetParent(content);
        }
    }
}