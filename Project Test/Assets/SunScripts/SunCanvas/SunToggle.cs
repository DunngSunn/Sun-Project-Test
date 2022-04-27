using UnityEngine;
using UnityEngine.UI;

namespace DunnGSunn
{
    public class SunToggle : MonoBehaviour
    {
        #region Fields

        private Toggle _toggle;

        [Header("===== Using tween or not =====")]
        public bool useTween;
        
        [Space]
        [HideIf(nameof(useTween), false)]
        public SunTween toggleTweenObjectActive;
        [HideIf(nameof(useTween), false)]
        public SunTween toggleTweenObjectDeactive;
        [HideIf(nameof(useTween), false)]
        public Graphic graphicsObjectActive;
        [HideIf(nameof(useTween), false)]
        public Graphic graphicsObjectDeactive;
        
        [Space]
        [HideIf(nameof(useTween), true)]
        public GameObject toggleObjectActive;
        [HideIf(nameof(useTween), true)]
        public GameObject toggleObjectDeactive;

        #endregion

        #region Unity callback functions

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }
        
        private void Reset()
        {
            if (useTween)
            {
                toggleTweenObjectDeactive = transform.Find("Deactive").GetComponent<SunTween>();
                toggleTweenObjectActive = transform.Find("Active").GetComponent<SunTween>();

                toggleObjectDeactive = null;
                toggleObjectActive = null;
            }
            else
            {
                toggleObjectDeactive = transform.Find("Deactive").gameObject;
                toggleObjectActive = transform.Find("Active").gameObject;

                toggleTweenObjectDeactive = null;
                toggleTweenObjectActive = null;
            }
        }
        
        private void Start()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        #endregion

        #region Toggle control functions

        public void InitializeToggle()
        {
            if (useTween)
            {
                toggleTweenObjectDeactive.gameObject.SetActive(true);
                toggleTweenObjectActive.gameObject.SetActive(false);
                
                graphicsObjectActive.color = Color.white;
                graphicsObjectDeactive.color = Color.white;

                _toggle.isOn = false;
            }
            else
            {
                toggleObjectDeactive.SetActive(true);
                toggleObjectActive.SetActive(false);
                
                _toggle.isOn = false;
            }
        }

        private void OnToggleValueChanged(bool value)
        {
            if (useTween)
            {
                if (value)
                {
                    toggleTweenObjectActive.PlayForward();
                    toggleTweenObjectDeactive.PlayReverse();
                }
                else
                {
                    toggleTweenObjectActive.PlayReverse();
                    toggleTweenObjectDeactive.PlayForward();
                }
            }
            else
            {
                toggleObjectActive.SetActive(value);
                toggleObjectDeactive.SetActive(!value);
            }
        }

        #endregion
    }
}