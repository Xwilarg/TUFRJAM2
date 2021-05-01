using Scripts.Config;
using Scripts.Enemy;
using Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Prop
{
    public class Mine : MonoBehaviour
    {
        private float _radius;

        private void Start()
        {
            _radius = GetComponent<SphereCollider>().radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Floor")) return;
            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(ConfigManager.S.Info.TimeBeforeExplosion);
            foreach (var col in Physics.OverlapSphere(transform.position, _radius))
            {
                if (!col.gameObject.CompareTag("Floor"))
                {
                    if (col.gameObject.CompareTag("Player"))
                    {
                        col.gameObject.GetComponent<PlayerController>().TakeDamage();
                    }
                    else if (col.gameObject.CompareTag("Enemy"))
                    {
                        col.gameObject.GetComponent<EnemyController>().TakeDamage();
                    }
                    var vel = transform.position - col.transform.position;
                    vel.x = _radius - vel.x;
                    vel.z = _radius - vel.z;
                    vel.y = Mathf.Abs(new Vector2(vel.x, vel.z).magnitude);
                    col.gameObject.GetComponent<Rigidbody>()?.AddForce(vel * ConfigManager.S.Info.ExplosionForce, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}
