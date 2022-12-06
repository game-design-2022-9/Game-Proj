using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemNameText;

    public void UdpateItemName(ItemName itemName )
    {
        itemNameText.text = itemName switch
        {
            ItemName.piece1 => "piece1",
            ItemName.Key => "一把钥匙",
            ItemName.piece2 => "piece2",
            ItemName.piece3 => "piece3",
            ItemName.piece4 => "piece4",
            ItemName.Ticket => "一张船票",
            ItemName.letter=>"一封信",
            _ => ""
        };
    }
}
