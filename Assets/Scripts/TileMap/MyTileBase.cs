using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Base", menuName = "TileMapCube/Base", order = 0)]
public class MyTileBase : Tile {
    public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go) {
        return true;
    }
}