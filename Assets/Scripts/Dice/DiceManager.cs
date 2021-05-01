using Scripts.Config;
using UnityEngine;

namespace Scripts.Dice
{
    public class DiceManager : MonoBehaviour
    {
        public static DiceManager S;

        private void Awake()
        {
            S = this;
        }

        [SerializeField]
        private GameObject _player;

        private int _diceCount = 1;
        public int DiceCount
        {
            set
            {
                _diceCount = value;
                if (_diceCount == 0)
                {
                    Instantiate(_player, Vector3.up * 20f, Quaternion.identity);
                }
            }
            get
            {
                return _diceCount;
            }
        }

        private void Start()
        {
            SpawnDices();
        }

        private void SpawnDices()
        {
            var cam = UnityEngine.Camera.main;
            Instantiate(ConfigManager.S.Info.DiceMaster, cam.transform.position + cam.transform.forward * ConfigManager.S.Info.SpawnDist, Quaternion.identity);
        }
    }
}
