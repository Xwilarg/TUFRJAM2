using Assets.Scripts.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dice
{
    public class DiceController : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private ThrowState _state = ThrowState.WAITING;
        private Vector3 _offset;
        private Vector3 _velocity;

        Dictionary<Vector3, int> directions = new Dictionary<Vector3, int>
        {
            { Vector3.up, 0 },
            { Vector3.forward, 1 },
            { Vector3.right, 2 },
            { Vector3.down, 3 },
            { Vector3.back, 4 },
            { Vector3.left, 5 }
        };

        private void Update()
        {
            if (_state == ThrowState.IN_HAND)
            {
                var oldT = transform.position;
                transform.position = _offset + UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -UnityEngine.Camera.main.transform.position.z));
                _velocity = transform.position - oldT;
            }

            if (_state == ThrowState.THROWN && _rb.velocity.magnitude < .1f && transform.position.y < 2f)
            {
                // From https://answers.unity.com/questions/1215416/rolling-a-3d-dice-detect-which-number-faces-up.html
                Vector3 referenceVectorUp = Vector3.up;
                float epsilonDeg = 5f;
                Vector3 referenceObjectSpace = transform.InverseTransformDirection(referenceVectorUp);
                float min = float.MaxValue;
                Vector3 minKey = Vector3.zero;
                foreach (Vector3 key in directions.Keys)
                {
                    float a = Vector3.Angle(referenceObjectSpace, key);
                    if (a <= epsilonDeg && a < min)
                    {
                        min = a;
                        minKey = key;
                    }
                }
                Debug.Log((min < epsilonDeg) ? directions[minKey] % 2 : -1);
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
    }
}
