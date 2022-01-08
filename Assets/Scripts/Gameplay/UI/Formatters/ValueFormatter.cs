using UnityEngine;

namespace Game.UI
{
    public abstract class ValueFormatter : ScriptableObject
    {
        public abstract string Format(double value);
    }
}