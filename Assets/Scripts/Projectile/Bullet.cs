using Scripts.Enemy;
using Scripts.Player;
using Scripts.Prop;
using UnityEngine;

namespace Scripts.Projectile
{
    public class Bullet : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        public bool IsFire = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.transform.parent.GetComponent<PlayerController>().TakeDamage();
            }
            else if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<EnemyController>().Stun(transform.position);
            }
            else if (IsFire && collision.collider.CompareTag("Crate"))
            {
                collision.collider.GetComponent<Crate>().TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}
