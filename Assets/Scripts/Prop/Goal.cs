using Scripts.Enemy;
using Scripts.GameOver;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                GameOverManager.S.Win();
            }
        }
    }
}
