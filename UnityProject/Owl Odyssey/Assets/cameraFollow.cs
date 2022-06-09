using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;

    float x = 0; float y = 1; float z = -5;

    Vector3 camPos;

    private void Start()
    {
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;
        camPos = new Vector3(x, y, z);
    }

    void Update()
    {
        Vector3 pPos = new Vector3(player.position.x - 1, 0, player.position.z + 1);
        transform.position = pPos + camPos;
    }
}
