using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPlantPrefab;

    public GameObject growPosition;

    public Transform plantHolders;

    public int collectedBugs = 0;

    public float harvestRadius = 1f;
    public LayerMask plantMask;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPot();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Collider[] plantsColliders = Physics.OverlapSphere(growPosition.transform.position, harvestRadius, plantMask);

            Debug.Log("Hit colliders are " + plantsColliders);

            foreach (Collider collider in plantsColliders)
            {
                Debug.Log("Collider is " + collider);
                TryToHarvestPlant(collider.transform.gameObject);
            }
        }
    }

    public void SpawnPot()
    {
        Instantiate(bugPlantPrefab, growPosition.transform.position, Quaternion.identity, plantHolders);
    }

    public void TryToHarvestPlant(GameObject plantToHarvest)
    {
        Debug.Log("Tried to harvest plant");

        BugLogic bugPlantScript = plantToHarvest.GetComponent<BugLogic>();

        if (bugPlantScript.harvestable == true)
        {
            collectedBugs += bugPlantScript.harvestAmount;
            Destroy(plantToHarvest);

            Debug.Log("Current bug total is " + collectedBugs);
        }
    }
}