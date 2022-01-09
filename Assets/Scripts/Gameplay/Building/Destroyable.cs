using UnityEngine;

namespace Game.Gameplay.Building
{
    public class Destroyable : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}