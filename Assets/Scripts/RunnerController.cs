using UnityEngine;
using System.Collections;

public class RunnerController : MonoBehaviour {

    public float highestPoint;
    public float bulletSpeed;
    private float grav, velocity, floor;
    public GameObject metronomeObj, bullet;
    private Metronome metronome;

    private bool jumping = false;
    private float jumpStartTime, jumpNought, beat;

    void Start(){
        floor = gameObject.transform.position.y;

        metronome = metronomeObj.GetComponent<Metronome>();
        beat = metronome.GetBeat();

        velocity = (4f*highestPoint)/beat;
        grav = (-velocity) / beat;
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
}
