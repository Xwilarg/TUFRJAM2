using Scripts.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }

        private int _healthIndex = 0;

        public void TakeDamage()
        {
            if (_health.Length == 0) return;
            _health[_healthIndex].color = Color.gray;
            _healthIndex++;
            if (_healthIndex == _health.Length)
                GameOverManager.S.Loose();
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
