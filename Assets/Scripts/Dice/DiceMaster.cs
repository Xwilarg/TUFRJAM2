using Scripts.Config;
using UnityEngine;

namespace Scripts.Dice
{
    public class DiceMaster : MonoBehaviour
    {
        protected Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private ThrowState _state = ThrowState.WAITING;
        private Vector3 _offset;
        private Vector3 _velocity;

        private int _spawnOffset = 1;

        private void Update()
        {
            if (_state == ThrowState.IN_HAND)
            {
                var oldT = transform.position;
                transform.position = _offset + UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z));
                _velocity = transform.position - oldT;
            }

            if (_state == ThrowState.THROWN && _rb.velocity.magnitude < .1f && transform.position.y < 1.5f)
            {
                SpawnDices(1, ConfigManager.S.Info.DiceObjective);
                SpawnDices(ConfigManager.S.Info.MasterNbDicesSpawn, ConfigManager.S.Info.DiceEnemy);
                SpawnDices(ConfigManager.S.Info.MasterNbDicesSpawn / 2, ConfigManager.S.Info.DiceAllie);
                SpawnDices(ConfigManager.S.Info.MasterNbDicesSpawn, ConfigManager.S.Info.DiceNeutral);
                DiceManager.S.DiceCount--;
                Destroy(gameObject);
            }
        }

        private void SpawnDices(int count, GameObject prefab)
        {
            for (int i = 0; i < count; i++)
            {
                var go = Instantiate(prefab, transform.position, Quaternion.identity);
                var rb = go.GetComponent<Rigidbody>();
                rb.AddForce(((Vector3.up * _spawnOffset) + Vector3.right * Random.Range(-15f, 15f) + Vector3.forward * Random.Range(-15f, 15f)) * ConfigManager.S.Info.RelaunchForce, ForceMode.Impulse);
                rb.AddTorque(Vector3.one * ConfigManager.S.Info.RelaunchTorque);
                DiceManager.S.DiceCount++;
                _spawnOffset++;
            }
        }

        private void OnMouseDown()
        {
            if (_state == ThrowState.WAITING)
            {
                _offset = transform.position - UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z));
                _state = ThrowState.IN_HAND;
            }
        }

        private void OnMouseUp()
        {
            if (_state == ThrowState.IN_HAND)
            {
                _rb.isKinematic = false;
                Vector3 mov = _velocity * ConfigManager.S.Info.FireVelocity;
                mov.z = Mathf.Abs(_velocity.x + _velocity.y) * ConfigManager.S.Info.FireVelocity;
                _rb.AddForce(mov, ForceMode.Impulse);
                _rb.AddTorque(mov * 10f);
                _state = ThrowState.THROWN;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                _rb.velocity = -_rb.velocity;
            }
        }
    }
}
