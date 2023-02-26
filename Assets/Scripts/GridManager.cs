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
    private float x, y, z;
    //CameraMovement cameraMovement;
    [SerializeField] GameObject mainCamera;
    //public static bool isCameraMoving;

    private void Awake()
    {
        x = (float)width / 2 - 0.5f;
        y = (float)height / 2 - 0.5f;
        z = -10;
        if (width > 16 || height > 9)
        {

            //isCameraMoving = true;
            mainCamera.GetComponent<CameraMovement>().enabled = true;
        }
        else
        {
            //isCameraMoving = false;
                    mainCamera.GetComponent<CameraMovement>().enabled = false;

        }
        //cameraMovement = camera.GetComponent<CameraMovement>();
    }

    private void Start()
    {
        GenerateGrid();
        //Debug.Log("2x " + MainMenuScript.width);
        //Debug.Log("2y " + MainMenuScript.height);
    }

    //16 and 9 is max displayed atm
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

        //x = (float)width / 2 - 0.5f;
        //y = (float)height / 2 - 0.5f;
        //z = -10;
        cam.transform.position = new Vector3(x, y, z);

        
        


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
