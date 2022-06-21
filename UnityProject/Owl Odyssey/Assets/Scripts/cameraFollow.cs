using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;

    float x = 0; float y = 1; float z = -5;

    Vector3 camPos;
    Vector3 posDif;

    private void Start()
    {
        x = this.gameObject.transform.position.x;
        y = this.gameObject.transform.position.y;
        z = this.gameObject.transform.position.z;
        camPos = new Vector3(x, y, z);
        posDif = new Vector3(x - player.position.x, y - player.position.y, z - player.position.z);
    }

    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x + posDif.x, player.position.y + posDif.y, player.position.z + posDif.z);
        transform.position = newPos;
    }
}
