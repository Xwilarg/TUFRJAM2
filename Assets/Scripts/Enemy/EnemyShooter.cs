using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyShooter : AEnemyAI
    {
        public override void Update()
        {
            Enemy.gameObject.transform.LookAt(Enemy.Player.transform, Vector3.up);
            Enemy.gameObject.transform.rotation = Quaternion.Euler(0f, Enemy.gameObject.transform.rotation.eulerAngles.y, 0f);
        }
    }
}
