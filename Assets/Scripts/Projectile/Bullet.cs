using Scripts.Enemy;
using Scripts.Player;
using UnityEngine;

namespace Scripts.Projectile
{
    public class Bullet : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                collision.collider.GetComponent<PlayerController>().TakeDamage();
            }
            else if (collision.collider.CompareTag("Enemy"))
            {
                collision.collider.GetComponent<EnemyController>().TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}
