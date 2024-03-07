using UnityEngine;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject roadSidePrefabWhite;
    public GameObject roadSidePrefabRed;
    public GameObject coinPrefab;
    public GameObject blockPrefab; // Add a reference to the block prefab
    public Transform greenCar;
    public float roadLength = 10f;
    public int numberOfRoadSides = 10;
    public int maxTotalRoadSides = 50; // Set your desired maximum total road sides
    public int numberOfCoins = 1; // Number of coins in each road segment
    public int numberOfBlocks = 1; // Number of blocks in each road segment

    private Transform currentRoad;
    private Transform nextRoad;
    private List<GameObject> roadSides = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();
    private List<GameObject> blocks = new List<GameObject>();
 

    void Start()
    {
        InstantiateRoad();
    }

    void Update()
    {
        // Check if the green car has passed the current road
        if (greenCar.position.z > currentRoad.position.z + roadLength - 6f)
        {
            // Check if there is a current road before attempting to destroy it
            if (currentRoad != null)
            {
                Destroy(currentRoad.gameObject);
            }

            // Destroy associated road sides
            foreach (var roadSide in roadSides)
            {
                Destroy(roadSide);
            }
            roadSides.Clear(); // Clear the list

            // Destroy associated coins
            foreach (var coin in coins)
            {
                Destroy(coin);
            }
            coins.Clear(); // Clear the list

            // Destroy associated blocks
            foreach (var block in blocks)
            {
                Destroy(block);
            }
            blocks.Clear(); // Clear the list

            InstantiateRoad();
        }
    }

    void InstantiateRoad()
    {
        // Instantiate a new road prefab and set it as the next road
        nextRoad = Instantiate(roadPrefab, new Vector3(0f, -1.53f, currentRoad ? currentRoad.position.z + roadLength : 0f), Quaternion.identity).transform;

        // Set the new road as the current road
        currentRoad = nextRoad;

        // Instantiate road sides
        InstantiateRoadSides(greenCar.position.z);

        // Instantiate coins in the road segment
        InstantiateCoins(currentRoad.position.z);

        // Instantiate blocks in the road segment
        InstantiateBlocks(currentRoad.position.z);
    }

    void InstantiateRoadSides(float roadZPosition)
    {
        int currentTotalRoadSides = roadSides.Count;

        // Calculate how many additional road sides can be instantiated
        int remainingRoadSides = Mathf.Min(maxTotalRoadSides - currentTotalRoadSides, numberOfRoadSides);

        // Instantiate road sides on the left
        InstantiateMultipleRoadSides(-6.43f, roadZPosition, roadSidePrefabWhite, roadSidePrefabRed, remainingRoadSides);

        // Instantiate road sides on the right
        InstantiateMultipleRoadSides(6.43f, roadZPosition, roadSidePrefabWhite, roadSidePrefabRed, remainingRoadSides);
    }

    void InstantiateMultipleRoadSides(float xOffset, float roadZPosition, GameObject prefab1, GameObject prefab2, int count)
    {
        for (int i = 0; i < count+8; i++)
        {
            float yOffset = i * 2f; // Adjust the yOffset based on your desired spacing

            // Alternate between white and red road sides
            GameObject roadSidePrefab = i % 2 == 0 ? prefab1 : prefab2;

            GameObject roadSide = Instantiate(roadSidePrefab, new Vector3(xOffset, -1.53f, roadZPosition + yOffset), Quaternion.identity);
            roadSides.Add(roadSide); // Add the instantiated road side to the list
        }
    }

    void InstantiateCoins(float roadZPosition)
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            float xOffset = Random.Range(-5f, 5f); // Adjust the xOffset based on your desired range
            float zOffset = i * 2f; // Adjust the zOffset based on your desired spacing

            Vector3 coinPosition = new Vector3(xOffset, -1f, roadZPosition + zOffset);
            GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
            coins.Add(coin); // Add the instantiated coin to the list
        }
    }

    void InstantiateBlocks(float roadZPosition)
    { 
        for (int i = 0; i < numberOfBlocks; i++)
        {
            float xOffset = Random.Range(-5f, 5f); // Adjust the xOffset based on your desired range
            float zOffset = i * 2f; // Adjust the zOffset based on your desired spacing

            Vector3 blockPosition = new Vector3(xOffset, -1f, roadZPosition + zOffset);
            GameObject block = Instantiate(blockPrefab, blockPosition, Quaternion.identity);
            blocks.Add(block); // Add the instantiated block to the list
        }
    }

    // Called when the car collects a coin
}
