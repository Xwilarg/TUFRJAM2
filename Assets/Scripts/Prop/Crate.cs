using UnityEngine;

namespace Scripts.Prop
{
    public class Crate : MonoBehaviour
    {
        public int _health = 2;

        public void TakeDamage()
        {
            _health--;
            if(_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
