using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float spawnTime;
    public Bird[] birdPrefabs; // biến lưu lại các con chim đã lưu trong Prefabs

    public int timeLimit;
    int m_curTimeLimit;

    int m_birdKilled;
    bool m_isGameover;

    public bool IsGameover { get => m_isGameover; set => m_isGameover = value; }
    public int BirdKilled { get => m_birdKilled; set => m_birdKilled = value; }

    private void Awake()
    {
        MakeSingleton(false);
        m_curTimeLimit = timeLimit;
    }

    public override void Start()
    {
        // SpawnBird();
        GameGUIManager.Ins.ShowGameGui(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_birdKilled);// update số chim đã bắn hạ

      
    }


    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGui(true);

    }
    IEnumerator TimeCountDown()
    {
        while(m_curTimeLimit > 0 )
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;

            if(m_curTimeLimit <= 0)
            {
                m_isGameover = true;


                if(m_birdKilled > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED: x" + m_birdKilled);

                }
                else if(m_birdKilled < Prefs.bestScore){
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED: x" + Prefs.bestScore);
                }

                //  
                Prefs.bestScore = m_birdKilled;
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.CurDialog = GameGUIManager.Ins.gameDialog;

              

            }
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));

        }
    }
    IEnumerator GameSpawn()
    {
        while(!m_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;

        float randCheck = Random.Range(0f, 1f); // lấy ngẫu nhiên trong khoảng giá trị từ 0 ->1


        if(randCheck >= 0.5f)//chúng ta sẽ kiểm tra giá trị ngẫu nhiên mà >= 0.5 f thì chúng ta sẽ gán giá trị Spawnpos (vị trí tạo ra các con chim) phía bên phải
        {
            spawnPos = new Vector3(11, Random.Range(1.5f,4f),0);
        }else//nếu biến rancheck <= 0.5 f ta sẽ tao ra những con chim ở phía tay trái
        {
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);
        }

        if(birdPrefabs != null && birdPrefabs.Length >0)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length); //ở dây ta lấy ngẫu nhiên giá trị index của mảng

            if(birdPrefabs[randIdx] != null)// kiểm tra nếu mảng giá trị tại vị ví trí ranIdx != null
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }

        }

    }

    string IntToTime(int time)//phương thức chuyển số int sang bên chuỗi thời gian
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        return minutes.ToString("00") + " : " + seconds.ToString("00");


    }
        

}
