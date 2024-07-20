using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate((float) -1 * Input.gyro.rotationRate.x, (float) -1 * Input.gyro.rotationRateUnbiased.z, 0);

        transform.position = transform.position + new Vector3(0.5f * Input.acceleration.x, 0.5f * (Input.acceleration.z + 0.35f), 0);

        if (Input.acceleration.z >= -0.15)
        {
            if (transform.rotation.x > -0.2)
            {
                transform.Rotate(5.5f * (-(Input.acceleration.z + 0.35f)), 0, 0);
            }
        }
        else if (Input.acceleration.z <= -0.45)
        {
            if (transform.rotation.x < 0.3)
            {
                transform.Rotate(5f * (-(Input.acceleration.z + 0.35f)), 0, 0);
            }
        }
        else if(Input.acceleration.z > -0.5 && Input.acceleration.z < -0.14)
        {
            if (transform.rotation.x > 0.0)
            {
                transform.Rotate(-1f, 0, 0);
            }
            if (transform.rotation.x < -0.01)
            {
                transform.Rotate(1f, 0, 0);
            }
        }

         if (Input.acceleration.x >= 0.2 && transform.rotation.z > -0.2)
         {
             transform.Rotate(0, (float)0.8, (float)-2.4);
         }
         else if (Input.acceleration.x <= -0.2 && transform.rotation.z < 0.2 )
         {
             transform.Rotate(0, (float) -0.8, (float) 2.4);
         }
         else if (Input.acceleration.x > -0.2 && Input.acceleration.x < 0.2)
         {
             if (transform.rotation.z < -0.01)
                 transform.Rotate(0, (float)-0.6, (float)1.8);
             else if (transform.rotation.z > 0.01)
                 transform.Rotate(0, (float)0.6, (float)-1.8);

            if (transform.rotation.y < -0.01)
                transform.Rotate(0, (float)0.3, 0);
            else if (transform.rotation.y > 0.01)
                transform.Rotate(0, (float)-0.3, 0);
        }


        if (transform.position.y < -15)
        {
            transform.position = new Vector3(transform.position.x, -15, transform.position.z);
        }
        else if (transform.position.y > 14)
        {
            transform.position = new Vector3(transform.position.x, 14, transform.position.z);
        }

        if (transform.position.x < -15)
        {
            transform.position = new Vector3(-15, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 15)
        {
            transform.position = new Vector3(15, transform.position.y, transform.position.z);
        }
    }
}
