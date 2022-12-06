using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InventoryUI : MonoBehaviour
{
    public Button leftButton, rightButton;

    public SlotUI slotUI;

    public int currentIndex;    //显示UI当前物品序号

    private void OnEnable()
    {
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateUIEvent -= OnUpdateUIEvent;
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if (itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if (index > 0)
                leftButton.interactable = true;
            if (index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }
    }

    /// <summary>
    /// 左右按钮Event事件
    /// </summary>
    /// <param name="amount"></param>
    public void SwitchItem(int amount)
    {
        currentIndex  += amount;

        if (currentIndex == 0)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if (currentIndex >= InventoryManager.getItemCount - 1)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        if(currentIndex>0&& currentIndex< InventoryManager.getItemCount - 1)
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
       
        EventHandler.CallChangeItemEvent(currentIndex);
    }
}
