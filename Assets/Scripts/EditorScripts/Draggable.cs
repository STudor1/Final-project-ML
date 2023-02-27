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
    [SerializeField]private GridManager grid;
    private GameObject copy;

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
            //this drags the copy of the object around
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
        if (gameObject.tag == "Selectable")
        {
            var spawnedObject = Instantiate(objectPrefab, new Vector3(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y), Quaternion.identity);
            spawnedObject.tag = "Selectable";
            Destroy(gameObject);
        }
        else
        {
            var spawnedObject = Instantiate(objectPrefab, new Vector3(roundVector2(mousePosition2).x, roundVector2(mousePosition2).y), Quaternion.identity);
            //this gets rid of the object we are dragging
            Destroy(copy);
            //changes the tag of the created gameobject
            spawnedObject.tag = "Selectable";
        }
        


        //would probably need to create a list/dic to keep track of them
    }



    private Vector2 roundVector2(Vector2 toRound) {
        return new Vector2(Mathf.Round(toRound.x), Mathf.Round(toRound.y));
    }

}
