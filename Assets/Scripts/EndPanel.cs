using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public Text textStage;
    public Text percentCompleted;
    public Text buttonText;
    public GamePlayManager gameManager;
    int percent;

    //on enable object init texts
    void OnEnable()
    {
        textStage.text = "Level  " + PlayerPrefs.GetInt("CURRENT_LEVEL", 1);

        //player finished the stage
        if (gameManager.gameStatus == GamePlayManager.GameStatus.FINISHED)
        {
            percentCompleted.text = "Completed!";
            percentCompleted.color = new Color(255/255f,215/255f,0);
            buttonText.text = "Next Level";
        }
        //player fail the stage
        else if (gameManager.gameStatus == GamePlayManager.GameStatus.GAMEOVER)
        {
            if (gameManager.fillAmount < .05f)
                percent = 0;
            else
            {
                percent = (int)Mathf.Round(gameManager.fillAmount * 100);

                if (percent > 99)
                    percent = 99;
            }

            percentCompleted.text = "Could not completed ";
            buttonText.text = "TRY AGAIN";
        }
    }

    //click on button NEXT/TRY AGAIN
    public void ButtonClicked()
    {
        AudioManager.Instance.PlayEffects(AudioManager.Instance.buttonClick);

        // success 
        if (gameManager.gameStatus == GamePlayManager.GameStatus.FINISHED)
        {
            PlayerPrefs.SetInt("CURRENT_LEVEL", PlayerPrefs.GetInt("CURRENT_LEVEL", 1) + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       
        } // if fail


        else if (gameManager.gameStatus == GamePlayManager.GameStatus.GAMEOVER)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    
}
