using Scripts.Camera;
using Scripts.Config;
using Scripts.Enemy;
using Scripts.Projectile;
using Scripts.Prop;
using System.Linq;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;

        [SerializeField]
        private Transform _gunEnd;

        [SerializeField]
        private GameObject _bullet;

        [SerializeField]
        private GameObject _cameraPrefab;

        private bool _canMove = false;
        public bool IsFire = false;

        private void Start()
        {
            foreach (var e in GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()))
            {
                e.Player = this;
            }

            GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>().SetBall();
            GameObject.FindGameObjectWithTag("Goal").GetComponent<Goal>().SetGoal();

            _rb = GetComponent<Rigidbody>();
            GameObject.FindGameObjectWithTag("Spawn").SetActive(false);
            _rb.AddForce(Vector3.down * 30f, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_canMove)
            {
                if (collision.collider.CompareTag("Ball"))
                {
                    var dir = collision.collider.transform.position - transform.position;
                    dir.y = Mathf.Abs(new Vector2(dir.x, dir.z).magnitude);
                    collision.collider.GetComponent<Rigidbody>().AddForce(dir * ConfigManager.S.Info.FeetForce, ForceMode.Impulse);
                }
            }
            else
            {
                if (collision.collider.CompareTag("Floor"))
                {
                    _canMove = true;
                    Destroy(UnityEngine.Camera.main.gameObject);
                    var go = Instantiate(
                        _cameraPrefab,
                        new Vector3(_cameraPrefab.transform.position.x, _cameraPrefab.transform.position.y, transform.position.z),
                        _cameraPrefab.transform.rotation);
                    go.GetComponent<Follow>()._toFollow = transform;
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

            _rb.velocity = new Vector3(Input.GetAxis("Vertical") * ConfigManager.S.Info.Speed, _rb.velocity.y, -Input.GetAxis("Horizontal") * ConfigManager.S.Info.Speed);
        }

        private void Update()
        {
            if (!_canMove) return;

            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.LookAt(hit.point, Vector3.up);
                var rot = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0f, rot.y, 0f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                var go = Instantiate(_bullet, _gunEnd.position, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(transform.forward * ConfigManager.S.Info.FireVelocity, ForceMode.Impulse);
                go.GetComponent<Bullet>().IsFire = IsFire;
            }
        }

        public void TakeDamage()
            => HealthManager.S.TakeDamage();

        public bool GainHealth()
            => HealthManager.S.GainHealth();
    }
}