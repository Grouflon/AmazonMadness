using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePerformancePannelController : MonoBehaviour
{
    [Header("Fired Blink")]
    public float firedBlinkPhase = 2.0f;
    public Color firedOnColor;
    public Color firedOffColor;

    [Header("Internal Objects")]
    public Transform negativeBarContainer;
    public Transform positiveBarContainer;
    public TMPro.TextMeshPro[] firedTexts;

	void Start ()
    {
        m_game = FindObjectOfType<GameController>();
	}
	
	void Update ()
    {
        int score = m_game.GetScore();

        {
            float loseRatio = Mathf.Clamp01((float)Mathf.Min(score, 0) / (float)m_game.loseScoreLimit);
            Vector3 negativeScale = negativeBarContainer.transform.localScale;
            negativeScale.x = loseRatio;
            negativeBarContainer.transform.localScale = negativeScale;

            negativeBarContainer.gameObject.SetActive(Mathf.Abs(loseRatio) > Mathf.Epsilon);
        }

        {
            float winRatio = Mathf.Clamp01((float)Mathf.Max(score, 0) / (float)m_game.threeStarScore);
            Vector3 posititveScale = positiveBarContainer.transform.localScale;
            posititveScale.x = winRatio;
            positiveBarContainer.transform.localScale = posititveScale;

            positiveBarContainer.gameObject.SetActive(Mathf.Abs(winRatio) > Mathf.Epsilon);
        }

        if (score <= m_game.loseScoreLimit)
        {
            if (!m_hasBlinkStarted)
            {
                m_hasBlinkStarted = true;
                m_blinkStartTime = Time.time;
            }

            float t = ((Time.time - m_blinkStartTime) % firedBlinkPhase) / firedBlinkPhase;

            foreach (TMPro.TextMeshPro text in firedTexts)
            {
                if (t < 0.5f)
                {
                    text.color = firedOnColor;
                }
                else
                {
                    text.color = firedOffColor;
                }
            }
        }
        else
        {
            m_hasBlinkStarted = false;

            foreach (TMPro.TextMeshPro text in firedTexts)
            {
                text.color = firedOffColor;
            }
        }
    }

    bool m_hasBlinkStarted = false;
    float m_blinkStartTime = 0.0f;
    GameController m_game;
}
