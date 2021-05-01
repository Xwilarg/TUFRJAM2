﻿using Scripts.Config;
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
                for (int i = 0; i < ConfigManager.S.Info.MasterNbDicesSpawn; i++)
                {
                    var go = Instantiate(ConfigManager.S.Info.DiceEnemy, transform.position, Quaternion.identity);
                    var rb = go.GetComponent<Rigidbody>();
                    rb.AddForce((Vector3.up + Vector3.right * Random.Range(.5f, 1f) + Vector3.forward * Random.Range(.5f, 1f)) * ConfigManager.S.Info.RelaunchForce, ForceMode.Impulse);
                    rb.AddTorque(Vector3.one * ConfigManager.S.Info.RelaunchTorque);
                    DiceManager.S.DiceCount++;
                }
                DiceManager.S.DiceCount--;
                Destroy(gameObject); ;
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
