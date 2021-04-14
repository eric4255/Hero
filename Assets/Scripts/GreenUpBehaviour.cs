using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenUpBehaviour : MonoBehaviour
{
    public Text mEnemyCountText = null;
    public Text driver = null;
    public Text eggsOnScreen = null;
    public Text planesTouched = null;
    public float speed = 20f;
    public float mHeroRotateSpeed = 90f / 2f; // 90-degrees in 2 seconds
    public bool mFollowMousePosition = true;
    private float cooldown = 0.2f;
    private float nextFire = 0f;
    private bool keyboard = false;
    // Start is called before the first frame update

    private int mPlanesTouched = 0;
    private int mEggsOnScreen = 0;
    private GameController mGameController = null;
    private GameObject[] eggsOn;
    void Start()
    {
        mGameController = FindObjectOfType<GameController>();
        eggsOn = GameObject.FindGameObjectsWithTag ("egg");
        mEggsOnScreen = eggsOn.Length;
    }

    // Update is called once per frame

    void Update()
    {
        eggsOn = GameObject.FindGameObjectsWithTag ("egg");
        mEggsOnScreen = eggsOn.Length;
        if (Input.GetKeyDown(KeyCode.M))
        {
            keyboard = !keyboard;
            mFollowMousePosition = !mFollowMousePosition;
            if (keyboard) {
                driver.text = "Drive = Keyboard";
            }
            else 
            {
                driver.text = "Drive = Mouse";
            }
            
        }
        
        Vector3 pos = transform.position;

        if (mFollowMousePosition)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("Position is " + pos);
            pos.z = 0f;  // <-- this is VERY IMPORTANT!
            // Debug.Log("Screen Point:" + Input.mousePosition + "  World Point:" + p);
        }
        if (Input.GetKey(KeyCode.W))
        {
            pos += ((speed * Time.smoothDeltaTime) * transform.up);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos -= ((speed * Time.smoothDeltaTime) * transform.up);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            // Prefab MUST BE locaed in Resources/Prefab folder!
            Debug.Log("Spawn Eggs");
            GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject); 
            e.transform.localPosition = transform.localPosition;
            e.transform.rotation = transform.rotation;
            Debug.Log("Spawn Eggs:" + e.transform.localPosition);
            nextFire = Time.time + cooldown;           
        }
        transform.position = pos;

        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        mPlanesTouched = mPlanesTouched + 1;
        planesTouched.text = "Enemy Touched = " + mPlanesTouched;
        Destroy(collision.gameObject);
        mGameController.EnemyDestroyed();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Here x Plane: OnTriggerStay2D");
    }
}
