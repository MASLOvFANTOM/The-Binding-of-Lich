using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private RoomTemplate templates;
    public GameObject player;
    public Animator cameraEmpty;
    [Header("Миникарта")] 
    public Animator miniMapAnimator;
    public Animator miniMapCamera;
    public bool tabPressed;


    private void Start()
    {
        Invoke("SerializeParametrs", 0.2f);
    }

    private void SerializeParametrs()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplate>();
        player = GameObject.FindGameObjectWithTag("Player");
        miniMapCamera = GameObject.FindGameObjectWithTag("minimap camera").GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        LevelRestart();
        if(Input.GetKeyDown(KeyCode.K)) CameraShake();
        ChangeStateMiniMapCamera();
    }

    public void ChangeStateMiniMapCamera()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) tabPressed = !tabPressed;
        miniMapAnimator.SetBool("big map", tabPressed);
        miniMapCamera.SetBool("big", tabPressed);
    }

    public void CameraShake()
    {
        cameraEmpty.SetTrigger("Shake");
    }

    private void Move() // Движение за игроком
    {
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        transform.DOMove(newPos, 0.4f);
    }

    public void LevelRestart() // Рестарт уровня
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.sceneCount);
        }
    }
}