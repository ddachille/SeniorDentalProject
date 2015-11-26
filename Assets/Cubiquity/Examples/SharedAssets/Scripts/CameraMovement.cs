﻿using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera-Control/Keyboard Orbit")]
 
public class CameraMovement : MonoBehaviour {
    public Transform target;
    //public float distance = 20.0f;
    //public float zoomSpd = 2.0f;
 
    public float xSpeed = 30.0f;
    public float ySpeed = 20.0f;
 
    public int yMinLimit = -723;
    public int yMaxLimit = 877;
 
    private float x = 0.0f;
    private float y = 0.0f;
 
    public void Start () {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
 
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }
 
    public void LateUpdate () {
        if (target) {
            x -= Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            y += Input.GetAxis("Vertical") * ySpeed * 0.02f;
 
            y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			Debug.Log ("X: "+x+" "+"Y: "+y);
 
	    //distance -= Input.GetAxis("Fire1") *zoomSpd* 0.02f;
        //    distance += Input.GetAxis("Fire2") *zoomSpd* 0.02f;
			
			if(y>=40){
				y=40;
			}
			else if(y<=-30){
				y=-30;
			}
			
			if(x>=48){
				x=48;
			}
			else if(x<=-48){
				x=-48;
			}
 
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            //Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
 
            transform.rotation = rotation;
            //transform.position = position;
        }
    }
 
    public static float ClampAngle (float angle, float min, float max) {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp (angle, min, max);
    }
}