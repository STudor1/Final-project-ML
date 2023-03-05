using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehaviour : MonoBehaviour
{
    [SerializeField] private GridManager grid;
    [SerializeField] private Draggable wall;
    [SerializeField] private Draggable box;
    [SerializeField] private ObstaclesManager obstacles;

    [SerializeField] private GameObject UIBox;
    //[SerializeField] private Draggable gameObjs2;
    private int width = MainMenuScript.width;
    private int height = MainMenuScript.height;
    private Dictionary<Vector2, GameObject> gameObjs;
    private Vector2 pos;

    private void OnMouseDown()
    {
        Debug.Log("Button clicked");
        //disable the ui box 
        UIBox.SetActive(false);
        //create a new list with every tile and every game object that has been placed on the grid
        gameObjs = new Dictionary<Vector2, GameObject>();

        //when I click the run button it will create a dictionary of game objs 
        //
        //gameObjDic.GenerateObstaclesDictionary();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //if get tile at position isnt null then i will replace it with a cube at z = 0
                pos = new Vector2(x, y);
                //Debug.Log(pos + " TILE " + grid.GetTileAtPosition(pos));

                //if get game obj at position isnt null then i will replace it with a game obj at z = 1

                if(obstacles.GetObstacleAtPosition(pos) != null)
                {
                    Debug.Log("There is a " + obstacles.GetObstacleAtPosition(pos) + " at " + pos);
                }

                //if (wall.GetGameObjAtPosition(pos) != null) {
                //    Debug.Log("There is a " + wall.GetGameObjAtPosition(pos) + " at " + pos);
                //}

                //if (box.GetGameObjAtPosition(pos) != null)
                //{
                //    Debug.Log("There is a box at " + wall.GetGameObjAtPosition(pos));
                //}

                //if we get tile at pos and we have a tile we add the tile to the dic
                //if we have a tile and a game object then we add the game object to the dic 
                //if (grid.GetTileAtPosition(pos)) 
                //{
                //Debug.Log("x " + x + " y " + y + " "+ gameObjs2.GetGameObjAtPosition(pos));

                //}

                //gameObjs[new Vector2(x, y)] = ;
            }
        }
        //loop thru the list and lock them in position
    }
}
