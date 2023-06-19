using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishPit : MonoBehaviour
{
    public Player player;
    private int obstacleCounter;
    public TextMeshProUGUI countText;
    
    public int totalRequiredBalls;

    [HideInInspector]
    GamePlayManager gamePlayManager;


    private void Start()
    {
        // Initial text of the finish pit
        gamePlayManager = FindObjectOfType<GamePlayManager>();
        totalRequiredBalls = gamePlayManager.GetRequiredBalls();
        countText.text = "0/" + totalRequiredBalls;
    }
    private void OnCollisionEnter(Collision collision)
    {
        obstacleCounter++;

        
        

        // Update the count text on the ground ( 0 / 20 , 1/20 .. )
        countText.text = obstacleCounter.ToString() + "/" + totalRequiredBalls;

        //player.finishLineStop();
        Debug.Log(obstacleCounter);
    }

    public int ObstaclesCount()
    {
        return obstacleCounter;
    }
    
}
