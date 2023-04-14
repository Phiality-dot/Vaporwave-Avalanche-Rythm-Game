using UnityEngine;
using System.Collections;
using System;
public class TeleportGun: MonoBehaviour
{
    public Transform player;
    public float range = 4f;
    public LayerMask layerMask;
    public float coolDown = 1f;
    private float nextFireTime;
    public LineRenderer lineRenderer;
    public Camera plcam;
    public ParticleSystem ps;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(player.transform.position);
                Debug.Log("fire");
                TeleportPlayer(plcam.transform.position + plcam.transform.forward * (range*4));
                Debug.Log(player.transform.position);
                ps.Play();
                nextFireTime = Time.time + coolDown;
                PlayerInfo.charge = 0f;
                StartCoroutine(recharge());
            }
        }
    }

    IEnumerator recharge()
    {
        while (PlayerInfo.charge != 1f)
        {
            PlayerInfo.charge += 1f / 4f;
            yield return new WaitForSeconds(1f);
        }
        yield return 0;
    }

    void TeleportPlayer(Vector3 newPosition)
    {
        Debug.Log("previous player pos is " + player.transform.position);
        Debug.Log("Attempting to tp to " + newPosition);
        Collider[] intersecting = Physics.OverlapSphere(newPosition, 0.01f);
        foreach (Collider collider in intersecting)
        {
            if (collider.CompareTag("zTag"))
            {
                PlayerInfo.charge = 1f;
                break;
            } else
            {
                player.transform.position = newPosition;
            }
        }
        
        Debug.Log("Current player pos is " + player.transform.position);
    }
}
