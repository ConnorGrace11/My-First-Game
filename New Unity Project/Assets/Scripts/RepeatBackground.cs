using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.z * 1.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;

        }
    }
}
