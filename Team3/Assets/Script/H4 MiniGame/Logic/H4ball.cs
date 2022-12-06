using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H4ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public H4BallDetails H4ballDeatils;

    public bool isMatch;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetupBall(H4BallDetails H4ball)
    {
        H4ballDeatils = H4ball;

        if (isMatch)
            SetRight();
        else
            SetWrong();
    }

    public void SetRight()
    {
        spriteRenderer.sprite = H4ballDeatils.rightSprite;
    }

    public void SetWrong()
    {
        spriteRenderer.sprite = H4ballDeatils.wrongSprite;
    }


}
