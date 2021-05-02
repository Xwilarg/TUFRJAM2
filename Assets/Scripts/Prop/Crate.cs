using UnityEngine;

namespace Scripts.Prop
{
    public class Crate : MonoBehaviour
    {
        public void TakeDamage()
        {
            Destroy(gameObject);
        }
    }
}
