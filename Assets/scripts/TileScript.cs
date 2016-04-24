using System;
using UnityEngine;

namespace Assets.scripts {

    public class TileScript : MonoBehaviour {

        // The tile is invisible void tile, not interactable by player or characters in game
        public bool IsVoidTile = false;

        // The tile is a wall and thereby delimits rooms and can't be passed through
        public bool IsWallTile = false;

        // The room script of the room the tile is in
        public RoomScript parentRoom;

        void OnMouseEnter() {
            if (parentRoom != null) {
                parentRoom.OnMouseEnter();
            }
        }

        void OnMouseExit() {
            if (parentRoom != null) {
                parentRoom.OnMouseExit();
            }
        }

    }

}
