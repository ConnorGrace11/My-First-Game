using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodItem : MonoBehaviour
{
    private float bSpeed = 50.0f;
    public float speed
    {
        get 
        {
            return bSpeed;
        } 
        set 
        {
            if (value < 0.0f)
            {
                Debug.LogError("You can't set speed to a negative value");
            }
            else
            {
                bSpeed = value;
            }
        } 
    
    }
    protected Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Move()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }

    public virtual void SetScale()
    {
        scaleChange = new Vector3(1.0f, 1.0f, 1.0f);
        transform.localScale = scaleChange;
    }
}
