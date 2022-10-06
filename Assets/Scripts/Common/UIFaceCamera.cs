using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    void Update()
    {
       // this.transform.rotation = Camera.main.transform.rotation;

        var rotation = Camera.main.transform.rotation;
        this.transform.rotation = new Quaternion(transform.rotation.x,rotation.y, transform.rotation.z, transform.rotation.w);
    }
}
