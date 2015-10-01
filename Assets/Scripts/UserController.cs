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
          if(!firstChannel.RunnerIsJumping()){
            MusicMiddleware.playSound("Square Jump 1");
          }
          firstChannel.RunnerJump();
       }
       if (secondJump && !secondChannel.IsInfected()){
          if(!secondChannel.RunnerIsJumping()){
            MusicMiddleware.playSound("Square Jump 2");
          }
          secondChannel.RunnerJump();
       }
       if (thirdJump && !thirdChannel.IsInfected()){
          if(!thirdChannel.RunnerIsJumping()){
            MusicMiddleware.playSound("Triangle Jump");
          }
          thirdChannel.RunnerJump();
       }
       if (fourthJump && !fourthChannel.IsInfected()){
          if(!fourthChannel.RunnerIsJumping()){
            MusicMiddleware.playSound("Noise Jump");
          }
          fourthChannel.RunnerJump();
       }

       //shooting
       if (firstShoot && !firstChannel.IsInfected()){
          firstChannel.RunnerShoot();
       }
       if (secondShoot && !secondChannel.IsInfected()){
          secondChannel.RunnerShoot();
       }
       if (thirdShoot && !thirdChannel.IsInfected()){
          thirdChannel.RunnerShoot();
       }
       if (fourthShoot && !fourthChannel.IsInfected()){
          fourthChannel.RunnerShoot();
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
