using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimerController : MonoBehaviour {

    public TMPro.TextMeshPro timerText;

	// Use this for initialization
	void Start ()
    {
        m_game = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float remainingTime = m_game.GetRemainingTime();

        timerText.text = "" + ((int)(remainingTime / 60.0f)).ToString("00") + ":" + ((int)(remainingTime % 60.0f)).ToString("00");
    }

    GameController m_game;
}
