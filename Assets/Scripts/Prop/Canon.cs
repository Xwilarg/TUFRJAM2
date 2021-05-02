using Scripts.Config;
using System.Collections;
using UnityEngine;

namespace Scripts.Prop
{
    public class Canon : MonoBehaviour
    {
        [SerializeField]
        private GameObject _goal;

        [SerializeField]
        private GameObject _ball;

        private void Start()
        {
            if (transform.position.z < 15f) transform.Rotate(0f, 180f, 0f);
            StartCoroutine(SpawnAll());
        }

        private IEnumerator SpawnAll()
        {
            var go = Instantiate(_goal, transform.position + -transform.forward * 2 + transform.up * 2f, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce((-transform.forward + transform.up) * ConfigManager.S.Info.CanonForce, ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
            Instantiate(_ball, transform.position + transform.up * 2f, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
