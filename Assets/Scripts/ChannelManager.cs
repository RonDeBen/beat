using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChannelManager : MonoBehaviour {

	public GameObject firstChannelObj, secondChannelObj, thirdChannelObj, fourthChannelObj;

    public static Channel firstChannel, secondChannel, thirdChannel, fourthChannel;

    void Awake(){
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

    public static int NumberOfPossibilities(){
        return ChannelPossibilities().Count;
    }

    public static List<int> ChannelPossibilities(){

        List<int> possibilities = new List<int>();
        if(!firstChannel.IsInfected()){
            possibilities.Add(0);
        }
        if(!secondChannel.IsInfected()){
            possibilities.Add(1);
        }
        if(!thirdChannel.IsInfected()){
            possibilities.Add(2);
        }
        if(!fourthChannel.IsInfected()){
            possibilities.Add(3);
        }
        return possibilities;
    }

    public static bool IsThisChannelInfected(string channel){
        switch(channel){
            case "first":
                return firstChannel.IsInfected();
            case "second":
                return secondChannel.IsInfected();
            case "third":
                return thirdChannel.IsInfected();
            case "fourth":
                return fourthChannel.IsInfected();
        }
        return false;
    }
}
