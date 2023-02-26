using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public static int width, height;
    public TMP_InputField xField;
    public TMP_InputField yField;
    public void StartProgram() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //check for ints later
    public void GetUserInput() {
        width = int.Parse(xField.text.ToString());
        height = int.Parse(yField.text.ToString());
        //Debug.Log("1x " + width);
        //Debug.Log("1y " + height);
    }
}
