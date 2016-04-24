﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts {

    public class RoomScript : MonoBehaviour {

        public bool IsVoidRoom = false;
        public static Color OverlayColor = new Color(1, 1, .5f);

        public void OnMouseEnter() {
            if (IsVoidRoom)
                return;

            foreach (Transform child in gameObject.transform) {
                child.gameObject.GetComponent<SpriteRenderer>().color = OverlayColor;
            }
        }

        public void OnMouseExit() {
            if (IsVoidRoom)
                return;

            foreach (Transform child in gameObject.transform) {
                child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

    }

}
