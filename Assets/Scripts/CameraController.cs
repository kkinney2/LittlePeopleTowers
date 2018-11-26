using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float angleModX = 20;
    public float angleModY = 20;

    private Vector3 offset;
    private float xPos;
    private float yPos;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
        transform.RotateAround(point: player.transform.position, axis: Vector3.up, angle: angleModX * Input.GetAxis("Mouse X"));
        transform.RotateAround(point: player.transform.position, axis: transform.TransformVector(Vector3.right), angle: angleModY * Input.GetAxis("Mouse Y"));
    }

    void LateUpdate()
    {
        offset = transform.position - player.transform.position;
        //transform.LookAt(player.transform.position);
    }
}
