using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text planesOnScreen = null;
    private int maxPlanes = 20;
    private float health = 1.0f;
    private Renderer ren;
    private int numberOfPlanes = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        planesOnScreen.text = "Enemy On Screen = " + numberOfPlanes;
        if (Input.GetKey(KeyCode.Escape)) {
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (numberOfPlanes < maxPlanes)
        {
            CameraSupport s = Camera.main.GetComponent<CameraSupport>();

            GameObject e = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            Vector3 pos;
            pos.x = s.GetWorldBound().min.x + Random.value * s.GetWorldBound().size.x;
            pos.y = s.GetWorldBound().min.y + Random.value * s.GetWorldBound().size.y;
            pos.z = 0;
            e.transform.localPosition = pos;
            ++numberOfPlanes;
        }

        
    }

    public void EnemyDestroyed()
    {
        --numberOfPlanes;
    }
    void OnCollisionEnter2D(Collision2D collision) {
    	if (collision.gameObject.name == "GreenUp") {
    		health = 0f;
    	}
    }

    void OnTriggerEnter2D(Collider2D collision) {
    	if (collision.gameObject.name == "Egg(Clone)") {
    		health -= 0.25f;
    		Color old = ren.material.color;
    		Color newc = new Color(old.r, old.g, old.b, old.a*0.8f);
    		ren.material.color = newc;
    	}
    }
}