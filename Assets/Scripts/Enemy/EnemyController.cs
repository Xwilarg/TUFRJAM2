using Scripts.Player;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private int _health = 2;

        private AEnemyAI _ai;

        public PlayerController Player;

        public void TakeDamage()
        {
            _health--;
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _ai = new EnemyShooter
            {
                Enemy = this
            };
        }

        private void Update()
        {
            _ai.Update();
        }
    }
}
