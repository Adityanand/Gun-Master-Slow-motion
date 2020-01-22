using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isdead;
    public bool BulletHit;
    Animator Anim;
    ObjectPool obj;
    LaserPointer pointer;
    public Transform BulletSpawnPosition;
    public float time;
    public float MinDistance;

    // Start is called before the first frame update
    void Start()
    {
        time = .4f;
        Anim = GetComponent<Animator>();
        obj = GameObject.FindGameObjectWithTag("Pool").GetComponent<ObjectPool>();
        pointer = GameObject.FindGameObjectWithTag("LesarPointer").GetComponent<LaserPointer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(BulletSpawnPosition.position,transform.forward,out hit,MinDistance)&&!isdead)
        {
            if(hit.collider.gameObject.tag=="Player")
            {
                time = time - 1 * Time.deltaTime;
                if (time <= 0)
                {
                    shoot();
                    time = 1;
                }
            }
        }
        if (BulletHit)
        {
            isdead = true;
            Anim.SetBool("Death", true);
            //if (this.GetComponentInChildren<MeshCollider>() != null)
            //    this.GetComponentInChildren<MeshCollider>().enabled = false;
                this.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void shoot()
    {
        Debug.Log("Shoot");
        var shoot = obj.SpawnFromPool("Bullet", BulletSpawnPosition.transform.position, Quaternion.identity);
        shoot.GetComponent<Rigidbody>().AddForce((BulletSpawnPosition.transform.right) * 20f);
        
    }
}
