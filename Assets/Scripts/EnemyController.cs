using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private LaneController LC;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy(){
        LC.StopFlashing();
    }

    public void SetLaneController(LaneController LC){
        this.LC = LC;
    }
}
