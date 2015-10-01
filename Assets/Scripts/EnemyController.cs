using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    private LaneController LC;
    private string channel = "first";
    private bool wasDestroyed = false;

	// Use this for initialization
	void Start () {
	   SpriteRenderer sprender = gameObject.GetComponent<SpriteRenderer>();
       Vector3 newPosition = gameObject.transform.position;
       newPosition.y += sprender.bounds.size.y / 2;
       gameObject.transform.position = newPosition;
	}

    void OnBecameInvisible(){
        if(!wasDestroyed){
            Score.ResetCombo();
            ChannelManager.infectChannel(channel);
        }
    }

    void OnTriggerEnter(Collider other){
        wasDestroyed = true;
        Score.EnemyPoints();
        LC.StopFlashing();

        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    public void SetLaneController(LaneController LC, string channel){
        this.LC = LC;
        this.channel = channel;
    }
}
