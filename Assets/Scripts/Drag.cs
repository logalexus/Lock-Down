using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {

        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }
}
