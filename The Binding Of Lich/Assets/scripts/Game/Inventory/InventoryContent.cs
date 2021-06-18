using System;
using UnityEngine;
using UnityEngine.AI;

public class InventoryContent : MonoBehaviour
{
    public RectTransform rectTransform;
    public Animator scrollViewAnimator;
    private bool view;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Controller();
        OnOff();
    }

    private void Controller()
    {
        int childCount = gameObject.transform.childCount;
        rectTransform.sizeDelta = new Vector2(0, childCount * 100 + 10);

        int yPos = Convert.ToInt32(rectTransform.position.y);
        yPos = Convert.ToInt16(yPos / 100) * 100;
            
        rectTransform.position = new Vector2(rectTransform.position.x, yPos);
    }

    private void OnOff()
    {
        if (Input.GetKeyDown(KeyCode.I)) view = !view;
        scrollViewAnimator.SetFloat("parameter", Convert.ToInt16(view));
    }
}
