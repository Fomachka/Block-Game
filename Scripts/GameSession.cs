using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //adds a slider from 0.1f to 10f to the serialized gameSpeed.
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed;
    [SerializeField] int currentScore = 0;
    [SerializeField] int pointsPerBlockDestroyed = 10;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

 

    //Singleton pattern used to retain an object throughout the whole game and change of scenes.
    private void Awake()
    {
        //stores how many gameobjects there are
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if(gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            //game session(score) doesn't get destroyed and moves to the next scene
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DestroyScriptInstance()
    {
        // Removes this script instance from the game object
        // resets the score
        Destroy(gameObject);
    }

    void Start()
    {
        scoreText.text = currentScore.ToString();
    }
    
    void Update()
    {
        // timescale of 0.5f is 2x slower the real time
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
