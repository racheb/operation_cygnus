using System;
using UnityEngine;
using System.Collections;
using Assets.scripts;

public class TileScript : MonoBehaviour {

    public Boolean IsVoidTile = false;

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnMouseOver() {
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            return;

        if (IsVoidTile) return;

        var prefab = ShipViewScene.Get().TilePrefabs["TileWall"];
        if (Input.GetMouseButtonDown(1))
            prefab = ShipViewScene.Get().TilePrefabs["TileFloor"];
        var newTile = Instantiate(prefab, this.transform.position, Quaternion.identity) as GameObject;
        newTile.transform.SetParent(this.transform.parent);
        newTile.name = this.name;
        Destroy(this.gameObject);
    }

}
