using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;

public class ParentCharactrsController : MonoBehaviour
{
    [Header("Параметры игрока")]
    public float health; // Жизни
    public float maxHealth; // Максимм жизней
    public float stamina, maxStamina; // Стамина; Максимальная стамина
    public int mana, maxMana; // Мана; Максимальная мана
    public int damage; // Урон игрока
    public int criticalChance = 10; // Шанс Крита
    [Tooltip("Скорость")]public float realSpeed, boostSpeed, lockedSpeed; // Скорость движения
    [Tooltip("Блокирование действий")]public bool lockedBoost, lockedMove; // Блокировка
    [Tooltip("Бессмертие")]public bool invulnerable; // Бессмертие
    [Tooltip("Перемещение из комнаты в комнату")]public bool moveRoom; // Перемещаюсь-ли по комноте
    
    [Space]
    public Camera camera; // Камера
    
    [Header("Списки")]
    public Transform[] allObjectForFlip; // Все объектв для разворота

    [Header("Другое")]
    public Image fillStaminaBar, fillHealthBar, fillManaBar;
    public Text healthAmountText, manaAmountText;
    public Transform pointAttack;
    public Animator _animator;
    public Animation getDamageAnimation;
    public Animator dustParticlemAnimator; // Партиклы бега
    public GameObject particleBlood; // Партикллы крови
    public ParticleSystem[] moreEffectParticle; // 0 --> fire; 1 --> bleeding; 2 --> blindness; 3 --> poison; 4 --> freeze
    public Animator BlindnessAnimator; // Слерота

    private void Awake()
    {
        ParentCharactrsController charactrsController = GameObject.FindGameObjectWithTag("NeedToCharacter")
            .GetComponent<ParentCharactrsController>();
        fillStaminaBar = charactrsController.fillStaminaBar;
        fillHealthBar = charactrsController.fillHealthBar;
        fillManaBar = charactrsController.fillManaBar;
        camera = charactrsController.camera;
        _animator = GetComponent<Animator>();
        healthAmountText = charactrsController.healthAmountText;
        manaAmountText = charactrsController.manaAmountText;
        BlindnessAnimator = charactrsController.BlindnessAnimator;
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
        dustParticlemAnimator.SetFloat("run", Mathf.Abs(xAxis) + Mathf.Abs(yAxis)); // включение анимации у частиц
    }

    public void FlipGraphics(int xDirection) // Переворот всех объектов которые надо перевернуть
    {
        
        xDirection = Mathf.Clamp(xDirection, -1, 0);
        xDirection = xDirection * -180;
        for (int i = 0; i < allObjectForFlip.Length; i++)
        {
            allObjectForFlip[i].rotation = Quaternion.Euler(0, xDirection, 0);
        }
        dustParticlemAnimator.transform.rotation = Quaternion.Euler(-90, xDirection, 0);
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

    public virtual void GetDamage(int monsterDamage, bool spawnBlood, bool poison) // Когда получил урон
    {
        health -= monsterDamage;
        getDamageAnimation.Play("GetDamage");
        if(spawnBlood) SpawnBloodParticle();
    }

    public void SpawnBloodParticle() // Кровь при поллучении урона
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 1.5f);
        Instantiate(particleBlood, newPos, Quaternion.Euler(-90, 0, 0));
    }

    public IEnumerator InvulnerableTimer() // Таймер бессмертия
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
    public IEnumerator FireEffect(int time) // Огонь
    {
        moreEffectParticle[0].Play();
        for (int i = 0; i < time; i++)
        {
            GetDamage(1, false, true);
            yield return new WaitForSeconds(1f);
        }
        moreEffectParticle[0].Stop();
    }
    
    public IEnumerator BleedingEffect(int time) // Кровотечение
    {
        moreEffectParticle[1].Play();
        for (int i = 0; i < time; i++)
        {
            GetDamage(1, false, true);
            yield return new WaitForSeconds(1f);
        }
        moreEffectParticle[1].Stop();
    }
    public IEnumerator BlindnessEffect(int time) // Слепота
    {
        moreEffectParticle[2].Play();
        for (int i = 0; i < time; i++)
        {
            BlindnessAnimator.SetBool("on", true);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        moreEffectParticle[2].Stop();
        BlindnessAnimator.SetBool("on", false);
    }
    
    public IEnumerator PoisonEffect(int time) // Отравление
    {
        moreEffectParticle[3].Play();
        for (int i = 0; i < time; i++)
        {
            GetDamage(1, false, true);
            yield return new WaitForSeconds(1f);
        }
        moreEffectParticle[3].Stop();
    }
    
    public IEnumerator FreezeEffect(int time) // Заморозка
    {
        moreEffectParticle[4].Play();
        for (int i = 0; i < time; i++)
        {
            lockedMove = true;
            yield return new WaitForSeconds(1f);
        }
        lockedMove = false;
        moreEffectParticle[4].Stop();
    }
    
    #endregion
    

    public void ManaAndHealthControl() // настройка маны и жизней
    {
        fillHealthBar.fillAmount = health / maxHealth;
        fillManaBar.fillAmount = mana / maxMana;
        
        healthAmountText.text = health.ToString();
        manaAmountText.text = mana.ToString();

        if (stamina < maxStamina) fillStaminaBar.gameObject.transform.parent.gameObject.SetActive(true); 
        else fillStaminaBar.gameObject.transform.parent.gameObject.SetActive(false);

        // Ограничение параметров
        health = Mathf.Clamp(health, 0, maxHealth);
        mana = Mathf.Clamp(mana, 0, maxMana);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        
        fillStaminaBar.fillAmount = stamina / maxStamina;
    }
}
