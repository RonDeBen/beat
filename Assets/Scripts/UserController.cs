using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    public GameObject firstChannelObj;
    public GameObject secondChannelObj;
    public GameObject thirdChannelObj;
    public GameObject fourthChannelObj;

    public GameObject sheetObj;

    private Channel firstChannel, secondChannel, thirdChannel, fourthChannel;
    private Sheet sheet;

    private MusicMiddleware MM;
	
    void Start(){
        firstChannel = firstChannelObj.GetComponent<Channel>();
        secondChannel = secondChannelObj.GetComponent<Channel>();
        thirdChannel = thirdChannelObj.GetComponent<Channel>();
        fourthChannel = fourthChannelObj.GetComponent<Channel>();

        sheet = sheetObj.GetComponent<Sheet>();

        MM = gameObject.GetComponent<MusicMiddleware>() as MusicMiddleware;
    }


	void Update () {
	     bool firstJump = MidiInput.GetKeyDown(36) || Input.GetKeyDown(KeyCode.A);
       bool secondJump = MidiInput.GetKeyDown(37) || Input.GetKeyDown(KeyCode.S);
       bool thirdJump = MidiInput.GetKeyDown(38) || Input.GetKeyDown(KeyCode.D);
       bool fourthJump = MidiInput.GetKeyDown(39) || Input.GetKeyDown(KeyCode.F);

       bool firstShoot = MidiInput.GetKeyDown(40) || Input.GetKeyDown(KeyCode.Q);
       bool secondShoot = MidiInput.GetKeyDown(41) || Input.GetKeyDown(KeyCode.W);
       bool thirdShoot = MidiInput.GetKeyDown(42) || Input.GetKeyDown(KeyCode.E);
       bool fourthShoot = MidiInput.GetKeyDown(43) || Input.GetKeyDown(KeyCode.R);


       //jumping
       if (firstJump){
        firstChannel.RunnerJump();
        MM.playSound("Square Jump 1");
       }
       if (secondJump){
        secondChannel.RunnerJump();
        MM.playSound("Square Jump 2");
       }
       if (thirdJump){
        thirdChannel.RunnerJump();
        MM.playSound("Triangle Jump");
       }
       if (fourthJump){
        fourthChannel.RunnerJump();
        MM.playSound("Noise Jump");
       }

       //shooting
       if (firstShoot){
        firstChannel.RunnerShoot();
        MM.playSound("Square Shot 1");
       }
       if (secondShoot){
        secondChannel.RunnerShoot();
        MM.playSound("Square Shot 2");
       }
       if (thirdShoot){
        thirdChannel.RunnerShoot();
        MM.playSound("Triangle Shot");
       }
       if (fourthShoot){
        fourthChannel.RunnerShoot();
        MM.playSound("Noise Shot");
       }

       //lane swapping
       firstChannel.RunnerSwapLane(MidiInput.GetKnob(1));
       secondChannel.RunnerSwapLane(MidiInput.GetKnob(2));
       thirdChannel.RunnerSwapLane(MidiInput.GetKnob(3));
       fourthChannel.RunnerSwapLane(MidiInput.GetKnob(4));
	}
}
