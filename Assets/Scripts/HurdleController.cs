using UnityEngine;
using System.Collections;

public class HurdleController : MonoBehaviour {

    private Vector3 runnerPos;
    private float depth; 
    private Color jumpedOverColor;

    private Color startColor, endColor;
    private float cycleTime, cycleStartTime;
    private bool isFlashing = false;
    private Material hurdleMat;


	// Use this for initialization
	void Start () {
        float hypotenuse = gameObject.GetComponent<BoxCollider>().size.y / 2f;
        depth = Mathf.Cos(gameObject.transform.eulerAngles.x) * hypotenuse;
        hurdleMat = gameObject.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	   if (hasBeenJumpedOver()){
            if(!isFlashing){
                Score.HurdlePoints();
                isFlashing = true;
            }
       }
       if (isFlashing){
            float oscillation = ((Time.time - cycleStartTime) % cycleTime) / cycleTime;
            float wave = Mathf.Sin(Mathf.PI*oscillation);
            hurdleMat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, wave));
       }
	}

    private bool hasBeenJumpedOver(){
        return (gameObject.transform.position.y - depth > runnerPos.y);
    }

    public void SetupHurdle(Vector3 runnerPos, Color jumpedOverColor, Color startColor, Color endColor, float cycleTime){
        this.runnerPos = runnerPos;
        this.jumpedOverColor = jumpedOverColor;
        this.startColor = startColor;
        this.endColor = endColor;
        this.cycleTime = cycleTime;
    }

    void OnCollisionEnter(){
        Score.ResetCombo();
        gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", jumpedOverColor); 
        Rigidbody body = gameObject.GetComponent<Rigidbody>() as Rigidbody;
        body.useGravity = true;
        body.velocity = Vector3.zero;
    }

    public void StartFlashing(){
        isFlashing = true;
        cycleStartTime = Time.time;
    }

    void OnBecomeInvisible(){
        Destroy(gameObject);
    }
}
