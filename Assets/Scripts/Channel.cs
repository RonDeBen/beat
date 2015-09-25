using UnityEngine;
using System.Collections;

public class Channel : MonoBehaviour {

    public GameObject leftLane;
    public GameObject middleLane;
    public GameObject rightLane;

    public GameObject runner, hurdle, enemy;

    private float leftCenter, middleCenter, rightCenter;

    private RunnerController runnerController;

    private Vector3 hurdlePos;
    private float hurdleSpeed;

	// Use this for initialization
	void Start () {
	   runnerController = runner.GetComponent<RunnerController>();

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void RunnerJump(){
        runnerController.Jump();
    }

    public void RunnerShoot(){
        runnerController.Shoot();
    }

    public void RunnerSwapLane(float knob){
        if (knob != 0.0f){
            if (knob < 0.40f){
                runnerController.SwapLane(leftLane.transform.position.x);
            } else if (knob < 0.60f){
                runnerController.SwapLane(middleLane.transform.position.x);
            } else {
                runnerController.SwapLane(rightLane.transform.position.x);
            }
        }
    }

    public void SpawnEnemy(int position){
        Vector3 enemyPos = hurdlePos;
        enemyPos.y += enemy.transform.localScale.y / 2;
        switch(position){
            case 1:
                enemyPos.x = leftLane.transform.position.x;
                break;
            case 2:
                enemyPos.x = middleLane.transform.position.x;
                break;
            case 3:
                enemyPos.x = rightLane.transform.position.x;
                break;
        }

        GameObject go = Instantiate(enemy, enemyPos, runner.transform.rotation) as GameObject;

        float theta = middleLane.transform.eulerAngles.x;

        float y = Mathf.Sin(theta);
        float z = -Mathf.Cos(theta);

        go.GetComponent<Rigidbody>().velocity = new Vector3(0, y, z) * (hurdleSpeed / 2);
    }

    public void SpawnHurdle(){
        GameObject go = Instantiate(hurdle, hurdlePos, middleLane.transform.rotation) as GameObject;

        float theta = go.transform.eulerAngles.x;

        float y = Mathf.Sin(theta);
        float z = -Mathf.Cos(theta);

        go.GetComponent<Rigidbody>().velocity = new Vector3(0, y, z) * hurdleSpeed;
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



}
