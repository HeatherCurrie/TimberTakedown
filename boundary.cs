using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour
{
    // REFERENCE: https://www.youtube.com/watch?v=ailbszpt_AI 
    // Code was taken from the above video, tweeked very slightly for my own needs

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start()
    {
        //set variables
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
    }

    // Update is called once per frame, after all functions have been called
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}