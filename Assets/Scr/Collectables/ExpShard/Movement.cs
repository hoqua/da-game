using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Update()
    {
        var step = 5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(2, 0, 20), step);
    }
}
