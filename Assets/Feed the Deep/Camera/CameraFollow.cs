using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 offsetPos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 playerPos = player.transform.position;
        offsetPos = transform.position - player.transform.position;
        offsetPos.y = 0f;
    }

    // Update is called once per frame
     void Update()
    {
        Vector3 target = player.transform.position;

        target.x = target.x + offsetPos.x;
        target.y = transform.position.y;
        target.z = target.z + offsetPos.z;

        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10f);
    }
}
