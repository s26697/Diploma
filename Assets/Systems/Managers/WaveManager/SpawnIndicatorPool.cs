using System.Collections.Generic;
using UnityEngine;

namespace Systems.Managers.WaveManager
{
    public class SpawnIndicatorPool
    {
        private readonly Queue<GameObject> pool = new();
        private readonly GameObject prefab;
        private readonly Transform parent;

        public SpawnIndicatorPool(GameObject prefab, int size, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < size; i++)
            {
                var go = GameObject.Instantiate(prefab, parent);
                go.SetActive(false);
                pool.Enqueue(go);
            }
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
            {
                var go = GameObject.Instantiate(prefab, parent);
                go.SetActive(false);
                return go;
            }

            var indicator = pool.Dequeue();
            indicator.SetActive(true);
            return indicator;
        }

        public void Return(GameObject indicator)
        {
            indicator.SetActive(false);
            pool.Enqueue(indicator);
        }
    }
}