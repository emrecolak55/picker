using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int CURRENT_LEVEL;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        CURRENT_LEVEL = PlayerPrefs.GetInt("CURRENT_LEVEL", 1);
        SceneManager.LoadScene(CURRENT_LEVEL - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
