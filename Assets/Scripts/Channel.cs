using UnityEngine;
using System.Collections;

public class Channel : MonoBehaviour {

    public GameObject leftLane;
    public GameObject middleLane;
    public GameObject rightLane;

    public GameObject runner, hurdle, enemy;

    private float leftCenter, middleCenter, rightCenter;
    private LaneController leftController, middleController, rightController;

    private RunnerController runnerController;

    private Vector3 hurdlePos;
    private float hurdleSpeed;

    public Color hurdleColor;
    public Color jumpedOverColor;
    private Color laneColor;

    public Color flashingColor;
    public float cycleTime;

    private string channel;

    public float infectionLength, flashLength;
    public int numberOfFlashes;
    private bool infected, healing;
    private float recoveryTime;

	// Use this for initialization
	void Start () {
	   runnerController = runner.GetComponent<RunnerController>();

       leftController = leftLane.GetComponent<LaneController>() as LaneController;
       middleController = middleLane.GetComponent<LaneController>() as LaneController;
       rightController = rightLane.GetComponent<LaneController>() as LaneController;

       leftController.SetUpLane(leftLane.GetComponent<Renderer>().material.GetColor("_EmissionColor"), flashingColor, cycleTime);
       middleController.SetUpLane(middleLane.GetComponent<Renderer>().material.GetColor("_EmissionColor"), flashingColor, cycleTime);
       rightController.SetUpLane(rightLane.GetComponent<Renderer>().material.GetColor("_EmissionColor"), flashingColor, cycleTime);

       laneColor = leftLane.GetComponent<Renderer>().material.GetColor("_EmissionColor");

       runnerController.SetShotNoise(TheNoise());
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate(){
        if(infected && Time.time > recoveryTime && !healing){
            BecomeUninfected();
        }
    }

    private string TheNoise(){
        switch(channel){
            case "first":
                return "Square Shot 1";
            case "second":
                return "Square Shot 2";
            case "third":
                return "Triangle Shot";
            case "fourth":
                return "Noise Shot";
            default: 
                return "fuck";
        }
    }

    public bool RunnerIsJumping(){
        return runnerController.IsJumping();
    }

    public bool IsInfected(){
        return infected;
    }

    public void BecomeInfected(){
        MusicAnalyzer.MuteChannel(channel);
        infected = true;
        healing = false;
        recoveryTime = Time.time + infectionLength;

        leftLane.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        middleLane.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        rightLane.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);

        leftController.SetInfected(true);
        middleController.SetInfected(true);
        rightController.SetInfected(true);
    }

    public void BecomeUninfected(){
        healing = true;
        StartCoroutine(Flash());
    }

    IEnumerator Flash() {

        Material leftMat = leftLane.GetComponent<Renderer>().material;
        Material middleMat = middleLane.GetComponent<Renderer>().material;
        Material rightMat = rightLane.GetComponent<Renderer>().material;

        for(int k = 0; k < numberOfFlashes; k++){
            leftMat.SetColor("_EmissionColor", Color.black);
            middleMat.SetColor("_EmissionColor", Color.black);
            rightMat.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(flashLength);
            leftMat.SetColor("_EmissionColor", laneColor);
            middleMat.SetColor("_EmissionColor", laneColor);
            rightMat.SetColor("_EmissionColor", laneColor);
            yield return new WaitForSeconds(flashLength);
        }

        infected = false;

        leftController.SetInfected(false);
        middleController.SetInfected(false);
        rightController.SetInfected(false);

        MusicAnalyzer.UnmuteChannel(channel);
    }

    public void RunnerJump(){
        runnerController.Jump();
    }

    public void RunnerShoot(){
        runnerController.Shoot();
    }

    public void RunnerSwapLane(float knob){
        runnerController.SetArrowPosition(knob);
        if (knob != 0.0f){
            if (knob < 0.33f){
                runnerController.SwapLane(leftLane.transform.position.x);
            } else if (knob < 0.66f){
                runnerController.SwapLane(middleLane.transform.position.x);
            } else {
                runnerController.SwapLane(rightLane.transform.position.x);
            }
        }
    }

    public void SpawnEnemy(int position){
        Vector3 enemyPos = hurdlePos;
        enemyPos.y += enemy.transform.localScale.y / 2;
        LaneController LC = middleController;
        switch(position){
            case 1:
                enemyPos.x = leftLane.transform.position.x;
                leftController.StartFlashing();
                LC = leftController;
                break;
            case 2:
                enemyPos.x = middleLane.transform.position.x;
                middleController.StartFlashing();
                LC = middleController;
                break;
            case 3:
                enemyPos.x = rightLane.transform.position.x;
                rightController.StartFlashing();
                LC = rightController;
                break;
        }

        GameObject go = Instantiate(enemy, enemyPos, runner.transform.rotation) as GameObject;

        float theta = middleLane.transform.eulerAngles.x;

        float y = Mathf.Sin(theta);
        float z = -Mathf.Cos(theta);

        go.GetComponent<Rigidbody>().velocity = new Vector3(0, y, z) * (hurdleSpeed / 3);
        go.GetComponent<EnemyController>().SetLaneController(LC, channel);
    }

    public void SpawnHurdle(){
        GameObject go = Instantiate(hurdle, hurdlePos, middleLane.transform.rotation) as GameObject;

        float theta = go.transform.eulerAngles.x;

        float y = Mathf.Sin(theta);
        float z = -Mathf.Cos(theta);

        go.GetComponent<Rigidbody>().velocity = new Vector3(0, y, z) * hurdleSpeed;
        go.GetComponent<Renderer>().material.SetColor("_EmissionColor", hurdleColor);
        go.GetComponent<HurdleController>().SetupHurdle(runner.transform.position, jumpedOverColor, laneColor, Color.magenta, cycleTime, infected);
    }

    public void SetDimensions(float laneWidth, float laneHeight){
        leftLane.transform.localScale = new Vector3(laneWidth, laneHeight, 0f);
        middleLane.transform.localScale = new Vector3(laneWidth, laneHeight, 0f);
        rightLane.transform.localScale = new Vector3(laneWidth, laneHeight, 0f);
    }

    public void SetChannelX(Vector3 topLeft, float laneWidth){
        leftLane.transform.position = topLeft;
        middleLane.transform.position = topLeft + new Vector3(laneWidth, 0, 0);
        rightLane.transform.position = topLeft + new Vector3(laneWidth * 2, 0, 0);
    }

    public void SetUpHurdles(int beatsPerMeasure, float beat){

        float measureLength = Vector3.Distance(hurdlePos, runner.transform.position);
        float hurdleLength = measureLength / beatsPerMeasure;

        Vector3 temp = hurdle.transform.localScale;
        temp.y = hurdleLength;
        hurdle.transform.localScale = temp;

        float laneLength = middleLane.transform.localScale.y;
        float theta = middleLane.transform.eulerAngles.x;

        float hypotenuse = (laneLength / 2) + (hurdleLength / 2);

        float newY = middleLane.transform.position.y - (Mathf.Sin(theta) * hypotenuse);
        float newZ = middleLane.transform.position.z + (Mathf.Cos(theta) * hypotenuse);

        hurdlePos = new Vector3(middleLane.transform.position.x, newY, newZ);
        hurdleSpeed = (Vector3.Distance(hurdlePos, runner.transform.position)) * beat;

    }

    public float GetChannelHeight(){
        return middleLane.transform.localScale.y;
    }

    public void SetChannel(string channel){
        this.channel = channel;
    }



}
