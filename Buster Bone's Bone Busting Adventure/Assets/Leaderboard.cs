using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {
    public Text text;
    public GameManager GM;
    public int number;
    void Start()
    {
        GM = GameObject.Find("_GM").GetComponent<GameManager>();
        Array.Sort(GM.score);
        text.text = GM.score[number].ToString();
    }
}
     
