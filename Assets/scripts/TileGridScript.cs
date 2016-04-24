using System;
using System.Collections.Generic;
using Assets.scripts.data.xml;
using UnityEngine;

namespace Assets.scripts {

    public class TileGridScript : MonoBehaviour {

        //Default file for ship data
        public TextAsset DefaultShipXmlFile;

        private GameObject[,] tiles;

        void Start() {
            //Create initial ship
            CreateTileGrid();
        }

        //Creates the tile data, rooms, etc. from the xml file attached to this script
        private void CreateTileGrid() {
            //Clear tiles
            DestroyTileGrid();

            //Load ship data from xml
            var shipXml = ShipXml.Load(DefaultShipXmlFile);

            //Get prefabs
            var prefabArray = shipXml.Grid.GetPrefabArray();
            tiles = new GameObject[prefabArray.GetLength(0), prefabArray.GetLength(1)];

            var tileScale = ShipViewScene.Get().TilePrefabs.TileUnits;
            var displaceX = tiles.GetLength(0)*tileScale/-2f + tileScale/2f;
            var displaceY = tiles.GetLength(1)*tileScale/-2f + tileScale/2f;

            //Get ship grid game object from the scene
            var shipGrid = ShipViewScene.Get().ShipGrid;

            //Create instances of the prefabs to create the actual tiles
            for (var y = 0; y < tiles.GetLength(1); y++) {
                for (var x = 0; x < tiles.GetLength(0); x++) {
                    var tile = Instantiate(prefabArray[x, y], new Vector3(x * tileScale, y * tileScale), Quaternion.identity) 
                        as GameObject;
                    //Translate the tile's position so that the grid is centered on the ship grid object
                    tile.transform.Translate(shipGrid.transform.position.x + displaceX,
                        shipGrid.transform.position.y + displaceY, 0);
                    tile.name = GetTileName(x, y);
                    tiles[x, y] = tile;
                }
            }

            //Get rooms for the tiles and add room objects as children to the ship grid's transform
            var rooms = RoomCreator.MakeRooms(tiles);
            foreach(var room in rooms) room.transform.SetParent(shipGrid.transform);
        }

        //Clears the tiles and rooms of the ship
        private void DestroyTileGrid() {
            //Destroy child objects of the grid (rooms with tiles)
            foreach (Transform tileTransform in ShipViewScene.Get().ShipGrid.transform) {
                Destroy(tileTransform.gameObject);
            }
        }

        //Returns the tile name for the given position
        private static string GetTileName(int x, int y) {
            return "tile_" + x.ToString() + "_" + y.ToString();
        }

    }

}
