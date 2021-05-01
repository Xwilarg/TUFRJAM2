using System;
using UnityEngine;

namespace Scripts.Dice
{
    [Serializable]
    public struct DiceInspector
    {
        public DiceType Type;
        public GameObject Prefab;
    }
}
