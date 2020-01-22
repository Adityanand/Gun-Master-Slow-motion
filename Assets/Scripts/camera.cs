using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float speed;
    public Vector3 offset;
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        offset = new Vector3(75, 0f, 29.7f);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 DesiredPosition = Player.transform.position + offset;
        Vector3 SmoothPsoition = Vector3.Lerp(transform.position, DesiredPosition, speed);
        transform.position = SmoothPsoition;
    }
}
