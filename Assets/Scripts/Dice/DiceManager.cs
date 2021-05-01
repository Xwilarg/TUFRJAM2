using Assets.Scripts.Config;
using UnityEngine;

namespace Scripts.Dice
{
    public class DiceManager : MonoBehaviour
    {
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
