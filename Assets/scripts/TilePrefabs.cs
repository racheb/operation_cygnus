using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scripts {

    public class TilePrefabs : MonoBehaviour {

        public GameObject[] Prefabs;

        private Dictionary<string, GameObject> prefabDict; 

        public float TileUnits = .5f;

        private GameObject GetPrefab(string prefabName) {
            if (prefabDict == null || prefabDict.Count != Prefabs.Length) {
                prefabDict = new Dictionary<string, GameObject>();
                foreach(GameObject prefab in Prefabs)
                    prefabDict.Add(prefab.name, prefab);
            }
            if(!prefabDict.ContainsKey(prefabName))
                throw new NullReferenceException("Prefab " + prefabName + " not found");
            return prefabDict[prefabName];
        }

        public GameObject this[string prefabName] {
            get { return GetPrefab(prefabName); }
        }

    }

}
