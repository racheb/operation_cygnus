using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.scripts {

    public class TilePrefabs : MonoBehaviour {

        //Array of tile prefabs, populated in the editor
        public GameObject[] Prefabs;

        //Maps the prefab's name to themselves, for easier access
        private Dictionary<string, GameObject> prefabDict; 

        //Size of a tile in editor units. Sprites are 24x24 and imported as 48 pixels per unit, therefore .5
        public float TileUnits = .5f;

        //Searches for the prefab matching this name. Creates the dictionary if it doesn't already exist
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

        //Index operator for convenient access of prefabs by name
        public GameObject this[string prefabName] {
            get { return GetPrefab(prefabName); }
        }

    }

}
