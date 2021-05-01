using Scripts.Config;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private int _health = 2;

        private AEnemyAI _ai;

        public PlayerController Player;

        private LineRenderer _ln;

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
            _ln = GetComponentInChildren<LineRenderer>();
            _ai = new EnemyShooter
            {
                Enemy = this
            };
        }

        private Vector3 _aimDest;
        public void ToggleAim(bool value)
        {
            _ln.enabled = value;
        }

        private void Update()
        {
            _ai.Update();
            if (_ln.enabled)
            {
                if (Physics.Raycast(transform.position + transform.forward, transform.forward, out RaycastHit hit))
                {
                    _ln.SetPositions(new[] {
                        transform.position,
                        hit.point
                    });
                }
                else
                {
                    _ln.SetPositions(new[] {
                        transform.position,
                        transform.position + transform.forward * 100f
                    });
                }
            }
        }
    }
}
