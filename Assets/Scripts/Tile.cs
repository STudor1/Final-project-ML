using UnityEngine;

/// <summary>
/// This class defines a tile and sets the colour of each tile based on the placement of it.
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject highlight;

    //Used by the grid manager to pick what colour the tile has
    public void Init(bool isOffset)
    {
        if (isOffset)
        {
            _renderer.color = offsetColor;
        }
        else {
            _renderer.color = baseColor;
        }
    }

    //When the mouse hovers over I turn on the highlight object
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    //When the mouse hovers over I turn off the highlight object
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
