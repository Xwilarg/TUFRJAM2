using Scripts.Config;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyShooter : AEnemyAI
    {
        private float _timer = 3f;

        private ShootState _state = ShootState.WAITING;
        public override void Update()
        {
            if (_state == ShootState.WAITING || _state == ShootState.RELOAD || _state == ShootState.AIM)
            {
                Enemy.gameObject.transform.LookAt(Enemy.Player.transform, Vector3.up);
                Enemy.gameObject.transform.rotation = Quaternion.Euler(0f, Enemy.gameObject.transform.rotation.eulerAngles.y, 0f);
            }
            if (_state == ShootState.WAITING || _state == ShootState.RELOAD) // Follow the player with its gaze
            {
                if (Enemy.Ball != null && Vector3.Distance(Enemy.Ball.position, Enemy.gameObject.transform.position) < ConfigManager.S.Info.MinDistanceWithBall)
                {
                    Enemy.gameObject.transform.LookAt(Enemy.Ball, Vector3.up);
                    Enemy.gameObject.transform.rotation = Quaternion.Euler(0f, Enemy.gameObject.transform.rotation.eulerAngles.y, 0f);
                    Enemy.Rb.velocity = Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                    return;
                }
                else if (Enemy.Player != null)
                {
                    var dist = Vector3.Distance(Enemy.Player.transform.position, Enemy.gameObject.transform.position);
                    if (dist < ConfigManager.S.Info.MinDistanceWithPlayer)
                    {
                        Enemy.Rb.velocity = -Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                        return;
                    }
                    /*else if (dist > ConfigManager.S.Info.MaxDistanceWithPlayer)
                    {
                        Enemy.Rb.velocity = Enemy.transform.forward * ConfigManager.S.Info.EnemySpeed;
                        return;
                    }*/
                }
            }
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                return;
            }
            if (_state == ShootState.AIM)
            {
                _state = ShootState.LOCK;
                _timer = ConfigManager.S.Info.TimeLock;
                Enemy.ToggleAim(false);
                return;
            }
            else if (_state == ShootState.LOCK)
            {
                _state = ShootState.RELOAD;
                _timer = ConfigManager.S.Info.TimeReload;
                var go = Object.Instantiate(Enemy.Bullet, Enemy.transform.position + Enemy.transform.forward, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(Enemy.transform.forward * ConfigManager.S.Info.FireVelocity, ForceMode.Impulse);
                return;
            }
            else if (_state == ShootState.RELOAD)
            {
                _state = ShootState.WAITING;
                return;
            }
            if (_state == ShootState.WAITING)
            {
                _state = ShootState.AIM;
                _timer = ConfigManager.S.Info.TimeAim;
                Enemy.ToggleAim(true);
            }
        }
    }
}
