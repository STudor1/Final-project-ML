using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //[SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Tile> tiles;
    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;
    private void Start()
    {
        GenerateGrid();
        //Debug.Log("2x " + MainMenuScript.width);
        //Debug.Log("2y " + MainMenuScript.height);
    }

    //16 and 9 is max displayed atm
    void GenerateGrid()
    {
        if (width < 17 && height < 10) {
            tiles = new Dictionary<Vector2, Tile>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                    spawnedTile.Init(isOffset);

                    tiles[new Vector2(x, y)] = spawnedTile;
                }
            }
        }
        else
        {
            //this will make the camera be able to move around with wasd
            Debug.Log("x " + MainMenuScript.width);
            Debug.Log("y " + MainMenuScript.height);
        }


        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);

    }

    //this will get a tile at the position 
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
