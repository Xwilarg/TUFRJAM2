using UnityEngine;

namespace Scripts.Camera
{
    public class Follow : MonoBehaviour
    {
        public Transform _toFollow;

        private Vector3 _offset;

        private void Start()
        {
            _offset = _toFollow.position - transform.position;
        }

        private void Update()
        {
            transform.position = _toFollow.transform.position - _offset;
        }
    }
}
