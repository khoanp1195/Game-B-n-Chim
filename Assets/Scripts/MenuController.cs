using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   

    public void StartGame()
    {
        SceneManager.LoadScene("Menu");
    }


    public void OptionMenu()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void HighScore()
    {
        SceneManager.LoadScene("HighScore");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    } 
    public void Main()
    {
        SceneManager.LoadScene("Main");
    } 
    public void Hard()
    {
        SceneManager.LoadScene("Hardd");
    }
    public void Hardd()
    {
        SceneManager.LoadScene("Harddd");
    }
    public void Impossible()
    {
        SceneManager.LoadScene("IMPOSSIBLE");
    }
    public void Level()
    {
        SceneManager.LoadScene("Level");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
