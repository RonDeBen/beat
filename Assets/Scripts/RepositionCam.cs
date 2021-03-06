﻿using UnityEngine;
using System.Collections;

public class RepositionCam : MonoBehaviour {

	void Start () {

        float height = 2f * GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;

        Vector3 newPosition = new Vector3(transform.position.x - width / 2, transform.position.y - height / 2, 0);
        
        float offsetX = transform.position.x + (0f - newPosition.x);
        float offsetY = transform.position.y + (0f - newPosition.y);
        transform.position = new Vector3(offsetX, offsetY, -10);
	}
	
}
