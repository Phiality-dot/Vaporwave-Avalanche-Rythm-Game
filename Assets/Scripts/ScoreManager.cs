using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public float startDelay = 4.3f;
    public float scoreDecrementSpeed = 1.0f;
    public float maxScore = 100f;
    private float currentScore;
    private bool triggered = false;
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI victoryText;
    public Button returnButton;

    private void Start()
    {
        returnButton.enabled = false;
        StartCoroutine(WaitAndStart());
    }
    


    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(startDelay);
        currentScore = maxScore;
        scoreText.text = "Score: " + currentScore;
        StartCoroutine(DecrementScore());
    }

    private IEnumerator DecrementScore()
    {
        while (currentScore > 0)
        {
            yield return new WaitForSeconds(scoreDecrementSpeed);
            currentScore--;
            scoreText.text = "Score: " + currentScore;
        }

        triggered = true;
        victoryText.text = "Time's Up!";
        returnButton.interactable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.tag == "playerTag")
        {
            StopAllCoroutines();
            returnButton.enabled = true;
            victoryText.text = "Level Complete!\nScore: " + currentScore;
            returnButton.interactable = true;
            triggered = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}