using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for generating the grid and creating a dictionary of the grid.
/// </summary>
public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform cam;
    [SerializeField] GameObject mainCamera;

    private Dictionary<Vector2, Tile> tiles;
    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;
    private float x, y, z;
    
    //Check if the tiles fit on the screen, if they don't then allow for the user to use the camera
    //To improve I would check this on the computer that is being used rather than having fixed values.
    private void Awake()
    {
        x = (float)width / 2 - 0.5f;
        y = (float)height / 2 - 0.5f;
        z = -10;
        if (width > 16 || height > 9)
        {
            mainCamera.GetComponent<CameraMovement>().enabled = true;
        }
        else
        {
            mainCamera.GetComponent<CameraMovement>().enabled = false;
        }
    }

    //Creates the grid
    private void Start()
    {
        GenerateGrid();
    }

    //Takes the user input and generates the grid
    void GenerateGrid()
    {
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

        cam.transform.position = new Vector3(x, y, z);
    }

    //This will get a tile at the given position 
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }
}
