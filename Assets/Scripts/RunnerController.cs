using UnityEngine;
using System.Collections;

public class RunnerController : MonoBehaviour {

    public float highestPoint;
    public float bulletSpeed;
    public float secondsIndicatorLasts;
    private float grav, velocity, floor, timeToRemoveIndicator, lastArrowKnob;
    public GameObject metronomeObj, bullet, radialIndicator, arrow;
    private Metronome metronome;
    private MusicMiddleware MM;

    private bool jumping = false;
    private float jumpStartTime, jumpNought, beat;

    void Start(){
        floor = gameObject.transform.position.y;

        metronome = metronomeObj.GetComponent<Metronome>();
        MM = gameObject.GetComponent<MusicMiddleware>();
        beat = metronome.GetBeat();

        velocity = (4f*highestPoint)/beat;
        grav = (-velocity) / beat;
    }

    void Update(){
        if(Time.time > timeToRemoveIndicator){
            indicatorShouldBeVisible(false);
        }
    }

	public void Jump(){
        if (!jumping){
            jumping = true;
            jumpStartTime = Time.time;
        }
    }

    public void Shoot(){
        Vector3 bulletPos = gameObject.transform.position;
        float theta = 40;

        float y = Mathf.Sin(theta);
        float z = -Mathf.Cos(theta);

        GameObject go = Instantiate(bullet, bulletPos, bullet.transform.rotation) as GameObject;
        go.GetComponent<Rigidbody>().velocity = new Vector3(0, y, z) * -bulletSpeed;
    }

    public void SetRunnerPos(Vector3 newX){
        gameObject.transform.position = newX;
    }

    public void SwapLane(float newX){
        Vector3 newPosition = gameObject.transform.position;
        newPosition.x = newX;
        gameObject.transform.position = newPosition;
    }

    public void SetArrowPosition(float knob){
        if(Mathf.Abs(knob - lastArrowKnob) > 0.001){
            indicatorShouldBeVisible(true);
            lastArrowKnob = knob;
            timeToRemoveIndicator = Time.time + secondsIndicatorLasts;
            arrow.transform.eulerAngles = new Vector3(0f, 0f, -knob * 270 + 45);
        }
    }

    private void indicatorShouldBeVisible(bool truth){
        radialIndicator.GetComponent<SpriteRenderer>().enabled = truth;
        arrow.GetComponent<SpriteRenderer>().enabled = truth;
    }

    void FixedUpdate(){
        if (jumping){
            float airtime = Time.time - jumpStartTime;
            float jumpPos = floor + (velocity * airtime) + (grav * Mathf.Pow(airtime, 2));
            if(jumpPos <= floor){
                jumpPos = floor;
                jumping = false;
            }
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, jumpPos, gameObject.transform.position.z);
        }
    }

    void OnCollisionEnter(){
        MM.playSound("Hurdle Collision");
    }
}
