using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    private Dictionary<Vector2, GameObject> obstacleDictionary;

    //This will have the methods to add stuff to a single dictoinary and will be called in drag

    private void Start()
    {
        obstacleDictionary = new Dictionary<Vector2, GameObject>();

    }

    public void AddObstacle(Vector2 pos, GameObject obstacle)
    {
        obstacleDictionary.Add(pos, obstacle);
    }

    public void RemoveObstacle(Vector2 pos)
    {
        obstacleDictionary.Remove(pos);
    }

    public GameObject GetObstacleAtPosition(Vector2 pos)
    {
        if (obstacleDictionary.TryGetValue(pos, out var gameObj))
        {
            return gameObj;
        }

        return null;
    }
}
