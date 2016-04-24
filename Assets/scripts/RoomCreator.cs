using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts {

    public class RoomCreator {

        /// <summary>
        /// Returns a list of GameObjects being the rooms created from a
        /// flood fill algorithm over the tile grid. The tiles that are
        /// part of a room are children of the room transform.
        /// All parts not part of a valid room (walls, void, etc.) are
        /// placed in a special room called "no_room".
        /// </summary>
        /// <param name="grid">The grid of tiles</param>
        /// <returns>A list of rooms</returns>
        
        //Copied straight from Java code (libGdx version)
        public static List<GameObject> MakeRooms(GameObject[,] grid) {
            var width = grid.GetLength(0);
            var height = grid.GetLength(1);
            var roomList = new List<GameObject>();
            var checkedTiles = new bool[width, height];
            var roomCounter = 0;

            //Go through all tiles yet unchecked and run the flood fill algorithm on them
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    if(checkedTiles[x, y])
                        continue;

                    var tileList = new List<GameObject>();
                    FloodFill(x, y, checkedTiles, tileList, grid);

                    //If tiles for a new room are found: create room
                    if (tileList.Count <= 0) continue;
                    var roomObject = NewRoom("room_" + roomCounter++);
                    var roomScript = roomObject.GetComponent<RoomScript>();
                    foreach (var tile in tileList) {
                        tile.transform.SetParent(roomObject.transform);
                        tile.GetComponent<TileScript>().parentRoom = roomScript;
                    }
                    roomList.Add(roomObject);
                }
            }

            //Create room for "outcast" tiles, considered non-room tiles
            var noRoomObject = NewRoom("no_room");
            noRoomObject.GetComponent<RoomScript>().IsVoidRoom = true;
            var noRoomScript = noRoomObject.GetComponent<RoomScript>();
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    if(checkedTiles[x, y]) continue;
                    grid[x, y].transform.SetParent(noRoomObject.transform);
                    grid[x, y].GetComponent<TileScript>().parentRoom = noRoomScript;
                }
            }
            roomList.Add(noRoomObject);

            return roomList;
        }

        //Recursively seeks tiles belonging to the same room as the tile it started with
        private static void FloodFill(int x, int y, bool[,] checkedTiles, List<GameObject> tileList, GameObject[,] grid) {
            if (x < 0 || y < 0 || x >= checkedTiles.GetLength(0) || y >= checkedTiles.GetLength(1))
                return;
            var tileScript = grid[x, y].GetComponent<TileScript>();

            if (tileScript.IsVoidTile || tileScript.IsWallTile || checkedTiles[x, y])
                return;

            checkedTiles[x, y] = true;
            tileList.Add(grid[x, y]);
            FloodFill(x - 1, y, checkedTiles, tileList, grid);
            FloodFill(x, y - 1, checkedTiles, tileList, grid);
            FloodFill(x + 1, y, checkedTiles, tileList, grid);
            FloodFill(x, y + 1, checkedTiles, tileList, grid);
        }

        //Creates new room object with attached script component
        private static GameObject NewRoom(string name) {
            var room = new GameObject {name = name};
            room.AddComponent<RoomScript>();
            return room;
        }

    }

}
