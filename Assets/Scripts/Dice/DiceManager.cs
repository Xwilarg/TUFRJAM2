using UnityEngine;

namespace Scripts.Dice
{
    public class DiceManager : MonoBehaviour
    {
        [SerializeField]
        private DiceInspector[] _dices;

        private void Start()
        {
            SpawnDices();
        }

        private void SpawnDices()
        {
            foreach (var dice in _dices)
            {
                var cam = UnityEngine.Camera.main;
                Instantiate(dice.Prefab, cam.transform.position + cam.transform.forward * 3f, Quaternion.identity);
            }
        }
    }
}
