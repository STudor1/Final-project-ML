using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject UIBox;

    private void OnMouseDown()
    {
        Debug.Log("Button clicked");
        //disable the ui box 
        UIBox.SetActive(false);
        //create a new list with every tile and every game object that has been placed on the grid

        //loop thru the list and lock them in position
    }
}
