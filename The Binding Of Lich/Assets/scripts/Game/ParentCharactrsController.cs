using System;
using UnityEngine;
using UnityEngine.UI;

public class ParentCharactrsController : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float stamina;
    public float maxStamina;
    public int mana;
    public int maxMana;
    public float damage;
    public float realSpeed;
    public float boostSpeed;
    public float lockedSpeed;
    public bool lockedBoost;
    public Camera camera;
    public Image[] allHealthCell = new Image[18];
    public Image[] allManaCell = new Image[9];
    public Sprite[] parameterStage = new Sprite[3];
    public Image fillStaminaBar;

    private void Awake()
    {
        ParentCharactrsController charactrsController = GameObject.FindGameObjectWithTag("NeedToCharacter").GetComponent<ParentCharactrsController>();
        allHealthCell = charactrsController.allHealthCell;
        allManaCell = charactrsController.allManaCell;
        parameterStage = charactrsController.parameterStage;
        fillStaminaBar = charactrsController.fillStaminaBar;
        camera = charactrsController.camera;
    }

    public void Move(Rigidbody2D rb)
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        // rb.position = rb.position + new Vector2(xAxis, yAxis) * (Time.deltaTime * realSpeed);
        // rb.AddForce(new Vector2(xAxis, yAxis) * (Time.deltaTime * realSpeed) , ForceMode2D.Impulse);
        // transform.Translate(new Vector2(xAxis, yAxis) * (Time.deltaTime * realSpeed));
        rb.MovePosition(rb.position + new Vector2(xAxis, yAxis) * (realSpeed * Time.fixedDeltaTime));
        if (!lockedBoost)
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
            {
                realSpeed = boostSpeed;
                stamina -= (500 / maxStamina) * Time.deltaTime;
            }
            else
            {
                if(xAxis == 0 && yAxis == 0) stamina += (700 / maxStamina) * Time.deltaTime;
                else stamina += (400 / maxStamina) * Time.deltaTime;
                realSpeed = lockedSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CenterRoom"))
        {
            Debug.Log(other.transform.parent.transform.parent.name);
            camera.gameObject.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, -10);
        }
    }

    public void ManaAndHealthControl()
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

        health = Mathf.Clamp(health, 0, maxHealth);
        mana = Mathf.Clamp(mana, 0, maxMana);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        
        fillStaminaBar.fillAmount = stamina / maxStamina;
    }
}
