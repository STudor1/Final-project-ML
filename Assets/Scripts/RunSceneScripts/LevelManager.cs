using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //This will take the 2 dictionaries grid and obstacle and then create the lvl in 3D based on them
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wall3DPrefab;
    [SerializeField] private GameObject box3DPrefab;
    [SerializeField] private GameObject agent3DPrefab;
    [SerializeField] private GameObject exit3DPrefab;

    public static Transform exitTrans;

    //[SerializeField] private GridManager grid2D;
    [SerializeField] private ObstaclesManager obstacles2D;
    //private ObstaclesManager obstacles2D;

    private Dictionary<Vector3, GameObject> grid3D;
    private Dictionary<Vector3, GameObject> obstacles3D;

    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;
    //private Dictionary<Vector2, GameObject> grid2d = ObstaclesManager.obstacleDictionary;

    private Vector2 pos;
    private int floorY = 0;
    private int obstaclesY = 1; //y is 1 for everything in the scene, as they are placed at y1 and y from previous becomes z in 3D

    private string wall = "Wall";
    private string box = "Box";
    private string exit = "Exit";
    private string agent = "Agent";

    private void Awake()
    {
        obstacles2D = GameObject.Find("ObstaclesManager").GetComponent<ObstaclesManager>();
    }

    private void Start()
    {
        Debug.Log("New scene loaded");
        grid3D = new Dictionary<Vector3, GameObject>();
        obstacles3D = new Dictionary<Vector3, GameObject>();

        GenerateFloor();
        //Debug.Log("Objects in dictionary:");
        GenerateObstacles();
        //Debug.Log(obstacles2D.obstacleDictionary.Count);
    }

    private void GenerateFloor()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                pos = new Vector2(x, z);

                var spawnedFloor = Instantiate(floorPrefab, new Vector3(x, floorY, z), Quaternion.identity);
                grid3D[new Vector3(x, floorY, z)] = spawnedFloor;

                

            }
        }
    
    
    
    }

    private void GenerateObstacles()
    {
        //y in 2d becomes z in 3d
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                pos = new Vector2(x, z);

                //Debug.Log(pos);

                //if (obstacles2D.GetObstacleAtPosition(pos) != null)
                //{
                //    Debug.Log("New Scene: There is a " + obstacles2D.GetObstacleAtPosition(pos) + " at " + pos);
                //}


                if (obstacles2D.GetObstacleAtPosition(pos) != null)
                {
                    placeObstacleType(obstacles2D.GetObstacleAtPosition(pos), x, z);
                }


            }
        }
    }

    private void placeObstacleType(string obstacle, int x, int z)
    {
        if (obstacle == wall)
        {
            var spawnedObstacle = Instantiate(wall3DPrefab, new Vector3(x, obstaclesY, z), Quaternion.identity);

            obstacles3D[new Vector3(x, obstaclesY, z)] = spawnedObstacle;
        }
        if (obstacle == box)
        {
            var spawnedObstacle = Instantiate(box3DPrefab, new Vector3(x, obstaclesY, z), Quaternion.identity);

            obstacles3D[new Vector3(x, obstaclesY, z)] = spawnedObstacle;
        }
        if (obstacle == exit)
        {
            var spawnedObstacle = Instantiate(exit3DPrefab, new Vector3(x, obstaclesY, z), Quaternion.identity);

            obstacles3D[new Vector3(x, obstaclesY, z)] = spawnedObstacle;

            exitTrans = spawnedObstacle.transform;
        }
        if (obstacle == agent)
        {
            var spawnedObstacle = Instantiate(agent3DPrefab, new Vector3(x, obstaclesY, z), Quaternion.identity);

            obstacles3D[new Vector3(x, obstaclesY, z)] = spawnedObstacle;
        }
    }
}
