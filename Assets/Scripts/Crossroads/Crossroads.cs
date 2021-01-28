using RogoDigital.Lipsync;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossroads : MonoBehaviour
{
    private enum CrossroadProgress { Start, BeforeLooking, Looking, AfterLooking, UnlockTeleport, FinishExercise, WrapUp, Done};
    private enum CrossroadTarget { Left, Right};

    public LayerMask layerMaskToIgnore;

    public LipSyncData[] audioFiles;
    public GameObject[] lookZones;
    public GameObject nextHenk;
    public bool finishExercise = false;

    private LipSync character;
    private Animator animator;

    private CrossroadProgress progress = CrossroadProgress.Start;

    private int lookCounter = 0;
    private CrossroadTarget crossroadTarget = CrossroadTarget.Left;
    private RaycastHit hitInfo;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInChildren<LipSync>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " " + other.name);
        if (other.tag == "Player" && progress == CrossroadProgress.Start)
        {
            progress = CrossroadProgress.BeforeLooking;
            StartCoroutine(UpdateProgress(audioFiles[0], CrossroadProgress.Looking));
        }
    }

    private IEnumerator UpdateProgress(LipSyncData clip, CrossroadProgress crossroadProgress, float delay = 0, string animationTrigger = "None")
    {
        Debug.Log("Previous progress: " + progress);
        yield return new WaitForSeconds(delay);

        if (animationTrigger != "None")
            animator.SetTrigger(animationTrigger);

        character.Play(clip, 0);
        character.audioSource.clip = clip.clip;
        character.audioSource.Play();

        yield return new WaitForSeconds(clip.length);
        character.Stop(true);
        progress = crossroadProgress;

        Debug.Log("Current progress: " + crossroadProgress);

    }

    // Update is called once per frame
    void Update()
    {
        if (progress == CrossroadProgress.Looking)
        {
            Transform cameraTransform = Camera.main.transform;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo, 100.0f, ~layerMaskToIgnore))
            {
                if (crossroadTarget == CrossroadTarget.Left && lookCounter == 0 && hitInfo.transform == lookZones[0].transform)
                {
                    Debug.Log("Look Left 1");
                    lookCounter++;

                    crossroadTarget = CrossroadTarget.Right;
                }

                if (crossroadTarget == CrossroadTarget.Right && lookCounter == 1 && hitInfo.transform == lookZones[1].transform)
                {
                    Debug.Log("Look Right 1");
                    lookCounter++;
                    crossroadTarget = CrossroadTarget.Left;
                }

                if (crossroadTarget == CrossroadTarget.Left && lookCounter == 2 && hitInfo.transform == lookZones[0].transform)
                {
                    Debug.Log("Look Left 2");
                    lookCounter++;
                    crossroadTarget = CrossroadTarget.Right;
                }

                if (lookCounter == 3)
                {
                    progress = CrossroadProgress.AfterLooking;
                    StartCoroutine(UpdateProgress(audioFiles[2], CrossroadProgress.UnlockTeleport, 0, "isBowing"));
                }


                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
            }
        } else if (progress == CrossroadProgress.UnlockTeleport)
        {
            progress = CrossroadProgress.FinishExercise;

            if (finishExercise)
            {

                StartCoroutine(UpdateProgress(audioFiles[3], CrossroadProgress.WrapUp));
            }else
            {
                StartCoroutine(UpdateProgress(audioFiles[1], CrossroadProgress.Done));
            }
        } else if (progress == CrossroadProgress.WrapUp)
        {
            progress = CrossroadProgress.Done;
            this.gameObject.SetActive(false);

        } else if (progress == CrossroadProgress.Done && !finishExercise)
        {
            UnlockNextArea();
        }
    }

    private void UnlockNextArea()
    {
        nextHenk.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
