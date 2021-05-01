using Assets.Scripts.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;

        [SerializeField]
        private Image[] _health;

        [SerializeField]
        private Transform _gunEnd;

        [SerializeField]
        private GameObject _bullet;

        private int _healthIndex = 0;

        private bool _canMove = false;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            GameObject.FindGameObjectWithTag("Spawn").SetActive(false);
            _rb.AddForce(Vector3.down * 30f, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_canMove) return;

            if (collision.collider.CompareTag("Floor"))
            {
                _canMove = true;
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

            _rb.velocity = new Vector3(Input.GetAxis("Horizontal") * ConfigManager.S.Info.Speed, _rb.velocity.y, Input.GetAxis("Vertical") * ConfigManager.S.Info.Speed);
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
            }
        }

        public void TakeDamage()
        {
            if (_health.Length == 0) return;
            _health[_healthIndex].color = Color.gray;
            _healthIndex++;
        }
    }
}