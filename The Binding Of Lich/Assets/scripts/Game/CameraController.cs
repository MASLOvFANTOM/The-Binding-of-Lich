using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [Header("Основа")]
    public GameObject player;
    public GameObject cameraEmpty;
    public Animator cameraAnimator;
    
    [Header("Миникарта")] 
    public Animator miniMapAnimator;
    public Animator miniMapCamera;
    public bool tabPressed;


    private void Start()
    {
        Invoke("SerializeParametrs", 0.2f);
    }

    private void SerializeParametrs() // Определление параметров
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Определение игрока
        miniMapCamera = GameObject.FindGameObjectWithTag("minimap camera").GetComponent<Animator>(); // определение миникарты аниматора
    }

    private void Update()
    {
        Move(); // Движение за игроком
        LevelRestart(); // Рестарт уровня
        ChangeStateMiniMapCamera();
    }

    public void ChangeStateMiniMapCamera() // Изменение положений мини-карты
    {
        if (Input.GetKeyDown(KeyCode.Tab)) tabPressed = !tabPressed;
        miniMapAnimator.SetBool("big map", tabPressed);
        miniMapCamera.SetBool("big", tabPressed);
    }

    public void CameraShake() // Анимация тряски
    {
        cameraAnimator.SetTrigger("shake"); 
    }

    private void Move() // Движение за игроком
    {
        
        Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, 0); // правильная позиция для камеры
        cameraEmpty.transform.DOMove(newPos, 0.4f); // движение камеры
    }

    public void LevelRestart() // Рестарт уровня
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.sceneCount);
        }
    }
}