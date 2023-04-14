using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    public float score = 0f;
    public Image backgroundOverlay;
    public TMPro.TextMeshProUGUI scoreDisplay;
    public Button returnToMenuButton;

    private bool isFrozen = false;
    private FirstPersonController playerController;

    private void Start()
    {
        playerController = GameObject.FindWithTag("playerTag").GetComponent<FirstPersonController>();
        returnToMenuButton.gameObject.SetActive(false);
        returnToMenuButton.onClick.AddListener(ReturnToMenu);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "playerTag")
        {
            isFrozen = true;
            playerController.enabled = false;
            backgroundOverlay.gameObject.SetActive(true);
            scoreDisplay.gameObject.SetActive(true);
            scoreDisplay.text = "Score: " + score.ToString();
            returnToMenuButton.gameObject.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Update()
    {

    }
}
