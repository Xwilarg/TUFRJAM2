using Assets.Scripts.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Dice.DiceImpl
{
    public class DiceSpawn : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _toSpawn;

        protected Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            StartCoroutine(LandTimer());
        }

        Dictionary<Vector3, int> _directions = new Dictionary<Vector3, int>
        {
            { Vector3.up, 0 },
            { Vector3.forward, 1 },
            { Vector3.right, 2 },
            { Vector3.down, 3 },
            { Vector3.back, 4 },
            { Vector3.left, 5 }
        };

        private bool _canLand = false;

        private IEnumerator LandTimer()
        {
            yield return new WaitForSeconds(1f);
            _canLand = true;
        }

        private void Update()
        {
            if (_canLand == true && _rb.velocity.magnitude < .1f && transform.position.y < 1.5f)
            {
                // From https://answers.unity.com/questions/1215416/rolling-a-3d-dice-detect-which-number-faces-up.html
                Vector3 referenceVectorUp = Vector3.up;
                float epsilonDeg = 15f;
                Vector3 referenceObjectSpace = transform.InverseTransformDirection(referenceVectorUp);
                float min = float.MaxValue;
                Vector3 minKey = Vector3.zero;
                foreach (Vector3 key in _directions.Keys)
                {
                    float a = Vector3.Angle(referenceObjectSpace, key);
                    if (a <= epsilonDeg && a < min)
                    {
                        min = a;
                        minKey = key;
                    }
                }
                var value = (min < epsilonDeg) ? _directions[minKey] : -1;
                if (value == -1)
                {
                    _rb.AddForce((Vector3.up + Vector3.right * Random.Range(-1f, 1f) + Vector3.forward * Random.Range(-1f, 1f)) * ConfigManager.S.Info.RelaunchForce, ForceMode.Impulse);
                    _rb.AddTorque(Vector3.one * ConfigManager.S.Info.RelaunchTorque);
                }
                else
                {
                    var go = Instantiate(_toSpawn[value], transform.position, _toSpawn[value].transform.rotation);
                    var rb = go.GetComponent<Rigidbody>();
                    rb.AddForce((Vector3.up) * ConfigManager.S.Info.RelaunchForce, ForceMode.Impulse);
                    DiceManager.S.DiceCount--;
                    Destroy(gameObject);
                }
            }
        }
    }
}
