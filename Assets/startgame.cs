using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgame : MonoBehaviour
{

    public int levelNumber; //Level Int in Scenes in Build
    public void startGame()
    {
        Application.LoadLevel(levelNumber); //Load Level (n)
    }
}
