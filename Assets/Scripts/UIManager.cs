using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   // public GameObject QuitPanel;
    GameObject GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameMaster");
    }
   public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   public void NextLevel()
    {
        if (Application.levelCount - 1 != SceneManager.GetActiveScene().buildIndex)
        {
            Destroy(GM);
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        }
        else
        {
            Destroy(GM);
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
   public void Restart()
    {
        Destroy(GM);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
    //public void QuitPannel()
    //{
    //    QuitPanel.SetActive(true);
    //    Time.timeScale = 0;
    //}
    //public void No()
    //{
    //    QuitPanel.SetActive(false);
    //    Time.timeScale = 1;
    //}
}
