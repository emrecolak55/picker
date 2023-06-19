using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Space(10)]
    public float angle;
    public bool movable;
    public int id;

    public float deltaX = .5f;  // Amount to move left and right from the start point
    public float speed = 2.0f;  
    public float startX;

    float tempTime;

    void Start()
    {
        tempTime = 0;
    }

    void Update()
    {
        //if obstacle is set to movable then move it over the time
        if (movable)
        {
            tempTime += Time.deltaTime;
            Vector3 v = new Vector3(startX,transform.position.y,transform.position.z);
            v.x += deltaX * Mathf.Sin(tempTime * speed);
            transform.position = v;
        }
    }
}