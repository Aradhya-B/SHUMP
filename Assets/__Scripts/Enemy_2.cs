using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    // Private variable to store the direction of rotation that is randomly generated on start 
    private int _rotation;
    public void Start()
    {
        // Generate a random rotation: either 45 degrees (enemy will move right) or negative 45 degrees (enemy will move left)
        _rotation = Random.Range(0.0f, 1.0f) > 0.5f ? 45 : -45;
        // Apply the rotation to the enemy on start so it is facing the direction it will be moving
        transform.rotation = Quaternion.Euler(0, 0, _rotation);
    }

    public override void Move()
    {
        // Getting the position of the enemy
        Vector3 tempPos = pos;
        // No matter the direction, the object is always moving down
        tempPos.y -= speed * Time.deltaTime;
        // If the object is supposed to move right, add to the x coordinate
        if (_rotation == 45)
        {
            tempPos.x += speed * Time.deltaTime;
        } else
        // If the object is supposed to move left, subtract from the x coordinate 
        {
            tempPos.x -= speed * Time.deltaTime;
        }
        // Apply the position change 
        pos = tempPos;
    }
}
