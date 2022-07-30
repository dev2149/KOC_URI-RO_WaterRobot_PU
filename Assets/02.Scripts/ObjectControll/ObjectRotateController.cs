using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectRotateController : MonoBehaviour
{

    Vector3 _FirstPoint;
    Vector3 _SencdPoint;
    public float xAngle;
    public float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    // Rock
    ManualController _manualController;
    private void OnEnable()
    {
        _manualController = GameObject.Find("MaualARLogic").GetComponent<ManualController>();
        xAngle = this.transform.rotation.x;
        yAngle = this.transform.rotation.y;
    }
    void Update()
    {
        if (Input.touchCount > 0 && !_manualController._Lock)

        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !_manualController._Lock)
            {
                _FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && !_manualController._Lock)
            {
                _SencdPoint = Input.GetTouch(0).position;
                xAngle = xAngleTemp + (_SencdPoint.x - _FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp + (_SencdPoint.y - _FirstPoint.y) * 90 / Screen.height; 
                this.transform.localRotation = Quaternion.Euler(yAngle, -xAngle, this.transform.rotation.z);
                //this.transform.localRotation = Quaternion.Euler(yAngle, this.transform.rotation.y, -xAngle);
            }
        }
    }
}