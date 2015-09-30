using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	private Color startColor, endColor;
    private float cycleTime, cycleStartTime;
    private bool isFlashing = false;
    private Material laneMat;

    private bool infected;

    void Start(){
        laneMat = gameObject.GetComponent<Renderer>().material;
    }

    void FixedUpdate(){
        if (isFlashing && !infected){
            float oscillation = (Time.time - cycleStartTime) / cycleTime;
            float wave = Mathf.Abs(Mathf.Sin(Mathf.PI*oscillation));
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
        if(!infected){
            isFlashing = false;
            laneMat.SetColor("_EmissionColor", startColor);
        }
    }

    public void SetInfected(bool isInfected){
        infected = isInfected;
        isFlashing = false;
    }

}
