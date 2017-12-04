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
    public Transform negativeBar;
    public Transform positiveBarContainer;
    public Transform positiveBar;
    public TMPro.TextMeshPro[] firedTexts;
    public TMPro.TextMeshPro scoreText;

    void Start ()
    {
        m_game = FindObjectOfType<GameController>();
	}
	
	void Update ()
    {
        int score = m_game.GetScore();
        float scoreRange = m_game.threeStarScore - m_game.loseScoreLimit;
        float negativeRange = m_game.oneStarScore - m_game.loseScoreLimit;
        float positiveRange = scoreRange - negativeRange;
        float negativeRatio = negativeRange / scoreRange;
        float positiveRatio = positiveRange / scoreRange;

        Debug.Log(negativeRatio);
        Debug.Log(positiveRatio);

        Vector3 negativeContainerScale = negativeBarContainer.localScale;
        negativeContainerScale.z = negativeRatio;
        negativeBarContainer.localScale = negativeContainerScale;

        Vector3 positiveContainerScale = positiveBarContainer.localScale;
        positiveContainerScale.z = positiveRatio;
        positiveBarContainer.localScale = positiveContainerScale;
        Vector3 positiveContainerPosition = positiveBarContainer.localPosition;
        positiveContainerPosition.z = negativeRatio;
        positiveBarContainer.localPosition = positiveContainerPosition;

        float scoreRatio = ((float)(score - m_game.loseScoreLimit)) / scoreRange;

        if (scoreRatio <= 0.0f)
        {
            negativeBar.gameObject.SetActive(false);
            positiveBar.gameObject.SetActive(false);
        }
        else if (scoreRatio < negativeRatio)
        {
            negativeBar.gameObject.SetActive(true);
            positiveBar.gameObject.SetActive(false);

            Vector3 negativeScale = negativeBar.localScale;
            negativeScale.z = scoreRatio;
            negativeBar.localScale = negativeScale;
        }
        else
        {
            negativeBar.gameObject.SetActive(true);
            positiveBar.gameObject.SetActive(true);

            Vector3 negativeScale = negativeBar.localScale;
            negativeScale.z = negativeRatio;
            negativeBar.localScale = negativeScale;

            Vector3 positiveScale = positiveBar.localScale;
            positiveScale.z = scoreRatio - negativeRatio;
            positiveBar.localScale = positiveScale;
            Vector3 positivePosition = positiveBar.localPosition;
            positivePosition.z = negativeRatio;
            positiveBar.localPosition = positivePosition;
        }

        if (m_game.IsGameOver() && score < m_game.oneStarScore)
        {
            if (!m_hasBlinkStarted)
            {
                m_hasBlinkStarted = true;
                m_blinkStartTime = Time.time;
            }

            scoreText.gameObject.SetActive(false);

            float t = ((Time.time - m_blinkStartTime) % firedBlinkPhase) / firedBlinkPhase;

            if (t < 0.5f)
            {
                foreach (TMPro.TextMeshPro text in firedTexts)
                {
                    text.gameObject.SetActive(true);
                    text.color = firedOnColor;
                }
            }
            else
            {
                foreach (TMPro.TextMeshPro text in firedTexts)
                {
                    text.gameObject.SetActive(false);
                    text.color = firedOffColor;
                }

                scoreText.gameObject.SetActive(true);
                scoreText.text = score.ToString("000");
                scoreText.color = negativeBarContainer.GetComponentInChildren<MeshRenderer>().material.color;
            }
            
        }
        else
        {
            m_hasBlinkStarted = false;

            foreach (TMPro.TextMeshPro text in firedTexts)
            {
                text.gameObject.SetActive(false);
                text.color = firedOffColor;
            }

            scoreText.gameObject.SetActive(true);
            scoreText.text = score.ToString("000");

            if (scoreRatio < negativeRatio)
            {
                scoreText.color = negativeBarContainer.GetComponentInChildren<MeshRenderer>().material.color;
            }
            else
            {
                scoreText.color = positiveBarContainer.GetComponentInChildren<MeshRenderer>().material.color;
            }
        }
    }

    bool m_hasBlinkStarted = false;
    float m_blinkStartTime = 0.0f;
    GameController m_game;
}
