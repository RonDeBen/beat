using UnityEngine;
using System.Collections;

public class GenerateLayout : MonoBehaviour {

    public float leftOffsetPercentage;
    public float rightOffsetPercentage;
    public float topOffsetPercentage;
    public float bottomOffsetPercentage;
    public float playerTopOffsetPercentage;

    public GameObject firstChannelObj, secondChannelObj, thirdChannelObj, fourthChannelObj;

    public GameObject firstRunnerObj, secondRunnerObj, thirdRunnerObj, fourthRunnerObj;

    public float channelSpacingPercentage;

    private float screenHeight, screenWidth;

	void Awake () {
	    screenHeight = 2f * Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;

        leftOffsetPercentage = returnDecimal(leftOffsetPercentage);
        rightOffsetPercentage = returnDecimal(rightOffsetPercentage);
        topOffsetPercentage = returnDecimal(topOffsetPercentage);
        bottomOffsetPercentage = returnDecimal(bottomOffsetPercentage);

        float channelWidth = (screenWidth - (screenWidth * (leftOffsetPercentage + rightOffsetPercentage + 3 * channelSpacingPercentage))) / 4;
        float channelHeight = screenHeight - (screenHeight * (topOffsetPercentage + bottomOffsetPercentage));

        positionChannels(channelWidth, channelHeight);

	}

    private void positionChannels(float channelWidth, float channelHeight){

        float laneWidth = channelWidth / 3;

        Channel firstChannel = firstChannelObj.GetComponent<Channel>();
        Channel secondChannel = secondChannelObj.GetComponent<Channel>();
        Channel thirdChannel = thirdChannelObj.GetComponent<Channel>();
        Channel fourthChannel = fourthChannelObj.GetComponent<Channel>();

        RunnerController firstRC = firstRunnerObj.GetComponent<RunnerController>();
        RunnerController secondRC = secondRunnerObj.GetComponent<RunnerController>();
        RunnerController thirdRC = thirdRunnerObj.GetComponent<RunnerController>();
        RunnerController fourthRC = fourthRunnerObj.GetComponent<RunnerController>();

        firstChannel.SetDimensions(laneWidth, channelHeight);
        secondChannel.SetDimensions(laneWidth, channelHeight);
        thirdChannel.SetDimensions(laneWidth, channelHeight);
        fourthChannel.SetDimensions(laneWidth, channelHeight);

        Vector3 topLeft = new Vector3((laneWidth / 2) + screenWidth * leftOffsetPercentage, (channelHeight / 2) + (screenHeight * bottomOffsetPercentage), 0);
        Vector3 runnerPos = new Vector3(topLeft.x + laneWidth, screenHeight - (screenHeight * playerTopOffsetPercentage), -1f);

        firstChannel.SetChannelX(topLeft, laneWidth);
        firstRC.SetRunnerPos(runnerPos);

        topLeft += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        runnerPos += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        secondChannel.SetChannelX(topLeft, laneWidth);
        secondRC.SetRunnerPos(runnerPos);

        topLeft += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        runnerPos += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        thirdChannel.SetChannelX(topLeft, laneWidth);
        thirdRC.SetRunnerPos(runnerPos);

        topLeft += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        runnerPos += new Vector3(channelWidth + screenWidth * channelSpacingPercentage, 0 , 0);
        fourthChannel.SetChannelX(topLeft, laneWidth);
        fourthRC.SetRunnerPos(runnerPos);

    }

    private void positionRunners(float runnerWidth){

    }

    private float returnDecimal(float percent){
        if(percent > 1){
            return percent / 100;
        } else {
            return percent;
        }
    }



}
