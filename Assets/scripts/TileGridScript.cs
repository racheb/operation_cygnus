using System;
using System.Collections.Generic;
using Assets.scripts.data.xml;
using UnityEngine;

namespace Assets.scripts {

    public class TileGridScript : MonoBehaviour {

        public TextAsset DefaultShipXmlFile;

        private GameObject[,] tiles;

        void Start() {
            CreateTileGrid();
        }

        private void CreateTileGrid() {
            var shipXml = ShipXml.Load(DefaultShipXmlFile);

            var prefabArray = shipXml.Grid.GetPrefabArray();
            tiles = new GameObject[prefabArray.GetLength(0), prefabArray.GetLength(1)];

            var tileScale = ShipViewScene.Get().TilePrefabs.TileUnits;
            var displaceX = tiles.GetLength(0)*tileScale/-2f + tileScale/2f;
            var displaceY = tiles.GetLength(1)*tileScale/-2f + tileScale/2f;

            var shipGrid = ShipViewScene.Get().ShipGrid;

            for (var y = 0; y < tiles.GetLength(1); y++) {
                for (var x = 0; x < tiles.GetLength(0); x++) {
                    var tile = Instantiate(prefabArray[x, y], new Vector3(x * tileScale, y * tileScale), Quaternion.identity) 
                        as GameObject;
                    tile.transform.Translate(shipGrid.transform.position.x + displaceX,
                        shipGrid.transform.position.y + displaceY, 0);
                    tile.name = GetTileName(x, y);
                    //tile.transform.SetParent(shipGrid.transform);
                    tiles[x, y] = tile;
                }
            }

            var rooms = RoomCreator.MakeRooms(tiles);
            foreach(var room in rooms) room.transform.SetParent(shipGrid.transform);
        }

        private void DestroyTileGrid() {
            foreach (Transform tileTransform in ShipViewScene.Get().ShipGrid.transform) {
                Destroy(tileTransform.gameObject);
            }
        }

        private static string GetTileName(int x, int y) {
            return "tile_" + x.ToString() + "_" + y.ToString();
        }

    }

}
