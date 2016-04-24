using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts {

    public class RoomCreator {

        public static List<GameObject> MakeRooms(GameObject[,] grid) {
            var width = grid.GetLength(0);
            var height = grid.GetLength(1);
            var roomList = new List<GameObject>();
            var checkedTiles = new bool[width, height];
            var roomCounter = 0;

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    if(checkedTiles[x, y])
                        continue;

                    var tileList = new List<GameObject>();
                    FloodFill(x, y, checkedTiles, tileList, grid);

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

        private static GameObject NewRoom(string name) {
            var room = new GameObject {name = name};
            room.AddComponent<RoomScript>();
            return room;
        }

    }

}
