using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //This will take the 2 dictionaries grid and obstacle and then create the lvl in 3D based on them
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wall3DPrefab;

    [SerializeField] private GridManager grid2D;
    [SerializeField] private ObstaclesManager obstacles2D;
    //private ObstaclesManager obstacles2D;

    private Dictionary<Vector3, GameObject> grid3D;
    private Dictionary<Vector3, GameObject> obstacles3D;

    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;
    //private Dictionary<Vector2, GameObject> grid2d = ObstaclesManager.obstacleDictionary;

    private Vector2 pos;
    private int floorZ = 0;
    private int obstaclesZ = 1;

    private void Awake()
    {
        obstacles2D = GameObject.Find("ObstaclesManager").GetComponent<ObstaclesManager>();
    }

    private void Start()
    {
        Debug.Log("New scene loaded");
        grid3D = new Dictionary<Vector3, GameObject>();
        obstacles3D = new Dictionary<Vector3, GameObject>();

        //GenerateFloor();
        Debug.Log("Objects in dictionary:");
        GenerateObstacles();
        //Debug.Log(obstacles2D.obstacleDictionary.Count);
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pos = new Vector2(x, y);

                if (grid2D.GetTileAtPosition(pos) != null) 
                {
                    var spawnedFloor = Instantiate(floorPrefab, new Vector3(x, y, floorZ), Quaternion.identity);

                    grid3D[new Vector3(x, y, floorZ)] = spawnedFloor;
                }

            }
        }
    
    
    
    }

    private void GenerateObstacles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                pos = new Vector2(x, y);

                //Debug.Log(pos);

                if (obstacles2D.GetObstacleAtPosition(pos) != null)
                {
                    Debug.Log("New Scene: There is a " + obstacles2D.GetObstacleAtPosition(pos) + " at " + pos);
                }


                //if (obstacles2D.GetObstacleAtPosition(pos) != null)
                //{
                //    var spawnedWall = Instantiate(wall3DPrefab, new Vector3(x, y, obstaclesZ), Quaternion.identity);

                //    obstacles3D[new Vector3(x, y, obstaclesZ)] = spawnedWall;
                //}


            }
        }
    }
}
