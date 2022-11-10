using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acoustic : GoodItem
{
    // Start is called before the first frame update
    void Start()
    {
        SetScale();
    }
    public override void Move()
    {
        speed = 5.0f;
        base.Move();
    }
    public override void SetScale()
    {
        scaleChange = new Vector3(1.5f, 1.5f, 1.5f);
        //base.SetScale();

    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }


}