using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header ("PlayerMovement")]
    public float MovementSpeed;
    public float TurnSpeed;
    public bool Jumping;
    public bool LeftRun;
    [Header("Gun Assest")]
    public bool BulletHit;
    public GameObject LesarPoint;
    ObjectPool Bulletspawner;
    GameObject[] Bullets;
    int i;
    Animator Anim;
    [Header("UI Elements")]
    public GameObject LevelCompleted;
    public GameObject LevelFailed;
    public GameObject RoundUI;
    private GameMaster gm;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
        RoundUI.SetActive(true);
    }
    void Start()
    {
        i = 0;
        MovementSpeed = 10f;
        TurnSpeed =3f;
        Anim = GetComponent<Animator>();
        Bulletspawner = ObjectPool.instance;
        Bullets = GameObject.FindGameObjectsWithTag("BulletUI");
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        transform.position = gm.LastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Jumping)
        {
            Time.timeScale = .2f;
            transform.Rotate(Vector3.right*TurnSpeed*Time.deltaTime);
        }
        else if(!LeftRun)
        {
            i = 0;
            foreach (var bullet in Bullets)
            {
                bullet.GetComponent<Image>().enabled = true;
            }
        }
        if (BulletHit)
        {
            Anim.SetBool("Death", true);
            StartCoroutine(LevelFailedUI());
        }
        else
        {
            transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            var Bullet = Bulletspawner.SpawnFromPool("Bullet", LesarPoint.transform.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce((LesarPoint.transform.right) * 20f);
            StartCoroutine(BulletUI());
        }
       
    }
    IEnumerator BulletUI()
    {
        Bullets[i].GetComponent<Image>().enabled = false;
        i++;
        yield return null;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //var Round= RoundUI.GetComponent<Image>().color;
        if (collision.collider.tag == "JumpIntiator")
        {
            Debug.Log("Hello");
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, 22.5f), ForceMode.Impulse);
            RoundUI.SetActive(true);// Round.a = 1;
            Jumping = true;
        }
        if (collision.collider.tag=="Ground")
        {
            Anim.SetBool("LeftRunning", false);
            Jumping = false;
            LeftRun = false;
            RoundUI.SetActive(false);//Round.a = 0;
            this.transform.rotation=Quaternion.identity;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            Time.timeScale = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            gm.LastCheckPointPos = other.gameObject.transform.position;
        }
        if (other.gameObject.tag == "LevelComplete")
        {
            LevelCompleted.SetActive(true);
            Time.timeScale = .1f;
        }
        if (other.gameObject.tag == "LeftRun")
        {
            Time.timeScale = .2f;
            LeftRun = true;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            RoundUI.SetActive(true);
            Anim.SetBool("LeftRunning", true);
        }
    }
    IEnumerator LevelFailedUI()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 1;
        LevelFailed.SetActive(true);
    }
}
