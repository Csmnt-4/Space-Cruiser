using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using TMPro;

public class SimpleTextController : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        /*textMesh.text = "Accel X: " + Input.acceleration.x +
            "\nAccel Y: " + Input.acceleration.y +
            "\nAccel Z: " + Input.acceleration.z;
*/




    }
}
