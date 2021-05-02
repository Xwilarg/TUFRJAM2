using Scripts.Player;
using UnityEngine;

namespace Scripts.Prop
{
    public class Collectible : MonoBehaviour
    {
        private void Update()
        {
            if (transform.position.y < .75f)
            {
                transform.position = new Vector3(transform.position.x, .75f, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            transform.Rotate(0f, 2f, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.transform.parent.GetComponent<PlayerController>().GainHealth())
            {
                Destroy(gameObject);
            }
        }
    }
}
