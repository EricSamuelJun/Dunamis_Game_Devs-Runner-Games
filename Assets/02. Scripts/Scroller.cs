using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public int count;
    public float speedRate;
    

    // Start is called before the first frame update
    void Start()
    {
        count = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isLive){
        float totalSpeed =GameManager.globalSpeed * speedRate * Time.deltaTime * -1f;
        transform.Translate(totalSpeed, 0, 0);
        }
    }
}
