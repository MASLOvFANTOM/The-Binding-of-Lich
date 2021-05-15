using UnityEngine;

public class KnightController : ParentCharactrsController
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move(rb);
        ManaAndHealthControl();
    }
}
