using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WristUIScroll : MonoBehaviour
{
    public GameObject rightHand;
    public Transform ScrollContext;

    private float yOnDragStart;
    private Vector2 scrollPosOnDragStart;
    private bool dragging = false;

    private Transform indexTransform;
    // Start is called before the first frame update
    void Start()
    {
        FingerTap[] children = rightHand.GetComponentsInChildren<FingerTap>();
        indexTransform = children[0].transform;
    }

    private void Update()
    {
        if (dragging)
        {
            //ScrollContext.localPosition = new Vector2(0, 20);
            ScrollContext.localPosition = scrollPosOnDragStart + new Vector2(0, ScrollContext.InverseTransformPoint(indexTransform.position).y - yOnDragStart);
        }
        if (release)
        {
            releaseTimer += Time.deltaTime;
            if(releaseTimer > 0.05f)
            {
                releaseTimer = 0;
                dragging = false;
                release = false;
            }
        }
    }

    private float releaseTimer = 0;
    private bool release = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!dragging)
        {
            yOnDragStart = ScrollContext.InverseTransformPoint(indexTransform.position).y;
            scrollPosOnDragStart = ScrollContext.localPosition;
            dragging = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        release = true;
    }
}
