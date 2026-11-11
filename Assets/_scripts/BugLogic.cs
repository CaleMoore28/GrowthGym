using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Unity.AI.Navigation.Editor;

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

    private int growTime = 0;
    public int growTimeToStage2 = 5;
    public int growTimeToStage3 = 10;

    private IEnumerator coroutine;

    public Mesh currentMesh;
    private Mesh bugPlantModel1;
    private Mesh bugPlantModel2;
    private Mesh bugPlantModel3;

    private MeshFilter meshFilter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void ChangeState()
    {
        switch(bugState)
        {
            default:
                break;

            case BugStates.Stage1:
                currentMesh = bugPlantModel1;
                harvestable = false;
                growTime = growTimeToStage2;
                harvestAmount = 0;
                break;

            case BugStates.Stage2:
                currentMesh = bugPlantModel2;
                harvestable = true;
                growTime = growTimeToStage3;
                harvestAmount = 5;
                break;

            case BugStates.Stage3:
                currentMesh = bugPlantModel3;
                harvestable = true;
                growTime = growTimeToStage3;
                harvestAmount = 20;
                break;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();

        bugPlantModel1 = Resources.Load<Mesh>("BlenderModels/bugplantOne");
        bugPlantModel2 = Resources.Load<Mesh>("BlenderModels/bugplantTwo");
        bugPlantModel3 = Resources.Load<Mesh>("BlenderModels/bugplantThree");

        InitiateGrow();
    }

    private IEnumerator Grow(float growTime)
    {
        yield return new WaitForSeconds(growTime);
    }

    private void InitiateGrow()
    {
        Debug.Log("Initiating grow of stage" + bugState.ToString());

        bugState += 1;
        ChangeState();

        coroutine = Grow(growTime);

        // After done growing, change model
        meshFilter.mesh = currentMesh;

        InitiateGrow();
    }
}