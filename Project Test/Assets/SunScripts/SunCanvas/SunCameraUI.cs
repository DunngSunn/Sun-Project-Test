using UnityEngine;

namespace DunnGSunn
{
    public class SunCameraUI : SunMonoSingleton<SunCameraUI>
    {
        #region Fields

        [Header("===== Depth of camera =====")]
        public int depth = 10;

        public Camera CameraUI { get; private set; }

        #endregion

        private void Reset()
        {
            depth = 10;
            SetDontDestroyOnLoad(true);
        }

        protected override void LoadInAwake()
        {
            CameraUI = gameObject.GetOrAddComponent<Camera>();
            CameraUI.depth = depth;
        }
    }
}
