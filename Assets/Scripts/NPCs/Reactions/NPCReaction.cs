using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPCReaction : MonoBehaviour
{
    private Transform cameraTransform;
    private RaycastHit hitInfo;

    private GameObject npcLookingAt;

    private float timeSpentLooking;
    private float timeToLook = 1.5f;
    private bool triggeredNPC = false;

    private Dictionary<string, float> playableAnimations = new Dictionary<string, float>();

    private Dictionary<GameObject, Vector3> npcToRotate = new Dictionary<GameObject, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;

        playableAnimations.Add("isAngry", 2f);
        playableAnimations.Add("isBowing", 2f);
        playableAnimations.Add("isAcknowledging", 2f);
        playableAnimations.Add("isKnodding", 2f);
        playableAnimations.Add("isAnnoyed", 20f);

    }

    // Update is called once per frame
    void Update()
    {
        NPCHitLogic();
        RotateNPC();
    }

    private void RotateNPC()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject gameObject in npcToRotate.Keys)
        {
            Vector3 point = new Vector3();
            if (npcToRotate.TryGetValue(gameObject, out Vector3 value))
            {
                point = value;
            }

            Quaternion targetRotation = Quaternion.LookRotation(-point, Vector3.up);
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * 2.0f);

            if (Quaternion.Angle(gameObject.transform.rotation, targetRotation) <= 0.1f)
            {
                toRemove.Add(gameObject);
            }
        }

        foreach (var gameobject in toRemove)
        {
            npcToRotate.Remove(gameobject);
        }
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
                StartCoroutine(PlayAnimation(npcLookingAt));
            }


            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 10f, Color.yellow);
        }
        else
        {
            npcLookingAt = null;
        }
    }



    private IEnumerator PlayAnimation(GameObject npc)
    {
        Animator animator = npc.GetComponentInParent<Animator>();
        PathMovement pathMovement = npc.GetComponentInParent<PathMovement>();
        NavMeshAgent navAgent = npc.GetComponentInParent<NavMeshAgent>();

        bool isMoving = animator.GetBool("isMoving");
        string animationTrigger = RandomValues(playableAnimations).First();
        float animationDuration = 0;
        if (playableAnimations.TryGetValue(animationTrigger, out float duration)) animationDuration = duration; 

        if (isMoving)
        {
            animator.SetBool("isMoving", false);
            pathMovement.enabled = false;
            navAgent.speed = 0;
        }
        animator.applyRootMotion = true;

        StartNPCRotation(npc);

        while (npcToRotate.ContainsKey(npc))
            yield return null;

        animator.SetTrigger(animationTrigger);

        yield return new WaitForSeconds(animationDuration);
        
        animator.applyRootMotion = false;

        if (isMoving)
        {
            animator.SetBool("isMoving", true);
            pathMovement.enabled = true;
        }
    }

    private void StartNPCRotation(GameObject npc)
    {
        Vector3 cameraPosition = new Vector3(cameraTransform.position.x, 0, cameraTransform.position.z);
        Vector3 npcPosition = new Vector3(npc.transform.position.x, 0, npc.transform.position.z);

        Vector3 targetDirection =  npcPosition - cameraPosition;

        npcToRotate.Add(npc, targetDirection);
    }

    public IEnumerable<TKey> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        List<TKey> values = Enumerable.ToList(dict.Keys);
        int size = dict.Count;
        while (true)
        {
            yield return values[Random.Range(0, size)];
        }
    }

}
