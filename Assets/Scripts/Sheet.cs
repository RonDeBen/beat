using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sheet : MonoBehaviour {

    [System.Serializable]
    public struct IndexedNote{
        public int beatNumber;
        public Note note;
    }

    public float beatNumberWeight = 1f;
    public float noteQuantityWeight = 1f;
    public float enemySpawnWeight = 2f;
    public int secondsToWait = 1;

    public bool fuck = true;
    public GameObject musicAnalyzer;

    public List<IndexedNote> notes;

    private int lastBeat;
    private MusicAnalyzer analyzer;

	// Use this for initialization
	void Start () {
        analyzer = musicAnalyzer.GetComponent<MusicAnalyzer>() as MusicAnalyzer;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void StartFakeMusic(){
        analyzer.StartFakeMusic();
        GenerateFakeNotes(1);
    }

    public Note NoteForBeat(int currentBeat){
        foreach(IndexedNote note in notes){
            if(note.beatNumber > currentBeat){
                return null;
            }
            if (note.beatNumber == currentBeat){
                AddFakeNoteToList();
                return note.note;
            }
        }
        return null;
    }

    private void GenerateFakeNotes(int numberOfNotes){
        for (int k = 0; k < numberOfNotes; k++){
            AddFakeNoteToList();
        }
    }

    private void AddFakeNoteToList(){
        if(fuck){
            IndexedNote newNote = new IndexedNote();
            newNote.beatNumber = GenerateBeatNumber(NextGaussianFloat());
            newNote.note = GenerateNote(NextGaussianFloat());
            notes.Add(newNote);
        }
    }

    private static float NextGaussianFloat(){
        float u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }

    private int GenerateBeatNumber(float gauss){
        if(gauss > 0){//positive
            if(gauss < beatNumberWeight){//1 standard deviation above
                lastBeat += secondsToWait * 3;
                return lastBeat;
            }
            if(gauss < beatNumberWeight * 2f){//2 standard deviations above
                lastBeat += secondsToWait * 4;
                return lastBeat;
            }//3 or more standard deviations
            lastBeat += secondsToWait * 5;
            return lastBeat;
        }
        if(gauss > -beatNumberWeight){//1 standard deviation above
            lastBeat += secondsToWait * 3;
            return lastBeat;
        }
        if(gauss > -beatNumberWeight * 2f){//2 standard deviations above
            lastBeat += secondsToWait * 2;
            return lastBeat;
        }//3 or more standard deviations
        lastBeat += secondsToWait;
        return lastBeat;
    }

    private Note GenerateNote(float gauss){
        gauss = Mathf.Abs(gauss);
        if(gauss < noteQuantityWeight){//1 standard deviation above
            return RandomNotes(1);
        }
        if(gauss < noteQuantityWeight * 2){//2 standard deviations above
            return RandomNotes(2);
        }
        if(gauss < noteQuantityWeight * 3){//3 or more standard deviations
            return RandomNotes(3);
        }
        return RandomNotes(4);
    }

    // private Note RandomNotes(int numberOfNotes){
    //     Note note = new Note();
    //     List<int> possibilities = ChannelManager.ChannelPossibilities();
    //     numberOfNotes = Mathf.Clamp(numberOfNotes, 1, possibilities.Count);
    //     for(int k = 0; k < numberOfNotes; k++){
    //         int selectedPossibility = Random.Range(0, possibilities.Count);
    //         switch(selectedPossibility){
    //             case 0:
    //                 note.square1 = true;
    //                 break;
    //             case 1:
    //                 note.square2 = true;
    //                 break;
    //             case 2:
    //                 note.triangle = true;
    //                 break;
    //             case 3:
    //                 note.noise = true;
    //                 break;
    //         }
    //         possibilities.RemoveAt(selectedPossibility);
    //     }
    //     note.enemy = ShouldAnEnemySpawn(NextGaussianFloat());
    //     return note;
    // }

    private Note RandomNotes(int numberOfNotes){
        Note note = new Note();
        numberOfNotes = Mathf.Clamp(numberOfNotes, 1, ChannelManager.NumberOfPossibilities());
        note.enemy = ShouldAnEnemySpawn(NextGaussianFloat());
        List<int> possibilities = analyzer.DeltaWavePossibilities();
        if(possibilities.Count >= numberOfNotes){
            return IncludeHurdlesFromArray(note, possibilities, numberOfNotes);
        }
        note = IncludeHurdlesFromArray(note, possibilities, possibilities.Count);
        numberOfNotes -= possibilities.Count;
        // possibilities = possibilities.Except(ChannelManager.ChannelPossibilities());
        possibilities = FuckYou(ChannelManager.ChannelPossibilities(), possibilities);
        return IncludeHurdlesFromArray(note, possibilities, numberOfNotes);

    }

    private List<int> FuckYou(List<int> firstList, List<int> secondList){
        for(int k = 0; k < secondList.Count; k++){
            if(firstList.Contains(secondList[k])){
                firstList.Remove(secondList[k]);
            }
        }
        return firstList;
    }

    private Note IncludeHurdlesFromArray(Note note, List<int> possibilities, int numberOfHurdles){
        for(int k = 0; k < numberOfHurdles; k++){
            int index = Random.Range(0, possibilities.Count);
            int selectedPossibility = possibilities[index];
            switch(selectedPossibility){
                case 0:
                    note.square1 = true;
                    break;
                case 1:
                    note.square2 = true;
                    break;
                case 2:
                    note.triangle = true;
                    break;
                case 3:
                    note.noise = true;
                    break;
            }
            possibilities.RemoveAt(index);
        }
        return note;
    }



    private bool ShouldAnEnemySpawn(float gauss){
        return Mathf.Abs(gauss) > enemySpawnWeight;
    }

    public void SetBeatNumberWeight(float weight){
        beatNumberWeight = weight;
    }

    public void SetNoteQuantityWeight(float weight){
        noteQuantityWeight = weight;
    }

    public void SetEnemySpawnWeight(float weight){
        enemySpawnWeight = weight;
    }

}
