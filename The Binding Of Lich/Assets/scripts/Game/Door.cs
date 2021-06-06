using System;
using System.Collections;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public bool open;
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites; // 0 --> open; 1 --> close;
    public GameObject teleportationPoint;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) OpenDoor();
        if(Input.GetKeyDown(KeyCode.J)) CloseDoor();
    }

    public void CloseDoor()
    {
        _spriteRenderer.sprite = sprites[1];
        open = false;
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = sprites[0];
        open = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (open)
        {
            if (other.CompareTag("Player"))
            {
                ParentCharactrsController _charactrsController = other.GetComponent<ParentCharactrsController>();
                if (!_charactrsController.moveRoom)
                {
                    other.transform.DOMoveX(teleportationPoint.transform.position.x, 0.2f);
                    other.transform.DOMoveY(teleportationPoint.transform.position.y, 0.2f);
                    _charactrsController.StartCoroutine(_charactrsController.MoveRoomTimer());
                }
            }
        }
    }
}