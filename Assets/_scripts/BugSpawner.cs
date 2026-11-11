using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPlantPrefab;

    public float harvestRadius = 1f;
    public LayerMask layerMask;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPot();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Physics.SphereCast(gameObject.transform.position, harvestRadius, Vector3.zero, out RaycastHit hit, 0, layerMask))
            TryToHarvestPlant();
        }
    }

    public void SpawnPot()
    {
        Instantiate(bugPlantPrefab, gameObject.transform);
    }

    public void TryToHarvestPlant()
    {

    }

    void OnCollisionEnter(Collider colldier)
    {

    }
}