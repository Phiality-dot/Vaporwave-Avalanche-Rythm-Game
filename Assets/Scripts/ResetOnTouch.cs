using UnityEngine;

public class ResetOnTouch : MonoBehaviour
{
    public string playerTag = "playerTag";
    public string spawnTag = "spawnTag";
    public bool dnd = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Reset the level here
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        }
        if (collision.gameObject.CompareTag("sTag"))
        {
            dnd = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("sTag"))
        {
            dnd = false;
        }
    }

    private void Start()
    {
        // Make sure the player has a Collider component
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null && player.GetComponent<Collider>() == null)
        {
            player.AddComponent<CapsuleCollider>();
        }
    }
}
