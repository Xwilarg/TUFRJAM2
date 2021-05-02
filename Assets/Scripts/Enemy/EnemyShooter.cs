using Scripts.Config;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyShooter : AEnemyAI
    {
        private ShootState _state = ShootState.WAITING;
        public override void Update()
        {
            if (_state == ShootState.WAITING || _state == ShootState.RELOAD) // Follow the player with its gaze
            {
                if (Enemy.Ball != null && Vector3.Distance(Enemy.Ball.position, Enemy.gameObject.transform.position) < ConfigManager.S.Info.MinDistanceWithBall)
                {
                    Enemy.gameObject.transform.LookAt(Enemy.Ball, Vector3.up);
                    Enemy.gameObject.transform.rotation = Quaternion.Euler(0f, Enemy.gameObject.transform.rotation.eulerAngles.y, 0f);
                    Enemy.Rb.velocity = Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                }
                else if (Enemy.Player != null)
                {
                    Enemy.gameObject.transform.LookAt(Enemy.Player.transform, Vector3.up);
                    Enemy.gameObject.transform.rotation = Quaternion.Euler(0f, Enemy.gameObject.transform.rotation.eulerAngles.y, 0f);
                    var dist = Vector3.Distance(Enemy.Player.transform.position, Enemy.gameObject.transform.position);
                    if (dist < ConfigManager.S.Info.MinDistanceWithPlayer)
                    {
                        Enemy.Rb.velocity = -Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                    }
                    else if (dist > ConfigManager.S.Info.MaxDistanceWithPlayer)
                    {
                        Enemy.Rb.velocity = Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                    }
                }
            }
        }
    }
}
