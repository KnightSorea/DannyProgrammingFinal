using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Highscore : MonoBehaviour
{
    public TextMeshProUGUI highscore;
    void Start()
    {
        highscore.text = $"HighScore:{PlayerPrefs.GetFloat("HighScore")}";
    }
}
