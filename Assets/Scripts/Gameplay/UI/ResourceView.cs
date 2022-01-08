using UnityEngine;

namespace Game.UI
{
    public class ResourceView : MonoBehaviour
    {
        public Display nameDisplay;
        public Display descriptionDisplay;
        public Display valueDisplay;

        public bool HasName => nameDisplay != null;
        public bool HasDescription => descriptionDisplay != null;
        public bool HasValue => valueDisplay != null;
    }
}