using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    protected Transform player;
    protected UI ui;

    [SerializeField] private Transform npc;
    [SerializeField] private GameObject interactToolTip;

    [Header("Floaty Tooltip")]
    [SerializeField] private float floatSpeed = 8f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private bool facingRight = true;
    protected virtual void Awake()
    {
        ui = FindFirstObjectByType<UI>();
        startPosition = interactToolTip.transform.position;
        interactToolTip.SetActive(false);
    }
    //protected virtual void Start()
    //{
    //    npc = GetComponentInChildren<Transform>(); 
    //}
    protected virtual void Update()
    {
        HandleNPCFlip();
        HandleNPCToolTipFloat();
    }
    private void HandleNPCToolTipFloat()
    {
        if (interactToolTip.activeSelf)
        {
            float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
            interactToolTip.transform.position = startPosition + new Vector3(0, yOffset);
        }
    }
    private void HandleNPCFlip()
    {
        if (player == null || npc == null) return;

        if(npc.position.x > player.position.x && facingRight)
        {
            npc.transform.Rotate(0, 180, 0);
            facingRight = false;
        }
        else if(npc.position.x < player.position.x && facingRight == false)
        {
            npc.transform.Rotate(0,180,0);
            facingRight = true;
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.transform;
        interactToolTip.SetActive(true);
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        interactToolTip.SetActive(false);
    }
}
