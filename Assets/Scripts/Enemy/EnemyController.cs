using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private int _health = 2;

        public void TakeDamage()
        {
            _health--;
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
