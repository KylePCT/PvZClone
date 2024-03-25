using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Plant_InstantKills : Plant
{
    private GameObject thisObject;

    [Header("Instant Kills")]
    public bool canKill;
    public float actionDelay;

    public Vector2 killArea = Vector2.zero;
    public Vector2 killAreaOffset = Vector2.zero;

    private List<GameObject> zombiesToKill;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    public void Initialise()
    {
        thisObject = gameObject;
        canKill = true;
        zombiesToKill = new List<GameObject>();

        CreateKillArea();
        StartCoroutine("Kill");
    }

    private void CreateKillArea()
    {
        if (killArea == null || killArea == Vector2.zero) return;

        BoxCollider killAreaCol = thisObject.AddComponent<BoxCollider>();
        killAreaCol.isTrigger = true;
        killAreaCol.center = new Vector3(killAreaOffset.x, 0, killAreaOffset.y);
        killAreaCol.size = new Vector3(killArea.x, 1, killArea.y);

    }

    private void OnTriggerEnter(Collider zombie)
    {
        if (zombie.CompareTag("Zombie")) zombiesToKill.Add(zombie.gameObject);
    }

    private void OnTriggerExit(Collider zombie)
    {
        zombiesToKill.Remove(zombie.gameObject);
    }

    IEnumerator Kill()
    {
        while (canKill)
        {
            //Wait for seconds.
            yield return new WaitForSeconds(actionDelay);

            if (zombiesToKill.Count == 0) yield return null;
            else
            {
                foreach (GameObject zom in zombiesToKill)
                {
                    Destroy(zom);
                }
            }
            Destroy(thisObject);
        }

        while (!canKill)
        {
            yield return null;
        }
    }

    private void Update()
    {
        
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(thisObject.GetComponent<Transform>().position + new Vector3(killAreaOffset.x, 0, killAreaOffset.y), 
                            new Vector3(killArea.x, 1, killArea.y));
    }
    */
}
