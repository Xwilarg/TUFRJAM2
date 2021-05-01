﻿using UnityEngine;

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
    }
}
