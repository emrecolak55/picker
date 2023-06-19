using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    public enum GameStatus {
        MENU,
        PLAYING,
        PAUSED,
        GAMEOVER,
        FINISHED,
        POPUP
    }

    public static int CURRENT_LEVEL;

    [HideInInspector]
    public int currentPercentage;
    public GameObject StageParent;
    public Image stageGraph;
    
    public TextMeshProUGUI currentLevelNumber;
    
    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public float percent;
    public Player player;

    public GameObject settingsPanel;

    public GameObject tabToStartText;

    public GameObject buttons;
    public GameObject endPanel;
    public GameObject endPanelwithFail;
    public GameObject title;

    public GameStatus gameStatus;

    [HideInInspector]
    public int numOfObstacles;
    [HideInInspector]
    public float fillAmount;

    [HideInInspector]
    FinishPit finishPit;

    public int REQUIRED_BALLS = 10;


    
    void Awake()
    {

        Time.timeScale = 1.0f;
        CURRENT_LEVEL = PlayerPrefs.GetInt("CURRENT_LEVEL", 1);


    }

    void Start()
    {
        currentLevelNumber.text = CURRENT_LEVEL.ToString();
        gameStatus = GameStatus.MENU;
        FirstScore();
        
    }
    
    
    void Update()
    {
        if (gameStatus == GameStatus.MENU)
            GetFirstInput();
    }

    public void ButtonSettingsClicked()
    {
        ChangeState(GameStatus.POPUP);
        AudioManager.Instance.PlayEffects(AudioManager.Instance.buttonClick);
        settingsPanel.SetActive(true);
    }

    public void ButtonCloseSettingsClicked()
    {
        ChangeState(GameStatus.MENU);
        AudioManager.Instance.PlayEffects(AudioManager.Instance.buttonClick);
        settingsPanel.SetActive(false);
    }


    //change game states
    public void ChangeState(GameStatus gs)
    {
        gameStatus = gs;
    }

    //init start score / reset slider
    void FirstScore()
    {
        currentPercentage = 0;
    }

    //player start the game
    void GetFirstInput()
    {
        if (Input.GetMouseButton(0) && (!IsPointerOverUIObject() || Input.mousePosition.y < Screen.height / 2) && gameStatus == GameStatus.MENU)
        {
            tabToStartText.SetActive(false);
            buttons.SetActive(false);
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gameMusic);
            AudioManager.Instance.PlayEffects(AudioManager.Instance.buttonClick);
            gameStatus = GameStatus.PLAYING;
            //numOfObstacles = obstacleManager.maxNumOfObstacles;
            StageParent.SetActive(true);
            player.SetFirstTouch();
            title.SetActive(false);
        }
    }

 
    //player fail to finish the level
    public void FinishLineReached()
    {
        
        finishPit = FindObjectOfType<FinishPit>();
        StartCoroutine(FinishPitBallsCounter());
    }

    IEnumerator FinishPitBallsCounter()
    {

        yield return new WaitForSecondsRealtime(2f); // Wait 2 seconds to count the balls
        if(finishPit.ObstaclesCount() >= REQUIRED_BALLS)
        {
            //Debug.Log("INSIDE111111");
            FinishSuccess();
        }
        else
        {
            //Debug.Log("INSIDE222222222222222222");
            FinishWithFail();
        }
    }

    public void FinishWithFail() // With fail
    {
        
        StartCoroutine(LevelFailed());
    }

    //player finish the stage
    public void FinishSuccess()
    {
        StartCoroutine(LevelFinishedWithSuccess());
    }

    //check if mouse pointer is over UI object
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    //show end panel on stage finish
    IEnumerator LevelFinishedWithSuccess()
    {
        ChangeState(GameStatus.FINISHED);
        StageParent.SetActive(false);
        AudioManager.Instance.PlayEffects(AudioManager.Instance.highscore);

        yield return new WaitForSecondsRealtime(1f);

        endPanel.SetActive(true);
    }

    //show end panel on stage fail
    IEnumerator LevelFailed()
    {
        ChangeState(GameStatus.GAMEOVER);
        StageParent.SetActive(false);
        AudioManager.Instance.PlayEffects(AudioManager.Instance.crash);

        yield return new WaitForSecondsRealtime(1f);

        endPanelwithFail.SetActive(true);
    }

    public int GetRequiredBalls()
    {
        return REQUIRED_BALLS;
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

}
