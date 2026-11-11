using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPlantPrefab;

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
            if (Physics.SphereCast(gameObject.transform.position, harvestRadius, Vector3.zero, out RaycastHit hit, 0, plantMask))
            {
                TryToHarvestPlant(hit.transform.gameObject);
            }
        }
    }

    public void SpawnPot()
    {
        Instantiate(bugPlantPrefab, gameObject.transform);
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