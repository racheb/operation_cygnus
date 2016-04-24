using System;
using UnityEngine;

namespace Assets.scripts {

    public class TileScript : MonoBehaviour {

        public bool IsVoidTile = false;
        public bool IsWallTile = false;

        public static Color OverlayColor = new Color(1, 1, .5f);

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
