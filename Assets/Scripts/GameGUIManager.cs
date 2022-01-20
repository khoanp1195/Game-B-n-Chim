using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject homeGui;
    public GameObject gameGui;

    public Dialog gameDialog;
    public Dialog pauseDialog;

    public Image fireRateFilled;
    public Text timerText;
    public Text killedCountingText;

    Dialog m_curDialog;// biến lưu lại Dialog hiện của chúng ta

    public Dialog CurDialog { get => m_curDialog; set => m_curDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);//không lưu dữ liệu khi load Scene

    }

    public void ShowGameGui(bool isShow)
    {
        if (gameGui)// kiểm tra nếu GameGui khác null
            gameGui.SetActive(isShow);// để hiển thị hoặc ẩn một GameObject ta sử dụng setActive
        if (homeGui)
            homeGui.SetActive(!isShow);// nếu isshow = true dc hiển thị
    }
    public void UpdateTimer(string time)
    {
        if (timerText)//kiểm tra timerTExt != null
            timerText.text = time;
       }
    public void UpdateKilledCounting(int killed)
    {
        if (killedCountingText)
            killedCountingText.text = "x" + killed.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        if (fireRateFilled)
            fireRateFilled.fillAmount = rate;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // dừng tất cả các hoạt động của game
        if(pauseDialog)
        {
            pauseDialog.Show(true);
            pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED " + Prefs.bestScore);
            m_curDialog = pauseDialog;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (m_curDialog)
            m_curDialog.Show(false);
    }
    public void Home()
    {
        Application.LoadLevel("Menu");
    }
    public void BackToHome()
    {
        ResumeGame();
        //để load scene ta sẽ sử dựng SceneManager
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Replay()
    {
        if (m_curDialog)
            m_curDialog.Show(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //ShowGameGui(true);
        //GameManager.Ins.PlayGame();
    }

    public void ExiGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();

    }
}
