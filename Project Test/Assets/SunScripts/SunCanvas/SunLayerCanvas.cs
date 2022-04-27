using UnityEngine;

namespace DunnGSunn
{
    public abstract class SunLayerCanvas<T> : MonoBehaviour where T : SunLayerCanvas<T>
    {
        #region Fields

        [Header("===== Destroy on load =====")]
        [SerializeField] private bool dontDestroyOnLoad;

        [Header("===== Canvas =====")] 
        [SerializeField] private Canvas thisLayerCanvas;

        public static T Instance { get; private set; }

        #endregion

        #region Unity callback functions

        private void Reset()
        {
            dontDestroyOnLoad = true;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            if (thisLayerCanvas != null)
            {
                thisLayerCanvas = GetComponent<Canvas>();
            }
            
            if (thisLayerCanvas != null && SunCameraUI.Instance.CameraUI != null)
            {
                thisLayerCanvas.renderMode = RenderMode.ScreenSpaceCamera;
                thisLayerCanvas.worldCamera = SunCameraUI.Instance.CameraUI;
            }
            
            LoadInStart();
        }

        protected virtual void LoadInStart()
        {
            
        }

        #endregion
    }
}