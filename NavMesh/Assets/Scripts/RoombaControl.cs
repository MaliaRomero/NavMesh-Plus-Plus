using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoombaControl : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject dirtPrefab; // Assign in Inspector
    public int dirtSpawnRange = 10; // Defines the area for new dirt placement
    private Transform targetDirt;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindNewDirt();
    }

    void Update()
    {
        if (targetDirt == null)
        {
            FindNewDirt();
        }
        else
        {
            agent.SetDestination(targetDirt.position);

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Destroy(targetDirt.gameObject); // Clean dirt
                SpawnNewDirt(); // Create new dirt
                FindNewDirt(); // Look for the next one
            }
        }
    }

    void FindNewDirt()
    {
        GameObject[] dirtSpots = GameObject.FindGameObjectsWithTag("Dirt");
        if (dirtSpots.Length > 0)
        {
            targetDirt = dirtSpots[Random.Range(0, dirtSpots.Length)].transform;
        }
    }

    void SpawnNewDirt()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();
        Instantiate(dirtPrefab, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPoint = new Vector3(
            Random.Range(-dirtSpawnRange, dirtSpawnRange), 
            0, 
            Random.Range(-dirtSpawnRange, dirtSpawnRange)
        );

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5f, NavMesh.AllAreas))
        {
            return hit.position; // Returns a valid point on the NavMesh
        }

        return GetRandomNavMeshPosition(); // Retry if invalid
    }
}