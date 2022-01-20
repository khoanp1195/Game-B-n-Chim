using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
     //lớp này giúp ta xử lý dữ liệu của người dùng cần lưu
    public static int bestScore
    {
        get => PlayerPrefs.GetInt(GameConsts.BEST_SCORE, 0);
        set
        {
            int curScore = PlayerPrefs.GetInt(GameConsts.BEST_SCORE);// biến này lưu điểm số đã lưu trong bộ nhớ
       
            if(value > curScore)
            {
                PlayerPrefs.SetInt(GameConsts.BEST_SCORE, value);

            }
        }

    }

}
