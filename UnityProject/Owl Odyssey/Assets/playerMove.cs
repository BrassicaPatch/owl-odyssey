using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public Camera camera;
    public GameObject player;

    [Space(10)]
    public float playerSpeed = 10f;
    public float playerRotation = 100f;
    public float smooth = 5.0f;

    void Update()
    {
        movePlayer();
        rotatePlayer();
    }

    void movePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            player.transform.position += direction * playerSpeed * Time.deltaTime;
        }
    }

    void rotatePlayer()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = player.transform.position + Vector3.forward * 2;

            float lookAngle = Mathf.Atan2(hit.point.x - newPosition.x, hit.point.z - newPosition.z) * Mathf.Rad2Deg;

            Quaternion target = Quaternion.Euler(0, lookAngle, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        }
    }
}
