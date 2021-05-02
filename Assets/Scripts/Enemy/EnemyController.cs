using Scripts.Config;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        private AEnemyAI _ai;

        public PlayerController Player;

        private LineRenderer _ln;

        public Rigidbody Rb;

        public Transform Ball, Goal;

        public Transform Bullet;

        private float _stunTimer = 0f;

        public void Stun(Vector3 other)
        {
            _stunTimer = ConfigManager.S.Info.StunTime;
            Rb.AddForce((transform.position - other) * ConfigManager.S.Info.StunForce, ForceMode.Impulse);
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
            if (_stunTimer > 0f)
            {
                _stunTimer -= Time.deltaTime;
                return;
            }
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
                dir.y = Mathf.Abs(new Vector2(dir.x, dir.z).magnitude);
                collision.collider.GetComponent<Rigidbody>().AddForce(dir * ConfigManager.S.Info.FeetForce, ForceMode.Impulse);
            }
        }
    }
}
