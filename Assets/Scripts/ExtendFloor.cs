using UnityEngine;

public class ExtendFloor : MonoBehaviour
{
    public AudioSource Audiosrc;
    public float playerSpeed = 10f;
    private AudioClip audioClip;
    public float objectHeight = 1f;
    private GameObject player;
    private GameObject cuberef;
    private void Awake()
    {
        player = GameObject.FindWithTag("playerTag");
    }
    private void Start()
    {
        audioClip = Audiosrc.clip;
        LevelInfo.amofsng = (audioClip.length - LevelInfo.secFromStart) - (audioClip.length - LevelInfo.endAt);
        float clipLength = LevelInfo.amofsng;
        float floorLength = transform.localScale.z;
        float distanceToCover = clipLength * playerSpeed;
        float newFloorLength = floorLength + distanceToCover;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newFloorLength);
        Vector3 newEndPosition = transform.position + new Vector3(0f, 0f, newFloorLength / 2 + objectHeight / 2);
        MoveObjects(newEndPosition);
        MovePlayerToStart(floorLength);
    }
    public void MovePlayerToStart(float floorLength)
    {
        cuberef = GameObject.Find("refCube");
        player.transform.position = cuberef.transform.position;



        
    }
    private void MoveObjects(Vector3 newEndPosition)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("EndObj");
        foreach (GameObject obj in objects)
        {
            if (obj.transform.position.z > transform.position.z)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, newEndPosition.z);
            }
        }
    }
    private void Update()
    {
        GameObject mspawnerref = GameObject.Find("masterspawner");
        mspawnerref.transform.position = new Vector3(mspawnerref.transform.position.x, mspawnerref.transform.position.y, player.transform.position.z + 230f);
    }
}
