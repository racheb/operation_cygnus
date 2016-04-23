using UnityEngine;

namespace Assets.scripts {

    public class ShipViewScene : MonoBehaviour {

        private static ShipViewScene INSTANCE;

        public static ShipViewScene Get() {
            return INSTANCE ?? (INSTANCE = GameObject.Find("_Scene").GetComponent<ShipViewScene>());
        }

        public GameObject Ship;
        public GameObject ShipGrid;
        public TilePrefabs TilePrefabs;

        void Start() {
            TilePrefabs = ShipGrid.GetComponent<TilePrefabs>();

        }

        void Update() {
	
        }
    }

}
