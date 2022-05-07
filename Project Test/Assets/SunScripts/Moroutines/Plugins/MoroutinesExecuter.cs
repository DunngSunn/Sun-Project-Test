using UnityEngine;

namespace Redcode.Moroutines
{
    internal sealed class MoroutinesExecuter : MonoBehaviour
    {
        internal static MoroutinesExecuter Instance { get; private set; }

        internal Owner Owner { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            Instance = new GameObject("[Redcode] MoroutinesExecuter").AddComponent<MoroutinesExecuter>();
            GameObject o;
            Instance.Owner = (o = Instance.gameObject).AddComponent<Owner>();

            o.hideFlags = HideFlags.HideInHierarchy;
            DontDestroyOnLoad(o);
        }
    }
}