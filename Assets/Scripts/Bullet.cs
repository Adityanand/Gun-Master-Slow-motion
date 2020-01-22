using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    ObjectPool obj;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(BulletKill());
    }
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Pool").GetComponent<ObjectPool>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            obj.poolDictionary["Bullet"].Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
            collision.collider.gameObject.GetComponentInParent<Enemy>().BulletHit = true;
        }
        if(collision.collider.tag=="Player")
        {
            obj.poolDictionary["Bullet"].Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
            collision.collider.gameObject.GetComponentInParent<Player>().BulletHit = true;
        }
    }
    IEnumerator BulletKill()
    {
        yield return new WaitForSeconds(2);
        obj.poolDictionary["Bullet"].Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
