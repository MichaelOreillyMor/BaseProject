
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GFFramework.UI
{
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