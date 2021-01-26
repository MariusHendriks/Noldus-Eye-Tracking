using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTalking : MonoBehaviour
{
    private Transform cameraTransform;
    private RaycastHit hitInfo;
    private SpeechRecognition speech;

    private GameObject npcLookingAt;

    private float timeSpentLooking;
    private float timeToLook = 1.5f;
    private bool triggeredNPC = false;




    // Start is called before the first frame update
    void Start()
    {
        speech = GetComponent<SpeechRecognition>();
        speech.enabled = false;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        NPCHitLogic();
    }

    private void NPCHitLogic()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo, 10f) && hitInfo.collider.gameObject.layer == 19)
        {
            if (npcLookingAt != hitInfo.collider.gameObject)
            {
                timeSpentLooking = 0;
                triggeredNPC = false;
            }

            npcLookingAt = hitInfo.collider.gameObject;
            timeSpentLooking += Time.deltaTime;

            if (timeSpentLooking >= timeToLook && !triggeredNPC)
            {

                triggeredNPC = true;
                speech.enabled = true;
            }


            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 10f, Color.yellow);
        }
        else
        {
            npcLookingAt = null;
            if (speech.enabled)
                speech.enabled = false;
        }
    }
}
