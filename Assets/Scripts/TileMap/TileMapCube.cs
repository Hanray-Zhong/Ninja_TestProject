using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapCube", menuName = "TileMapCube/Ground", order = 0)]
public class TileMapCube : TileBase {
    public Sprite TileSprite;
    public Color TileColor;
 
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = TileSprite;
        tileData.color = TileColor;
        base.GetTileData(position, tilemap, ref tileData);
    }
    
}