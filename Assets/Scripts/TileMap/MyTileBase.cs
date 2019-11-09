using UnityEngine;
using UnityEngine.Tilemaps;

public class MyTileBase : Tile {
    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go) {
        Debug.Log("Tile Init! Location : " + location + " GameObj : ");
        return true;
    }
}