using Scripts.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Config
{
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager S;

        private void Awake()
        {
            S = this;
        }

        public PlayerInfo Info;
    }
}
