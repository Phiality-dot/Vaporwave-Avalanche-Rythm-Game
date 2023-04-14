using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class randomobjectspawner : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] defaultMeshes;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public float beatThreshold = 0.5f;
    public float minImpulseForce = 10f;
    public float maxImpulseForce = 30f;
    public Vector3 forceDirection = Vector3.up;
    public Vector3 constantForceDirection = new Vector3(0, -1, 0);
    public float constantForceMagnitude = 10f;
    public float waittime = 4.3f;
    ResetOnTouch rote;
    GameObject player;


    public AudioSource audioSource;
    private float[] samples = new float[512];
    private float[] frequencies = new float[512];
    private void Awake()
    {
        player = GameObject.FindWithTag("playerTag");
    }
    private void Start()
    {
        
        StartCoroutine(WaitAndStart());
        GameObject audioSourceObject = GameObject.Find("cMusic");
        if (audioSourceObject != null)
        {
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            audioSource.time = LevelInfo.secFromStart;
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            GameObject mspawnerref = GameObject.Find("masterspawner");
            mspawnerref.transform.position = new Vector3(mspawnerref.transform.position.x, player.transform.position.y + 60f, player.transform.position.z + 230f);
        } else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            GameObject mspawnerref = GameObject.Find("masterspawner");
            mspawnerref.transform.position = new Vector3(mspawnerref.transform.position.x, player.transform.position.y - 35f, player.transform.position.z + 230f);
        } else
        {

        }
    }

    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(waittime);
        InvokeRepeating("SpawnObjectOnBeat", 0f, 0.1f);
    }

    private void SpawnObjectOnBeat()
    {

        float[] samples = new float[1024];
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

        float maxSample = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] > maxSample)
            {
                maxSample = samples[i];
            }
        }

        if (maxSample > beatThreshold)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int meshIndex = Random.Range(0, defaultMeshes.Length);

            GameObject spawnedObject = Instantiate(defaultMeshes[meshIndex], spawnPoints[spawnIndex].transform.position, Quaternion.identity);
            Rigidbody rb = spawnedObject.AddComponent<Rigidbody>();
            rote = spawnedObject.AddComponent<ResetOnTouch>();
            rb.mass = 0.1f;
            rb.drag = 0f;
            rb.angularDrag = 0f;
            rb.AddForce(constantForceDirection * constantForceMagnitude, ForceMode.Force);
            rb.AddForce(forceDirection * Random.Range(minImpulseForce, maxImpulseForce), ForceMode.Impulse);
            rb.velocity = constantForceDirection * constantForceMagnitude;
            float randomScale = Random.Range(minScale, maxScale);
            spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            spawnedObject.tag = "spawnTag";
            dshit();
            
        }
    }
    private void dshit()
    {
        
        if (LevelInfo.amofsng > 1)
        {
            if (rote.dnd == false)
            {
                foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("spawnTag"))
                {
                    Destroy(spawn);
                }
            } else
            {
                foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("spawnTag"))
                {
                    if (spawn.GetComponent<Rigidbody>().velocity.z >= -9f)
                    {
                        Destroy(spawn);
                    }
                }
            }
        }
    }
}
