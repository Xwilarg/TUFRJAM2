using Scripts.Enemy;
using System.Linq;
using UnityEngine;

namespace Scripts.Prop
{
    public class Ball : MonoBehaviour
    {
        private void Start()
        {
            foreach (var e in GameObject.FindGameObjectsWithTag("Enemy").Select(x => x.GetComponent<EnemyController>()))
            {
                e.Ball = transform;
            }
        }
    }
}
