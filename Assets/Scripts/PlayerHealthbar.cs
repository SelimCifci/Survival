using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    public Player player;
    public RectTransform bar;

    private void Update()
    {
        bar.sizeDelta = new Vector2(1.6f * player.hp, bar.sizeDelta.y);
    }
}
