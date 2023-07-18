using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platillos : MonoBehaviour
{
    float smooth = 5.0f;    
    private float newvalue=0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float z;
    // Update is called once per frame
    void Update()
    {
        
        newvalue++;
        /*Quaternion target = Quaternion.Euler(0, 0, newvalue);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        */

        /*var angles = transform.rotation.eulerAngles;
        print("Angles:"+angles);
        angles.z += Time.deltaTime * 10;
        print("Angles.z:" + angles.z);
        //if (angles.z >= 90) angles.z = 0;
        transform.rotation = Quaternion.Euler(angles);*/

        var angles = transform.rotation.eulerAngles;
        //print("Angles:" + angles);
        angles.z += Time.deltaTime * 10;
        //print("Angles.z:" + angles.z);
        //if (angles.z >= 90) angles.z = 0;
        transform.rotation = Quaternion.Euler(angles);



        //z += Time.deltaTime * 10;
        //transform.rotation = Quaternion.Euler(0, 0, z);

    }
}
