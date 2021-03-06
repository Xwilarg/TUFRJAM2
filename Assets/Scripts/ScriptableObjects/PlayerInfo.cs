using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [Header("Player")]
        public float Speed;
        public float FireVelocity;

        [Header("Enemy")]
        public float EnemySpeed;
        public float MinDistanceWithPlayer;
        public float MinDistanceWithBall;

        public float TimeAim, TimeLock, TimeReload;

        [Header("Dice")]
        public float ThrowForce;
        public float SpawnDist;
        public float RelaunchForce;
        public float RelaunchTorque;

        public GameObject DiceMaster, DiceEnemy, DiceAllie, DiceNeutral, DiceObjective;

        public int MasterNbDicesSpawn;

        [Header("Mine")]
        public float TimeBeforeExplosion;
        public float ExplosionForce;

        [Header("Canon")]
        public float CanonForce;

        [Header("Ball")]
        public float FeetForce;

        [Header("Bullet")]
        public float StunTime;
        public float StunForce;
    }
}
