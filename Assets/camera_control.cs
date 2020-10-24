using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_control : MonoBehaviour
{
    public position_control pc;
    public Camera thisCam;
    private bool sizeUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x_max = Mathf.Max(pc.x_pos.ToArray());
        float y_max = Mathf.Max(pc.y_pos.ToArray());
        float x_min = Mathf.Min(pc.x_pos.ToArray());
        float y_min = Mathf.Min(pc.y_pos.ToArray());
        float x_pos = (x_max + x_min) / 2;
        float y_pos = (y_max + y_min) / 2;
        Vector3 topRight = new Vector3(x_max, y_max, 0);
        Vector3 bottomLeft = new Vector3(x_min, y_min, 0);
        transform.position = new Vector3(x_pos, y_pos, -10);
        Vector3 viewPos1 = thisCam.WorldToViewportPoint(topRight);
        Vector3 viewPos2 = thisCam.WorldToViewportPoint(bottomLeft);
        if (viewPos1.x > 0.95f || viewPos1.y > 0.9f || viewPos2.x < 0.05f || viewPos2.y < 0.05f)
        {
            sizeUp = true;
        }
        else
        {
            sizeUp = false;
        }
        if (sizeUp)
        {
            thisCam.orthographicSize += 2;
        }
    }
}
