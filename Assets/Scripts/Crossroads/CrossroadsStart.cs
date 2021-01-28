using RogoDigital.Lipsync;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadsStart : MonoBehaviour
{
    private enum CrossroadProgress { Start, Speak, Teleport, SpeakAgain, ByeBye, ThankYou, Done};
    private enum CrossroadTarget { Left, Right};


    public LipSyncData[] audioFiles;
    public GameObject NPC;
    public GameObject teleportPoint;
    public GameObject firstCrossing;

    private LipSync character;
    private Animator animator;

    private CrossroadProgress progress = CrossroadProgress.Start;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInChildren<LipSync>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);


        if (other.tag == "Player" && progress == CrossroadProgress.Start)
        {
            progress = CrossroadProgress.Speak;

            playerTransform = other.transform;
            StartCoroutine(UpdateProgress(audioFiles[0], CrossroadProgress.Teleport));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (progress == CrossroadProgress.Teleport)
        {
            playerTransform.position = teleportPoint.transform.position;
            NPC.transform.position = new Vector3(teleportPoint.transform.position.x + 1f, teleportPoint.transform.position.y, teleportPoint.transform.position.z + 1f);
            NPC.transform.Rotate(new Vector3(0, 30, 0));

            progress = CrossroadProgress.SpeakAgain;
            StartCoroutine(UpdateProgress(audioFiles[1], CrossroadProgress.ByeBye));
        } else if (progress == CrossroadProgress.ByeBye)
        {
            progress = CrossroadProgress.ThankYou;
            StartCoroutine(UpdateProgress(audioFiles[2], CrossroadProgress.Done));
        } else if (progress == CrossroadProgress.Done)
        {
            UnlockNextTeleportArea();
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

    private void UnlockNextTeleportArea()
    {
        firstCrossing.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
