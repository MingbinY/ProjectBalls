using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] collectionPrefab;
    [SerializeField]
    GameObject abilityPickupPrefab;
    GameObject player;
    public int[] abilityPickupScore;
    public Transform topTrans, downTrans, leftTrans, rightTrans;
    float maxZ, minZ, maxX, minX;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxZ = topTrans.position.z;
        minZ = downTrans.position.z;
        maxX = rightTrans.position.x;
        minX = leftTrans.position.x;
    }

    public void SpawnRandomPickup()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        Instantiate(collectionPrefab[Random.Range(0,collectionPrefab.Length)], pos, Quaternion.identity);
        
    }

    public void SpawnPickup()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        Instantiate(collectionPrefab[0], pos, Quaternion.identity);
    }

    public void SpawnAbilityPickup()
    {
        Vector3 pos = new Vector3(Random.Range(minX, maxX), 5, Random.Range(minZ, maxZ));
        while (Vector3.Distance(pos, player.transform.position) < 5 || pos.x < minX || pos.x > maxX || pos.z < minZ || pos.z > maxZ)
        {
            pos = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
        }
        Instantiate(abilityPickupPrefab, pos, Quaternion.identity);
    }
}
