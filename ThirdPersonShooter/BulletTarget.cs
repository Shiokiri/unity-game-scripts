using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTarget : MonoBehaviour
{
    private int healthPoint;
    // Start is called before the first frame update
    void Start()
    {
        healthPoint = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(healthPoint);
        if (healthPoint == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void reduceHealthPoint(int reduceNumber)
    {
        this.healthPoint -= reduceNumber;
    }
}
