using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour
{
    GamePlayManager gamePlayManagerScript;

    [HideInInspector]
    public float maxXRight = 5.4f;
    [HideInInspector]
    public float maxXLeft = -4.5f;

    
    public float moveForwardSpeed = 8f;
    [HideInInspector]
    public float dragTreshold = 3f;
    [Header("Player Y offset")]
    public float yOffset = 0f;
    [HideInInspector]
    public bool isDead;
    bool hitObstacle;

    int previousId;
    float deltaPos;
    float pos = 3;
    float targetAngle;
    Rigidbody rgdBdy;
    Vector3 mousePos;
    Vector3 mouseStartPos;
    Vector3 velocity = Vector3.zero;

    [HideInInspector]
    public bool drag;

    void Start()
    {
        gamePlayManagerScript = GameObject.Find("GamePlayManager").GetComponent<GamePlayManager>();

        Time.timeScale = 1.0f;
        Application.targetFrameRate = 60;
        targetAngle = 90;

        rgdBdy = GetComponent<Rigidbody>();

        hitObstacle = true;
        transform.position = new Vector3(0, yOffset, pos);
    }

    
    //set player first tap on screen
    public void SetFirstTouch()
    {
        drag = true;
        mouseStartPos = Input.mousePosition;
        mouseStartPos.z = 10;
        deltaPos = transform.position.x;
        hitObstacle = false;
    }

    void Update()
    {
        //when player playing
        if (gamePlayManagerScript.gameStatus == GamePlayManager.GameStatus.PLAYING)
        {
            
            if (Mathf.Abs(transform.position.x) > 4.6f) // 3.36 yı değiş - - - - - - - --  - -- - - - - - - - -------
            {
                // 
            }

            if (Input.GetMouseButtonDown(0))
            {
                drag = true;
                mouseStartPos = Input.mousePosition;
                mouseStartPos.z = 10;
                deltaPos = transform.position.x;
            }

            if (drag)
            {
                mousePos = Input.mousePosition;
                mousePos.z = 10; // select distance = 10 units from the camera
            }

            if (Input.GetMouseButtonUp(0))
            {
                drag = false;
            }

            // The player keeps moving forward
            if (!hitObstacle)
            {
                pos += Time.deltaTime * moveForwardSpeed;
                var playerXPos = deltaPos + dragTreshold * (Camera.main.ScreenToWorldPoint(mousePos).x - Camera.main.ScreenToWorldPoint(mouseStartPos).x);
                if (playerXPos > maxXRight)
                {
                    playerXPos = maxXRight;
                }
                else if ( playerXPos < maxXLeft)
                {
                    playerXPos = maxXLeft;
                }

                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(playerXPos, yOffset, pos), ref velocity, .05f);
                transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, targetAngle, 0), .08f);
                
            }
        } //player finished the stage
        else if (gamePlayManagerScript.gameStatus == GamePlayManager.GameStatus.FINISHED && moveForwardSpeed > 0)
        {
            pos += Time.deltaTime * moveForwardSpeed;
            moveForwardSpeed -= 3 * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(deltaPos + dragTreshold * (Camera.main.ScreenToWorldPoint(mousePos).x - Camera.main.ScreenToWorldPoint(mouseStartPos).x), yOffset, pos), ref velocity, .05f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, targetAngle, 0), .05f);

            if (moveForwardSpeed <= 0)
            {
                moveForwardSpeed = 0;
                rgdBdy.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

    }

 

    public void finishLineStop()
    {
        moveForwardSpeed = 0;
        gamePlayManagerScript.gameStatus = GamePlayManager.GameStatus.GAMEOVER;
        gamePlayManagerScript.FinishLineReached();
        return;
    }

}




