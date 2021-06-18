using System;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private TextMeshPro _textMeshPro;
    private float timeHide = 0.5f;
    private Color textColor;
    private Color criticalColor = new Color(255, 30, 10);
    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        // Движение вверх
        float moveSpeedY = 3f;
        timeHide -= Time.deltaTime;
        transform.position += new Vector3(0, moveSpeedY) * Time.deltaTime;
        
        // Исчезновение
        if (timeHide <= 0)
        {
            float hideSpead = 3f;
            textColor.a -= hideSpead * Time.deltaTime;
            _textMeshPro.color = textColor;
        }
    }

    public void create(Vector3 position, int damage, bool criticalChance) // Создание объекта
    {
        Transform damagePopupTransform = Instantiate(this.transform, position, Quaternion.identity); // Создание
        
        DamagePopUp damagePopUp = damagePopupTransform.GetComponent<DamagePopUp>(); // Определение компонента
        damagePopUp.SetUp(damage, criticalChance); // Задаю параметры на тексте(урон и крит)
    }

    public void SetUp(int damage, bool criticalChance) // Установка
    {
        _textMeshPro.SetText(damage.ToString()); // Текст
        textColor = _textMeshPro.color; //Цвет
        if (criticalChance) textColor = criticalColor; // Крит цвет
    }
}
