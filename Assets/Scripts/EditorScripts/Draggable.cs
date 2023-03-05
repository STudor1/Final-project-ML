using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Draggable : MonoBehaviour
{
    private Vector3 mousePositionOffSet;
    [SerializeField] private GameObject objectPrefab;
    // private Vector3 mousePosition3;
    private Vector2 mousePosition2;
    //private Vector2 test;
    [SerializeField] private GridManager grid;
    private GameObject copy;
    public Dictionary<Vector2, GameObject> gameObjDic;
    private Vector2 pos;
    private Vector2 lastPos;
    private Vector2 test = new Vector2(3, 3);

    [SerializeField] private ObstaclesManager obstacles;

    private void Start()
    {
        gameObjDic = new Dictionary<Vector2, GameObject>();
        mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return WorldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    //Logic
    //on mouse down delete the object at that position 
    //on mouse up add the object at that position 

    private void OnMouseDown()
    {
        if (gameObject.tag == "Selectable")
        {
            mousePositionOffSet = gameObject.transform.position - GetMouseWorldPosition();
            //This will set last pos to the position the obj is at when we click on it, we want to remove this pos from dic
            lastPos = new Vector2(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y);
            Debug.Log(lastPos);
        }
        else
        {
            //create a copy of the game object to drag around
            copy = Instantiate(gameObject);
            //capture the mouse offset
            mousePositionOffSet = gameObject.transform.position - GetMouseWorldPosition();
        }

    }

    private void OnMouseDrag()
    {
        if (gameObject.tag == "Selectable")
        {
            mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //this drags the object once placed
            transform.position = GetMouseWorldPosition() + mousePositionOffSet;
            grid.GetTileAtPosition(mousePosition2);
        }
        else
        {
            mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //this drags the copy of the object around
            copy.transform.position = GetMouseWorldPosition() + mousePositionOffSet;
            grid.GetTileAtPosition(mousePosition2);
        }


        //Debug.Log(mousePosition2);
        //Debug.Log(roundVector2(mousePosition2));
        //Debug.Log(grid.GetTileAtPosition(roundVector2(mousePosition2)));
    }
    //when we release the mouse a new wall objects gets created at the tile we are on
    private void OnMouseUp()
    {
        pos = new Vector2(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y);

        if (gameObject.tag == "Selectable")
        {
            //var spawnedObject = Instantiate(objectPrefab, new Vector3(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y), Quaternion.identity);
            //spawnedObject.tag = "Selectable";
            gameObject.transform.position = pos;
            //will get rid of game obj at last position
            Debug.Log("Step 1: Will remove " + gameObject + " at " + roundVector2(lastPos).x + " " + roundVector2(lastPos).y);
            //gameObjDic.Remove(lastPos); //this should remove the game object at that value || last pos is the key I want to remove
            //Destroy(gameObject); //I think this also removes the object from the dictionary?
            obstacles.RemoveObstacle(lastPos);

            //will add new game obj at current pos
            Debug.Log("Step 2: Will add " + gameObject + " at " + pos);
            //AddNewItem(pos, gameObject); // this should add a game obejct at current pos || pos is the key I want to add
            obstacles.AddObstacle(pos, gameObject);

            //gameObjDic[pos] = gameObject;

        }
        else
        {

            var spawnedObject = Instantiate(objectPrefab, new Vector3(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y), Quaternion.identity);

            //This line adds the initial obstacle at this position
            //WORKS FINE
            Debug.Log("Game obj is " + spawnedObject);
            Debug.Log("Step 0: Will add initial " + spawnedObject + " at " + roundVector2(mousePosition2).x + " " + roundVector2(mousePosition2).y);
            //AddNewItem(pos, spawnedObject);
            obstacles.AddObstacle(pos, spawnedObject);

            //this gets rid of the object we are dragging
            Destroy(copy);
            //changes the tag of the created gameobject
            spawnedObject.tag = "Selectable";
        }



        //would probably need to create a list/dic to keep track of them
    }

    private void AddNewItem(Vector2 pos, GameObject gameObj) 
    {
        Debug.Log("adding " + gameObj + " to key " + pos);
        gameObjDic.Add(pos, gameObj);
        foreach (var kvp in gameObjDic)
        {
            Debug.Log("Key = {0}, Value = {1}" + kvp.Key, kvp.Value);
        }
    }

    private Vector2 roundVector2(Vector2 toRound)
    {
        return new Vector2(Mathf.Round(toRound.x), Mathf.Round(toRound.y));
    }

    public GameObject GetGameObjAtPosition(Vector2 pos)
    {
        if (gameObjDic.TryGetValue(pos, out var gameObj))
        {
            return gameObj;
        }

        return null;
    }
}