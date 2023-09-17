using System.Collections;
using UnityEngine;

namespace MainGame.Infrastructure.Services.SceneLoader
{
    public class LoadingCurtainView : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _waitTime = 0.05f;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new(_waitTime);
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }
    
        public void Hide()
        {
            StartCoroutine(DoFadeIn());
        }

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _waitTime;
                yield return _wait;
            }
      
            gameObject.SetActive(false);
        }
    }
}