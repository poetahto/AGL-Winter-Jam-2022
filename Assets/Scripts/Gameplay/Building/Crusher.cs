using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Building
{
    public class Crusher : MonoBehaviour
    {
        [SerializeField] public Collider2D targetCollider;

        private void Start()
        {
            var filter = new ContactFilter2D();
            var hits = new List<Collider2D>();
            targetCollider.OverlapCollider(filter, hits);

            foreach (var hitCollider in hits)
            {
                if (hitCollider.TryGetComponent(out Destroyable destroyable))
                    destroyable.Destroy();
            }
        }
    }
}