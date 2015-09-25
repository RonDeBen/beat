using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	private Color startColor, endColor;
    private float cycleTime, cycleStartTime;
    private bool isFlashing = false;
    private Material laneMat;

    void Start(){
        laneMat = gameObject.GetComponent<Renderer>().material;
    }

    void FixedUpdate(){
        if (isFlashing){
            float oscillation = ((Time.time - cycleStartTime) % cycleTime) / cycleTime;
            float wave = Mathf.Sin(Mathf.PI*oscillation);
            laneMat.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, wave));
        }
    }

    public void SetUpLane(Color startColor, Color endColor, float cycleTime){
        this.startColor = startColor;
        this.endColor = endColor;
        this.cycleTime = cycleTime;
    }

    public void StartFlashing(){
        isFlashing = true;
        cycleStartTime = Time.time;
    }

    public void StopFlashing(){
        isFlashing = false;
        laneMat.SetColor("_EmissionColor", startColor);
    }

}
