using System.Collections.Generic;
using UnityEngine;

namespace DunnGSunn
{
    [DisallowMultipleComponent]
    public class SunUIController : SunMonoSingleton<SunUIController>
    {
        #region Fields

        [Header("===== All screen =====")]
        [SerializeField] private List<SunUI> uiScreens;
        
        [Header("===== Starting screen =====")]
        [SerializeField] private SunUI startingScreen;

        private static Stack<SunUI> UIStack = new Stack<SunUI>();
        
        #endregion

        #region UI control

        /// <summary>
        /// Lấy UI trong list
        /// </summary>
        public static T GetScreen<T>() where T : SunUI
        {
            foreach (var gameUI in Instance.uiScreens)
            {
                if (gameUI is T tScreen)
                {
                    return tScreen;
                }
            }

            return null;
        }

        /// <summary>
        /// Kiểm tra UI có trong stack không?
        /// </summary>
        public static bool IsScreenInStack<T>() where T : SunUI
        {
            return UIStack.Contains(GetScreen<T>());
        }

        /// <summary>
        /// Kiểm tra UI có ở đầu stack không?
        /// </summary>
        public static bool IsScreenOnTopOfStack<T>() where T : SunUI
        {
            return UIStack.Count > 0 && GetScreen<T>() == UIStack.Peek();
        }

        /// <summary>
        /// Hiển thị UI khởi đầu
        /// </summary>
        public static void PushStartingScreen(SunUI startingScreen)
        {
            startingScreen.Show();
            UIStack.Push(startingScreen);
        }

        /// <summary>
        /// Hiển thị UI mới, ẩn UI cũ
        /// </summary>
        public static void PushScreen<T>(bool hideCurrentScreen) where T : SunUI
        {
            var thisScreen = GetScreen<T>();
            thisScreen.Show();

            if (UIStack.Count > 0)
            {
                var currentScreen = UIStack.Peek();
                if (hideCurrentScreen && currentScreen != null) 
                    currentScreen.Hide();
            }

            UIStack.Push(thisScreen);
        }
        
        /// <summary>
        /// Hiển thị UI mới, ẩn UI cũ, ẩn UI cũ hơn
        /// </summary>
        public static void PushScreen<T>(bool hideCurrentScreen, bool hideSecondScreen) where T : SunUI
        {
            var thisScreen = GetScreen<T>();
            thisScreen.Show();

            if (UIStack.Count > 0)
            {
                var currentScreen = UIStack.Pop();
                var secondScreen = UIStack.Peek();

                if (hideSecondScreen && secondScreen != null) secondScreen.Hide();
                if (hideCurrentScreen && currentScreen != null) currentScreen.Hide();

                UIStack.Push(currentScreen);
            }

            UIStack.Push(thisScreen);
        }

        /// <summary>
        /// Ẩn UI đang hiển thị và trở về UI trước đó
        /// </summary>
        public static void PopScreen()
        {
            if (UIStack.Count > 1)
            {
                var currentScreen = UIStack.Pop();
                currentScreen.Hide();

                var newCurrentScreen = UIStack.Peek();
                newCurrentScreen.Show();
            }
        }
        
        /// <summary>
        /// Ẩn UI đang hiển thị và trở về UI trước đó, hiển thị UI cũ
        /// </summary>
        public static void PopScreen(bool showSecondScreen)
        {
            if (UIStack.Count > 1)
            {
                var currentScreen = UIStack.Pop();
                currentScreen.Hide();

                var newCurrentScreen = UIStack.Pop();
                newCurrentScreen.Show();

                var secondScreen = UIStack.Peek();
                if (showSecondScreen) secondScreen.Show();

                UIStack.Push(newCurrentScreen);
            }
        }

        /// <summary>
        /// Ẩn tất cả UI cho đến UI đầu
        /// </summary>
        public static void PopAllScreen()
        {
            for (var i = 0; i < UIStack.Count; i++)
            {
                PopScreen();
            }
        }

        #endregion

        #region Unity callback functions

        private void Start()
        {
            foreach (var sunUI in uiScreens)
            {
                sunUI.gameObject.SetActive(true);
                sunUI.Initialize();
                sunUI.gameObject.SetActive(false);
            }
            
            if (startingScreen != null)
            {
                PushStartingScreen(startingScreen);
            }
        }

        #endregion
    }
}