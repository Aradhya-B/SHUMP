using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S; // Singleton 

    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    // This variable holds a reference to the last triggering GameObject
    private GameObject _lastTriggerGo = null;

    private void Awake()
    {
        if (S == null)
        {
            S = this; // Set the Singleton
        } else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // Rotate the ship to make it feel more dynamic 
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        // Make sure it's not the same triggering go as last time
        if (go == _lastTriggerGo)
        {
            return;
        }
        _lastTriggerGo = go;
        
        if (go.tag == "Enemy")
        {
            Destroy(go);
            Destroy(this.gameObject);
            Main.S.DelayedRestart(gameRestartDelay);
        } else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }
}
