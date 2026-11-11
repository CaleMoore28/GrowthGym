using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Unity.AI.Navigation.Editor;
using Unity.Jobs;
using UnityEngine.UIElements;

public enum BugStates
{
    Unitialized,
    Stage1,
    Stage2,
    Stage3
}

public class BugLogic : MonoBehaviour
{
    public BugStates bugState = BugStates.Unitialized;

    public bool harvestable = false;

    public int harvestAmount = 0;

    private float growTime = 0.1f;
    public float growTimeToStage2 = 5f;
    public float growTimeToStage3 = 10f;

    private IEnumerator coroutine;

    public Mesh currentMesh;
    private Mesh bugPlantModel1;
    private Mesh bugPlantModel2;
    private Mesh bugPlantModel3;

    public Material currentMaterial;

    private Material bugPlantModel1Mat;
    private Material bugPlantModel2Mat;
    private Material bugPlantModel3Mat;

    private MeshFilter meshFilter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void ChangeState()
    {
        switch (bugState)
        {
            default:
                break;

            case BugStates.Stage1:
                currentMesh = bugPlantModel1;
                currentMaterial = bugPlantModel1Mat;
                harvestable = false;
                growTime = growTimeToStage2;
                harvestAmount = 0;
                break;

            case BugStates.Stage2:
                currentMesh = bugPlantModel2;
                currentMaterial = bugPlantModel2Mat;
                harvestable = true;
                growTime = growTimeToStage3;
                harvestAmount = 5;
                break;

            case BugStates.Stage3:
                currentMesh = bugPlantModel3;
                currentMaterial = bugPlantModel3Mat;
                harvestable = true;
                growTime = growTimeToStage3;
                harvestAmount = 20;
                break;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        gameObject.transform.Rotate(270f, 0, 0);
        gameObject.transform.localScale = new Vector3(20f, 20f, 20f);

        meshFilter = gameObject.GetComponent<MeshFilter>();

        bugPlantModel1 = GameObject.Find("mesh1").GetComponent<MeshFilter>().mesh;
        bugPlantModel2 = GameObject.Find("mesh2").GetComponent<MeshFilter>().mesh;
        bugPlantModel3 = GameObject.Find("mesh3").GetComponent<MeshFilter>().mesh;

        bugPlantModel1Mat = GameObject.Find("mesh1").GetComponent<MeshRenderer>().material;
        bugPlantModel2Mat = GameObject.Find("mesh2").GetComponent<MeshRenderer>().material;
        bugPlantModel3Mat = GameObject.Find("mesh3").GetComponent<MeshRenderer>().material;

        StartGrowing();
    }

    private void StartGrowing()
    {
        if (bugState != BugStates.Stage3)
        {
            StartCoroutine(Grow(growTime));
        }
    }

    private IEnumerator Grow(float growTime)
    {
        yield return new WaitForSeconds(growTime);

        bugState += 1;
        ChangeState();

        // After done growing, change model
        meshFilter.mesh = currentMesh;
        gameObject.GetComponent<MeshRenderer>().material = currentMaterial;

        StartGrowing();
    }
}