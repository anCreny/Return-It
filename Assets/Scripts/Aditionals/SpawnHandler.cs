using System.Collections.Generic;
using UnityEngine;

namespace Aditionals
{
    public class SpawnHandler
    {
        private List<GameObject> _pool = new();

        public SpawnHandler(int countOfSpawn)
        {
            CountOfSpawn = countOfSpawn;
        }

        public List<GameObject> Pool => _pool;

        public int CountOfSpawn { get; set; }

        public void AddInPool(GameObject target)
        {
            _pool.Add(target);
        }
    }
}