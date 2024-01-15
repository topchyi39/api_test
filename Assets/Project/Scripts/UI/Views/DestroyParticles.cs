using UnityEngine;

namespace UI.Views
{
    public class DestroyParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        
        public void Play(Vector3 position, Vector2 sizeDelta, Color color)
        {
            var mainModule = particles.main;
            mainModule.startColor = color;
            var shape = particles.shape;
            shape.scale = sizeDelta;
            particles.transform.position = position;
            
            particles.Emit(100);
        }
    }
}