using System;
using System.Collections;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public bool open; // Открыта ли
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites; // 0 --> open; 1 --> close;
    public GameObject teleportationPoint; // Куда телепортировать игрока


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CloseDoor() // Закрытие двери
    {
        _spriteRenderer.sprite = sprites[1];
        open = false;
    }

    IEnumerator OpenDoor() // Открытие двери
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = sprites[0];
        open = true;
    }

    private void OnTriggerStay2D(Collider2D other) // Если игрок в триггере
    {
        if (open) // Дверь открыта
        {
            if (other.CompareTag("Player")) // Если игрок
            {
                ParentCharactrsController _charactrsController = other.GetComponent<ParentCharactrsController>();
                
                // Переместить игрока в след. комнату
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