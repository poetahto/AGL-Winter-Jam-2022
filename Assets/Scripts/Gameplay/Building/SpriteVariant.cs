using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Building
{
    public class SpriteVariant : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] sprites;
        
        private void Awake()
        {
            int totalSprites = sprites.Length;
            int randomSprite = Random.Range(0, totalSprites - 1);
            
            spriteRenderer.sprite = sprites[randomSprite];
            spriteRenderer.flipX = Random.value > 0.5f;
        }
    }
}