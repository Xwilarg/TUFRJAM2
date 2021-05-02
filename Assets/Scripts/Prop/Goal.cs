using Scripts.Enemy;
using System.Linq;
using UnityEngine;

namespace Scripts.Prop
{
    public class Goal : MonoBehaviour
    {
        public void SetGoal()
        {
            foreach (var e in GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()))
            {
                e.Goal = transform;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
