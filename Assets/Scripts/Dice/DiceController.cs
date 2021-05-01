using Assets.Scripts.Config;
using UnityEngine;

namespace Scripts.Dice
{
    public class DiceController : MonoBehaviour
    {
        private ThrowState _state = ThrowState.WAITING;
        private Vector3 _offset;
        private Vector3 _velocity;

        private void Update()
        {
            if (_state == ThrowState.IN_HAND)
            {
                var oldT = transform.position;
                transform.position = _offset + UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z));
                _velocity = transform.position - oldT;
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
                var rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                Vector3 mov = _velocity * ConfigManager.S.Info.FireVelocity;
                mov.z = Mathf.Abs(_velocity.x + _velocity.y) * ConfigManager.S.Info.FireVelocity;
                rb.AddForce(mov, ForceMode.Impulse);
                rb.AddTorque(mov * 10f);
                _state = ThrowState.THROWN;
            }
        }
    }
}
