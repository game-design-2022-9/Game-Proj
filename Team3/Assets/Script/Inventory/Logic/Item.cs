using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemName itemName;

    public AudioSource audioSource;

    public void ItemClicked()
    {
        //添加到背包后隐藏物体
        audioSource.Play();
        InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }
}
