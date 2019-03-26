using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapCooldown : MonoBehaviour
{
    public float startSize;
    private float currentSize;
    public float endSize;
    public float shrinkSpeed;

    

    // Fade the color from red to green
    // back and forth over the defined duration

    public Color colorStart = Color.red;
    public Color colorEnd = Color.blue;
    private Color currentColor;
    
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer> ();
        transform.localScale = new Vector3(startSize, startSize, startSize);
        currentSize = startSize;
        //currentSize = startSize;
        //gameObject.transform.scale.x = startSize;
        //gameObject.transform.scale.z = startSize;

    }

    // Update is called once per frame
    void Update()
    {
        // Shrink Size
        if (currentSize > endSize)
        {
            float shrinkAmount = shrinkSpeed * Time.deltaTime;
            
            currentSize -= shrinkAmount;
            transform.localScale = new Vector3(currentSize, currentSize, currentSize); 

            // Fade color
            float sizingProgress = (currentSize-startSize)/(endSize-startSize);
            rend.material.color = Color.Lerp(colorStart, colorEnd, sizingProgress);
            
            //GetComponent<Renderer>().material.color.b = shrinkSpeed;   
        }

        // if (fadeTimePassed < 1.0f) {
        // }

        

        //float lerp = Mathf.PingPong(Time.time, duration) / duration;
        //rend.material.color = Color.Lerp(colorStart, colorEnd, lerp);
        
    }
}
