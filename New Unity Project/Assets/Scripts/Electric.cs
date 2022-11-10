using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : GoodItem
{
    // Start is called before the first frame update
    void Start()
    {
        SetScale();
    }
    public override void Move()
    {
        speed = 3.0f;
        base.Move();
    }

    public override void SetScale()
    {
        scaleChange = new Vector3(3.0f, 3.0f, 3.0f);
        //base.SetScale();

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        
    }


}
