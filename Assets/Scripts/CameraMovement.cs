using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController: MonoBehaviour
{
    [SerializeReference]
    GameObject spaceShip;
    Camera camera;
    Vignette vignette;
    PostProcessVolume volume;

    private void Start()
    {
        camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[10] = 10;
        camera.layerCullDistances = distances;


        // You can leave this variable out of your function, so you can reuse it throughout your class.

        // Create an instance of a vignette
        vignette = ScriptableObject.CreateInstance<Vignette>();
        vignette.enabled.Override(true);
        vignette.intensity.Override(1f);
        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);

        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (vignette.intensity.value > 0.17)
        {
            vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
        }

        camera.transform.Rotate((float)-0.2 * Input.gyro.rotationRate.x, (float)-0.2 * Input.gyro.rotationRate.z, Input.gyro.rotationRate.z);
        camera.transform.position = camera.transform.position + new Vector3((float) 0.5 * Input.acceleration.x, (float)(0.5 * (Input.acceleration.z + 0.35)), 0);
        //camera.transform.LookAt(spaceShip.transform.position);
        if (camera.transform.position.y < -14)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, -14, camera.transform.position.z);
        }
        else if (camera.transform.position.y > 15)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, 15, camera.transform.position.z);
        }

        if (camera.transform.position.x < -15)
        {
            camera.transform.position = new Vector3(-15, camera.transform.position.y, camera.transform.position.z);
        }
        else if (camera.transform.position.x > 15)
        {
            camera.transform.position = new Vector3(15, camera.transform.position.y, camera.transform.position.z);
        }
    }
}