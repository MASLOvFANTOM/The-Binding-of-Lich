using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ParentCharactrsController : MonoBehaviour
{
    [Header("Параметры игрока")]
    [Range(0, 20)]public int health;
    [Range(0, 20)]public int maxHealth;
    public float stamina, maxStamina;
    [Range(0, 10)]public int mana, maxMana;
    public int damage;
    [Tooltip("Скорость")]public float realSpeed, boostSpeed, lockedSpeed;
    [Tooltip("Блокирование действий")]public bool lockedBoost, lockedMove;
    [Tooltip("Бессмертие")]public bool invulnerable;
    [Tooltip("Перемещение из комнаты в комнату")]public bool moveRoom;
    
    [Space]
    public Camera camera;
    
    [Header("Списки")]
    public Image[] allHealthCell = new Image[18];
    public Image[] allManaCell = new Image[9];
    public Sprite[] parameterStage = new Sprite[3];
    public Transform[] allObjectForFlip;
    
    [Header("Другое")]
    public Image fillStaminaBar;
    public Transform pointAttack;
    public Animator _animator;
    public Animation getDamageAnimation;
    public SpriteRenderer _SpriteRenderer;
    public Animator ParticleSystemAnimator;

    private void Awake()
    {
        ParentCharactrsController charactrsController = GameObject.FindGameObjectWithTag("NeedToCharacter")
            .GetComponent<ParentCharactrsController>();
        allHealthCell = charactrsController.allHealthCell;
        allManaCell = charactrsController.allManaCell;
        parameterStage = charactrsController.parameterStage;
        fillStaminaBar = charactrsController.fillStaminaBar;
        camera = charactrsController.camera;
        _animator = GetComponent<Animator>();
    }

    public void Move(Rigidbody2D rb) // Движение персонажа
    {
        // Движение
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = Vector2.zero;
        if (!lockedMove) moveDirection = new Vector2(xAxis, yAxis);
        rb.MovePosition(rb.position + moveDirection * (realSpeed * Time.fixedDeltaTime));
        
        
        Boost(xAxis, yAxis);
        _animator.SetFloat("run", Mathf.Abs(xAxis) + Mathf.Abs(yAxis)); // включение анимации бега
        ParticleSystemAnimator.SetFloat("run", Mathf.Abs(xAxis) + Mathf.Abs(yAxis)); // включение анимации у частиц
    }

    public void FlipGraphics(int xDirection) // Переворот всех объектов которые надо перевернуть
    {
        
        xDirection = Mathf.Clamp(xDirection, -1, 0);
        xDirection = xDirection * -180;
        for (int i = 0; i < allObjectForFlip.Length; i++)
        {
            allObjectForFlip[i].rotation = Quaternion.Euler(0, xDirection, 0);
        }
        ParticleSystemAnimator.transform.rotation = Quaternion.Euler(-90, xDirection * -1, 0);
    }

    public void ChangePosPointAttack() // Изменение положения точки атаки.
    {
        Vector3 currentMousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(
            currentMousePos.x - pointAttack.parent.transform.position.x,
            currentMousePos.y - pointAttack.parent.transform.position.y);
        direction = direction * -1;
        FlipGraphics(Convert.ToInt16(direction.x));
        pointAttack.parent.transform.up = direction;
    }

    public void Boost(float xAxis, float yAxis) // Ускорение движения
    {
        if (!lockedBoost)
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                realSpeed = boostSpeed;
                stamina -= (500 / maxStamina) * Time.deltaTime;
            }
            else
            {
                if (xAxis == 0 && yAxis == 0) stamina += (700 / maxStamina) * Time.deltaTime;
                else stamina += (400 / maxStamina) * Time.deltaTime;
                realSpeed = lockedSpeed;
            }
        }
    }

    public virtual void GetDamage(int damage) // Когда получил урон
    {
        health -= damage;
        getDamageAnimation.Play("GetDamage");
    }

    public IEnumerator InvulnerableTimer() // Таймер на бессмертие
    {
        invulnerable = true;
        yield return new WaitForSeconds(0.7f);
        invulnerable = false;
        StopCoroutine(InvulnerableTimer());
    }

    public IEnumerator MoveRoomTimer() // Таймер на передвижение между комнатами
    {
        moveRoom = true;
        yield return new WaitForSeconds(1f);
        moveRoom = false;
    }

    #region Effects
    public IEnumerator FireEffect(int time)
    {
        for (int i = 0; i < time; i++)
        {
            GetDamage(1);
            yield return new WaitForSeconds(1f);
        }
    }
    
    #endregion
    

    public void ManaAndHealthControl() // настройка маны и жизней
    {
        // Настройка жизней
        for (int i = 0; i < allHealthCell.Length; i++)
        {
            if (i < health)
            {
                allHealthCell[i].sprite = parameterStage[0];
                allHealthCell[i].gameObject.SetActive(true);
            }
            else if(i > health && i <= maxHealth)
            {
                allHealthCell[i].sprite = parameterStage[2];
                allHealthCell[i].gameObject.SetActive(true);
            }
            else
            {
                allHealthCell[i].gameObject.SetActive(false);
            }
        }
        
        // Настройка маны
        for (int i = 0; i < allManaCell.Length; i++)
        {
            if (i < mana)
            {
                allManaCell[i].sprite = parameterStage[1];
                allManaCell[i].gameObject.SetActive(true);
            }
            else if(i > mana && i <= maxMana)
            {
                allManaCell[i].sprite = parameterStage[2];
                allManaCell[i].gameObject.SetActive(true);
            }
            else
            {
                allManaCell[i].gameObject.SetActive(false);
            }
        }
        if (stamina < maxStamina) fillStaminaBar.gameObject.transform.parent.gameObject.SetActive(true); 
        else fillStaminaBar.gameObject.transform.parent.gameObject.SetActive(false);

        // Ограничение параметров
        health = Mathf.Clamp(health, 0, maxHealth);
        mana = Mathf.Clamp(mana, 0, maxMana);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        
        fillStaminaBar.fillAmount = stamina / maxStamina;
    }
}
