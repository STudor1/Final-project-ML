using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mousePositionOffSet;
    //[SerializeField] private GameObject objectPrefab;
   // private Vector3 mousePosition3;
    private Vector2 mousePosition2;
    private Vector2 test;
    [SerializeField]private GridManager grid;

    private void Start()
    {
        test.x = 0;
        test.y = 0;
    }
    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position and return WorldPoint
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        //capture the mouse offset
        mousePositionOffSet = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        mousePosition2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition2 = mousePosition3;
        transform.position = GetMouseWorldPosition() + mousePositionOffSet;
        grid.GetTileAtPosition(mousePosition2);
        Debug.Log(mousePosition2);
        Debug.Log(roundVector2(mousePosition2));
        Debug.Log(grid.GetTileAtPosition(roundVector2(mousePosition2)));
        //var spawnedObject = Instantiate(objectPrefab, new Vector3(x, y), Quaternion.identity);

    }

    private Vector2 roundVector2(Vector2 toRound) {
        return new Vector2(Mathf.Round(toRound.x), Mathf.Round(toRound.y));
    }


}
