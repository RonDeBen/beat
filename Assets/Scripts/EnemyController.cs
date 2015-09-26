using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private LaneController LC;
    private string channel = "first";

	// Use this for initialization
	void Start () {
	
	}

    void OnDestroy(){
        Score.EnemyPoints();
        LC.StopFlashing();
    }

    void OnBecameInvisible(){
        ChannelManager.infectChannel(channel);
    }

    public void SetLaneController(LaneController LC, string channel){
        this.LC = LC;
        this.channel = channel;
    }
}
