using UnityEngine;
using System.Collections;

public class ChannelManager : MonoBehaviour {

	public GameObject firstChannelObj, secondChannelObj, thirdChannelObj, fourthChannelObj;

    private static Channel firstChannel, secondChannel, thirdChannel, fourthChannel;

    void Start(){
        firstChannel = firstChannelObj.GetComponent<Channel>() as Channel;
        secondChannel = secondChannelObj.GetComponent<Channel>() as Channel;
        thirdChannel = thirdChannelObj.GetComponent<Channel>() as Channel;
        fourthChannel = fourthChannelObj.GetComponent<Channel>() as Channel;


        firstChannel.SetChannel("first");
        secondChannel.SetChannel("second");
        thirdChannel.SetChannel("third");
        fourthChannel.SetChannel("fourth");
    }

    public static void infectChannel(string channel){
        switch(channel){
            case "first":
                firstChannel.BecomeInfected();
                break;
            case "second":
                secondChannel.BecomeInfected();
                break;
            case "third":
                thirdChannel.BecomeInfected();
                break;
            case "fourth":
                fourthChannel.BecomeInfected();
                break;
        }
    }
}
