using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Draggable : MonoBehaviour
{
    [SerializeField] private ObstaclesManager obstacles;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GridManager grid;

    private GameObject copy;
    private Vector3 mousePositionOffSet;
    private Vector2 mousePosition2; //means mouse position in vector 2
    private Vector2 pos;
    private Vector2 lastPos;

    private void Start()
    {
        mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return WorldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (gameObject.tag == "Selectable")
        {
            mousePositionOffSet = gameObject.transform.position - GetMouseWorldPosition();
            //This will set last pos to the position the obj is at when we click on it, we want to remove this pos from dictionary
            lastPos = new Vector2(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y);
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
            //This drags the object once placed
            transform.position = GetMouseWorldPosition() + mousePositionOffSet;
            grid.GetTileAtPosition(mousePosition2);
        }
        else
        {
            mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //This drags the copy of the object around
            copy.transform.position = GetMouseWorldPosition() + mousePositionOffSet;
            grid.GetTileAtPosition(mousePosition2);
        }
    }

    //When we release the mouse a new wall gets put on the tile we are on initially then
    //We can move that wall object and change its position
    private void OnMouseUp()
    {
        pos = new Vector2(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y); //current position

        if (gameObject.tag == "Selectable")
        {
            //This changes the position of the obstacle to the new position 
            gameObject.transform.position = pos;

            //Debug.Log("Step 1: Will remove " + gameObject + " at " + roundVector2(lastPos).x + " " + roundVector2(lastPos).y);
            //This removes the obstacle from the last position it was at 
            obstacles.RemoveObstacle(lastPos);

            //Debug.Log("Step 2: Will add " + gameObject + " at " + pos);
            //This adds the obstacle to the current position
            obstacles.AddObstacle(pos, gameObject);
        }
        else
        {
            var spawnedObject = Instantiate(objectPrefab, new Vector3(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y), Quaternion.identity);

            //Debug.Log("Game obj is " + spawnedObject);
            //Debug.Log("Step 0: Will add initial " + spawnedObject + " at " + roundVector2(mousePosition2).x + " " + roundVector2(mousePosition2).y);

            //This adds the initial obstacle at this position
            obstacles.AddObstacle(pos, spawnedObject);

            //this gets rid of the object we are dragging
            Destroy(copy);
            //changes the tag of the created gameobject
            spawnedObject.tag = "Selectable";
        }
    }


    private Vector2 roundVector2(Vector2 toRound)
    {
        return new Vector2(Mathf.Round(toRound.x), Mathf.Round(toRound.y));
    }

}