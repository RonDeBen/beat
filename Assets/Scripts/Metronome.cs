using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Metronome : MonoBehaviour {

    public int _beatsPerMinute;
    public int _beatsPerMeasure;

    public GameObject sheetObj, firstChannelObj, secondChannelObj, thirdChannelObj, fourthChannelObj;
    private Channel firstChannel, secondChannel, thirdChannel, fourthChannel;

    private float beat, nextBeat;
    private int currentBeat;

    private Sheet sheet;

	void Awake () {
        beat = 1f / (_beatsPerMinute / 60f);

        sheet = sheetObj.GetComponent<Sheet>();

        firstChannel = firstChannelObj.GetComponent<Channel>();
        secondChannel = secondChannelObj.GetComponent<Channel>();
        thirdChannel = thirdChannelObj.GetComponent<Channel>();
        fourthChannel = fourthChannelObj.GetComponent<Channel>();

        firstChannel.SetUpHurdles(_beatsPerMeasure, beat);
        secondChannel.SetUpHurdles(_beatsPerMeasure, beat);
        thirdChannel.SetUpHurdles(_beatsPerMeasure, beat);
        fourthChannel.SetUpHurdles(_beatsPerMeasure, beat);
	}

    void Start(){
        StartCoroutine(WaitForFirstNote());
    }
	
	void Update () {
	   if (Time.time > nextBeat){
         nextBeat = Time.time + beat;
         Tick(currentBeat);
         currentBeat++;
       }
	}

    void Tick(int beatNumber){
        ChannelTryToSpawnHurdle(sheet.NoteForBeat(beatNumber));
    }

    IEnumerator WaitForFirstNote(){
        yield return new WaitForSeconds(beat);
        StartFakeMusic();
    }

    public void StartFakeMusic(){
        sheet.StartFakeMusic();
    }

    public void ChannelTryToSpawnHurdle(Note currentNote){
        if (currentNote != null){
            if(currentNote.square1){
                firstChannel.SpawnHurdle();
            }
            if(currentNote.square2){
                secondChannel.SpawnHurdle();
            }
            if(currentNote.triangle){
                thirdChannel.SpawnHurdle();
            }
            if(currentNote.noise){
                fourthChannel.SpawnHurdle();
            }
            if(currentNote.enemy){
                SpawnRandomEnemy();
            }
        }
    }

    private void SpawnRandomEnemy(){
        List<int> possibilities = ChannelManager.ChannelPossibilities();
        int index = Random.Range(0, possibilities.Count);

        int channel = possibilities[index];
        // int position = Random.Range(1, 3);
        int position = 2;
        switch(channel){
            case 0:
                firstChannel.SpawnEnemy(position);
                break;
            case 1:
                secondChannel.SpawnEnemy(position);
                break;
            case 2:
                thirdChannel.SpawnEnemy(position);
                break;
            case 3:
                fourthChannel.SpawnEnemy(position);
                break;
        }
    }


    public float GetBeat(){
        return beat;
    }

    public void setBPM(int bpm){
        _beatsPerMinute = bpm;
    }
}
