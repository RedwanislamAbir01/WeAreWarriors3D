using UnityEngine;

namespace _Game.Helpers
{
    public class DestroyMe : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 5f;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }
    }
}