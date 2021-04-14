using System;
using System.Collections;
using UnityEngine;

namespace RPG.SceneManagemnet
{
    [CreateAssetMenu(fileName = "Fader Instance", menuName = "ScriptableObjects/Create fader instance")]
    class Fader : ScriptableObject
    {
        [SerializeField]
        private Canvas canvasPrefab;

        [Range(0, 1f)]
        [SerializeField] float fadeOutPace = 0.5f;

        
        private void Awake()
        {

        }


        public IEnumerator FadeInEffect()
        {
            CanvasGroup canvasGroup = GetCanvasGroup();
            canvasGroup.alpha = 0f;
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += fadeOutPace * Time.deltaTime;

                yield return null;
            }
        }

        private CanvasGroup GetCanvasGroup()
        {
            Canvas canvas = Instantiate(canvasPrefab) as Canvas;
            return canvas.GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOutEffect()
        {
            CanvasGroup canvasGroup = GetCanvasGroup();
            canvasGroup.alpha = 1f;
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= fadeOutPace * Time.deltaTime;

                yield return null;
            }
        }
    }
}
