using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggBehaviour : MonoBehaviour
{
    public const float kEggSpeed = 40f;
    private const int kLifeTime = 4000; // Alife for this number of cycles
    private Rigidbody2D rb2d;
    private int mLifeCount = 0; 
    private GameController mGameController = null;
    // Start is called before the first frame update
    void Start()
    {
        mLifeCount = kLifeTime;
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
        mLifeCount--;
        if (mLifeCount <= 0)
        {
            Destroy(transform.gameObject);  // kills self
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Plane(Clone)" || collision.name == "Plane")
        {
            Destroy(transform.gameObject);
            mGameController.EnemyDestroyed();
        }
    }
     

}