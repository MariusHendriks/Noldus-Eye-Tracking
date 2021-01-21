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

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;

        playableAnimations.Add("isAngry", 2f);
        playableAnimations.Add("isBowing", 2f);
        playableAnimations.Add("isKnodding", 2f);
        playableAnimations.Add("isAnnoyed", 20f);

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

                StartCoroutine(PlayAnimation());
                Debug.Log("Play animation");
                //Stop movement
                //Trigger animation
            }


            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 10f, Color.yellow);
        }
        else
        {
            npcLookingAt = null;
        }
    }



    private IEnumerator PlayAnimation()
    {
        Animator animator = npcLookingAt.GetComponentInParent<Animator>();
        PathMovement pathMovement = npcLookingAt.GetComponentInParent<PathMovement>();
        NavMeshAgent navAgent = npcLookingAt.GetComponentInParent<NavMeshAgent>();

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

        animator.SetTrigger(animationTrigger);

        yield return new WaitForSeconds(animationDuration);
        
        animator.applyRootMotion = false;

        if (isMoving)
        {
            animator.SetBool("isMoving", true);
            pathMovement.enabled = true;
        }
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
