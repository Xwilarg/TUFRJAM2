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

        public Rigidbody Rb;

        public Transform Ball;

        public Transform Bullet;

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
            ToggleAim(false);
            Rb = GetComponent<Rigidbody>();
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                var dir = collision.collider.transform.position - transform.position;
                dir.y = Mathf.Abs(new Vector2(dir.x, dir.z).magnitude) / 2f;
                collision.collider.GetComponent<Rigidbody>().AddForce(dir * ConfigManager.S.Info.FeetForce, ForceMode.Impulse);
            }
        }
    }
}
