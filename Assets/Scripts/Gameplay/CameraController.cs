using UnityEngine;

namespace Game.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        
        private void Update()
        {
            float xSpeed = Input.GetAxisRaw("Horizontal");
            float ySpeed = Input.GetAxisRaw("Vertical");
            Vector3 velocity = new Vector3(xSpeed, ySpeed).normalized;

            transform.position += velocity * Time.deltaTime * speed;
        }
    }
}