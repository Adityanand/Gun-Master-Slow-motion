using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public Transform LesarPoint;
    public Transform Endpos;
    LineRenderer LesarPointers;
    GameObject Player;
    GameObject Enemy;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    public void Update()
    {
        if (Player.GetComponent<Player>().Jumping == true|| Player.GetComponent<Player>().LeftRun==true)
            this.GetComponent<LineRenderer>().enabled = true;
        else
            this.GetComponent<LineRenderer>().enabled = false;
        RaycastHit hit;
        LesarPointers = GetComponent<LineRenderer>();
        LesarPointers.SetPosition(0, LesarPoint.position);
        if (Physics.Raycast(LesarPoint.position, transform.up, out hit))
        {
            if (hit.collider.gameObject != (Player||Enemy))
            {
                LesarPointers.SetPosition(1, hit.point);
            }
            else
            {
                LesarPointers.SetPosition(1, Endpos.position);
            }
        }
        else
            LesarPointers.SetPosition(1, Endpos.position);
       
    }
    //void OnDrawGizmosSelected()
    //{
    //    if (LesarPoint != null)
    //    {
    //        // Draws a blue line from this transform to the target
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(LesarPoint.position,Endpos.position);
    //    }
    //}
}
