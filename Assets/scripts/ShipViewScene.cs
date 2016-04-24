using UnityEngine;

namespace Assets.scripts {

    public class ShipViewScene : MonoBehaviour {

        private static ShipViewScene INSTANCE;

        //Returns instance of the scene object
        public static ShipViewScene Get() {
            return INSTANCE ?? (INSTANCE = GameObject.Find("_Scene").GetComponent<ShipViewScene>());
        }

        //The ship object
        public GameObject Ship;

        //The ship grid object, for convenience
        public GameObject ShipGrid;

        //The object holding the tile prefabs
        public TilePrefabs TilePrefabs;

    }

}
