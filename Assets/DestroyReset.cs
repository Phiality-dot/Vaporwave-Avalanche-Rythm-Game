using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        BoxCollider selfcolldier = GetComponent<BoxCollider>();
        selfcolldier.isTrigger = true;
        if(collision.gameObject.GetComponent<CharacterController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else
        {
            Destroy(collision.gameObject);
        }
    }
}
