using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerInfo", fileName = "PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [Header("Player")]
        public float Speed;

        public float FireVelocity;

        [Header("Dice")]
        public float ThrowForce;
        public float SpawnDist;
        public float RelaunchForce;
        public float RelaunchTorque;

        public GameObject DiceMaster, DiceEnemy, DiceAllie, DiceNeutral, DiceObjective;

        public int MasterNbDicesSpawn;

        [Header("Spawnable")]
        public GameObject[] Enemies;
    }
}
