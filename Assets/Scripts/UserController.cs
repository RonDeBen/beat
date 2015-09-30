using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    public GameObject firstChannelObj;
    public GameObject secondChannelObj;
    public GameObject thirdChannelObj;
    public GameObject fourthChannelObj;

    private Channel firstChannel, secondChannel, thirdChannel, fourthChannel;
	
    void Start(){
        firstChannel = firstChannelObj.GetComponent<Channel>();
        secondChannel = secondChannelObj.GetComponent<Channel>();
        thirdChannel = thirdChannelObj.GetComponent<Channel>();
        fourthChannel = fourthChannelObj.GetComponent<Channel>();
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

       bool firstInfect = Input.GetKeyDown(KeyCode.Z);
       bool secondInfect = Input.GetKeyDown(KeyCode.X);
       bool thirdInfect = Input.GetKeyDown(KeyCode.C);
       bool fourthInfect = Input.GetKeyDown(KeyCode.V);


       //jumping
       if (firstJump && !firstChannel.IsInfected()){
          firstChannel.RunnerJump();
          MusicMiddleware.playSound("Square Jump 1");
       }
       if (secondJump && !secondChannel.IsInfected()){
          secondChannel.RunnerJump();
          MusicMiddleware.playSound("Square Jump 2");
       }
       if (thirdJump && !thirdChannel.IsInfected()){
          thirdChannel.RunnerJump();
          MusicMiddleware.playSound("Triangle Jump");
       }
       if (fourthJump && !fourthChannel.IsInfected()){
          fourthChannel.RunnerJump();
          MusicMiddleware.playSound("Noise Jump");
       }

       //shooting
       if (firstShoot && !firstChannel.IsInfected()){
          firstChannel.RunnerShoot();
          MusicMiddleware.playSound("Square Shot 1");
       }
       if (secondShoot && !secondChannel.IsInfected()){
          secondChannel.RunnerShoot();
          MusicMiddleware.playSound("Square Shot 2");
       }
       if (thirdShoot && !thirdChannel.IsInfected()){
          thirdChannel.RunnerShoot();
          MusicMiddleware.playSound("Triangle Shot");
       }
       if (fourthShoot && !fourthChannel.IsInfected()){
          fourthChannel.RunnerShoot();
          MusicMiddleware.playSound("Noise Shot");
       }

       //infecting
       if(firstInfect){
          firstChannel.BecomeInfected();
       }
       if(secondInfect){
        secondChannel.BecomeInfected();
       }
       if(thirdInfect){
        thirdChannel.BecomeInfected();
       }
       if(fourthInfect){
        fourthChannel.BecomeInfected();
       }

       //lane swapping
       firstChannel.RunnerSwapLane(MidiInput.GetKnob(1));
       secondChannel.RunnerSwapLane(MidiInput.GetKnob(2));
       thirdChannel.RunnerSwapLane(MidiInput.GetKnob(3));
       fourthChannel.RunnerSwapLane(MidiInput.GetKnob(4));
	}
}
