using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private int maxWidthSize;
    [SerializeField] private int maxHeightSize;
    [SerializeField] private int minWidthSize;
    [SerializeField] private int minHeightSize;
    public static int width;
    public static int height;
    public TMP_InputField xField;
    public TMP_InputField yField;
    private bool isWidthBetween;
    private bool isHeightBetween;

    private void StartProgram() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //This will be called to get the user input from the fields 
    public void GetUserInput() {
        width = int.Parse(xField.text.ToString());
        height = int.Parse(yField.text.ToString());

        CheckWidth(width); //check if width is between given values
        CheckHeight(height); //check if height is between given values

        //if both are between values move on to the next scene
        if (isWidthBetween && isHeightBetween)
        {
            StartProgram();
        }
    }

    private void CheckWidth(int width)
    {
        if (width > maxWidthSize || width < minWidthSize)
        {
            xField.text = "Enter a value between " + minWidthSize + " and " + maxWidthSize + ".";
        }
        else
        {
            isWidthBetween = true;
        }
    }

    private void CheckHeight(int height)
    {
        if (height > maxHeightSize || height < minHeightSize)
        {
            yField.text = "Enter a value between " + minHeightSize + " and " + maxHeightSize + ".";
        }
        else
        {
            isHeightBetween = true;
        }
    }
}
