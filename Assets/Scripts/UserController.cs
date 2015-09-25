using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    public GameObject firstChannelObj;
    public GameObject secondChannelObj;
    public GameObject thirdChannelObj;
    public GameObject fourthChannelObj;

    public GameObject sheetObj;
    public GameObject MetronomeObj;

    private Channel firstChannel, secondChannel, thirdChannel, fourthChannel;
    private Sheet sheet;
    private Metronome metronome;
	
    void Start(){
        firstChannel = firstChannelObj.GetComponent<Channel>();
        secondChannel = secondChannelObj.GetComponent<Channel>();
        thirdChannel = thirdChannelObj.GetComponent<Channel>();
        fourthChannel = fourthChannelObj.GetComponent<Channel>();

        sheet = sheetObj.GetComponent<Sheet>();
        metronome = MetronomeObj.GetComponent<Metronome>();
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
       }
       if (secondJump){
        secondChannel.RunnerJump();
       }
       if (thirdJump){
        thirdChannel.RunnerJump();
       }
       if (fourthJump){
        fourthChannel.RunnerJump();
       }

       //shooting
       if (firstShoot){
        firstChannel.RunnerShoot();
       }
       if (secondShoot){
        secondChannel.RunnerShoot();
       }
       if (thirdShoot){
        thirdChannel.RunnerShoot();
       }
       if (fourthShoot){
        fourthChannel.RunnerShoot();
       }

       //lane swapping
       firstChannel.RunnerSwapLane(MidiInput.GetKnob(1));
       secondChannel.RunnerSwapLane(MidiInput.GetKnob(2));
       thirdChannel.RunnerSwapLane(MidiInput.GetKnob(3));
       fourthChannel.RunnerSwapLane(MidiInput.GetKnob(4));

       // sheet.SetBeatNumberWeight((MidiInput.GetKnob(5) - 0.5f ) * 6);
       // sheet.SetNoteQuantityWeight((MidiInput.GetKnob(6) - 0.5f) * 6);
       // sheet.SetEnemySpawnWeight((MidiInput.GetKnob(7) - 0.5f) * 6);
       metronome.setBPM((int)MidiInput.GetKnob(8)*180);
	}
}
