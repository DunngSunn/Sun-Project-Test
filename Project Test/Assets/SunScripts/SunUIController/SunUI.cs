using UnityEngine;

namespace DunnGSunn
{
    public abstract class SunUI : MonoBehaviour
    {
        #region Touch control

        protected float lastTimeClick;
        
        protected virtual bool CanClick
        {
            get
            {
                var result = Time.unscaledTime > lastTimeClick + 0.5f;
                if (result) lastTimeClick = Time.unscaledTime;
                return result;
            }
        }

        #endregion

        #region Fields

        [Header("===== Debug =====")] 
        [SerializeField] private bool isDebug;

        [Header("===== Initialize =====")] 
        [SerializeField] private bool initOnAwake;

        [Header("===== Show =====")] 
        [SerializeField] private bool usingTweenShow;
        [HideIf(variable: nameof(usingTweenShow), state: false)]
        public SunTweenControl tweenShow;
        
        [Header("===== Hide =====")]
        [SerializeField] private bool usingTweenHide;
        [HideIf(variable: nameof(usingTweenHide), state: false)]
        public SunTweenControl tweenHide;

        public bool IsShow { get; private set; }

        #endregion

        #region Unity callback functions

        private void Awake()
        {
            Application.targetFrameRate = 60;
            if (initOnAwake) Initialize();
            IsShow = false;
        }

        private void OnDestroy()
        {
            Destroy();
        }

        #endregion

        #region Override functions

        public virtual void Initialize() { }

        public virtual void Show()
        {
            if (IsShow) return;
            if (isDebug) Debug.Log("Show " + gameObject.name);
            
            lastTimeClick = Time.unscaledTime;
            if (!usingTweenShow)
                gameObject.SetActive(true);
            else
                tweenShow.PlayForward();

            IsShow = true;
        }

        public virtual void Hide()
        {
            if (!IsShow) return;
            if (isDebug) Debug.Log("Hide " + gameObject.name);
            
            lastTimeClick = Time.unscaledTime;
            if (!usingTweenHide) 
                gameObject.SetActive(false);
            else 
                tweenHide.PlayReverse();
            
            IsShow = false;
        }

        public virtual void Destroy() { }

        #endregion
    }
}