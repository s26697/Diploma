using System.Collections;
using UnityEngine;

namespace Systems.Managers.WaveManager
{
    [CreateAssetMenu(
        fileName = "SpawnIndicator",
        menuName = "Enemies/Spawn Indicator"
    )]
    public class SpawnIndicatorSo : ScriptableObject
    {
        [Header("prefab")]
        public GameObject prefab;

        [Header("delay")]
        public float delay = 0.5f;

        [Header("Pooling")]
        public int preloadCount = 10;

        private SpawnIndicatorPool _pool;

        public void InitPool(Transform parent)
        {
            if (_pool != null || prefab == null)
                return;

            _pool = new SpawnIndicatorPool(prefab, preloadCount, parent);
        }

        public IEnumerator Show(Vector2 position)
        {
            if (_pool == null || delay <= 0f)
                yield break;

            var indicator = _pool.Get();
            indicator.transform.position = position;

            yield return new WaitForSeconds(delay);

            _pool.Return(indicator);
        }
        
    }
}