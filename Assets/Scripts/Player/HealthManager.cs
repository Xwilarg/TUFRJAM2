using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class HealthManager : MonoBehaviour
    {
        public static HealthManager S;

        private void Awake()
        {
            S = this;
        }

        [SerializeField]
        private Image[] _health;

        private int _healthIndex = 0;

        public void TakeDamage()
        {
            if (_health.Length == 0) return;
            if (_healthIndex == _health.Length) return;
            _health[_healthIndex].color = Color.gray;
            _healthIndex++;
        }

        public bool GainHealth()
        {
            if (_health.Length == 0) return true;
            if (_healthIndex == 0) return false;

            _health[_healthIndex].color = Color.red;
            _healthIndex--;
            return true;
        }
    }
}
