using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    [SerializeField] float screenWidthInUnits = 16f;

    // cached reference
    GameSession theGameSession;
    Ball theBall;

    void Start()
    {
        // initiates both objects
        theGameSession = FindObjectOfType<GameSession>();
        theBall = FindObjectOfType<Ball>();
    }

    // Updating once per frame
    void Update()
    {
        // Tracking the position of a mouse (.x is for the x-axis)
        float mousePositionInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        
        // Vector2 only uses 2 positions which are X and Y (2D games)
        // We create new Vector that defines the position of paddle at the beginning of a game
        // Paddle stays at current position X and Y.
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);

        // Clamps mousePositionInUnits between minX and maxX...limits movement of a paddle on x-axis
        // cached references are initiated since it is expensive to use "FindObjectsOfType<>()" in Update() method
        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);
  
        // Transform component in Unity referring to its position
        // The transform position is now based on the paddlePosition units above
        transform.position = paddlePosition;
    }

    // Method for autoplay
    private float GetXPos()
    {
        if (theGameSession.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }
        else
        {
            //returns a position of a mouse
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
