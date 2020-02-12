using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 背包里的物品
/// </summary>
public class PackageItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    private EquipmentBase equipment;
    RectTransform rectTransform;

    private void changeSprite(Sprite sprite)
    {
        this.GetComponent<Image>().sprite = sprite;
    }

    public void setEquipment(EquipmentBase equipment)
    {
        this.equipment = equipment;
        changeSprite(equipment.equipmentSprite);
    }

    void Start ()
    {
        Debug.Log("TODO 物品槽的对象池");

        rectTransform = this.GetComponent<RectTransform>();
        if (equipment == null)
            equipment = new EquipmentBase();
    }





    //拖动相关 ⬇
    private Transform originalParent;

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject rayHit = eventData.pointerCurrentRaycast.gameObject;
        if (rayHit != null)
        {
            //Debug.Log("射线命中了" + rayHit.gameObject);
            if (rayHit.tag == "UI_slot")
            {
                if (equipment.equipmentType == rayHit.GetComponent<PackageSlot>().slotType)//判断槽是否合法
                {
                    //装入装备槽
                    this.transform.SetParent(rayHit.gameObject.transform);
                    this.transform.position = rayHit.gameObject.transform.position;
                    Destroy(originalParent.gameObject);

                    //触发装备函数
                    equipment.onPutOnEquipment(PackageUtil.playerAttribute);
                }
                else
                {
                    Debug.Log("是不合法的装备槽");
                    this.transform.position = originalParent.position;
                    this.transform.SetParent(originalParent);
                }
            }
            else if (rayHit.tag == "Package_item")
            {
                Debug.Log("与另一件物品交换");
                if (this.equipment.equipmentType == rayHit.GetComponent<PackageItem>().equipment.equipmentType)
                {
                    //槽里的 和 其它交换
                    if (this.equipment.isPutOn)
                    {
                        SlotExchangeItem(this, rayHit.GetComponent<PackageItem>(), originalParent, true);
                    }
                    //其它 和 槽里交换
                    else if (rayHit.GetComponent<PackageItem>().equipment.isPutOn)
                    {
                        SlotExchangeItem(this, rayHit.GetComponent<PackageItem>(), originalParent, false);
                    }
                }
                //包里的两件物品交换
                else if (!this.equipment.isPutOn && !rayHit.GetComponent<PackageItem>().equipment.isPutOn)
                {
                    Debug.Log("包里的两件物品交换");
                    PackageExchangeItem(this, rayHit.GetComponent<PackageItem>(), originalParent);
                }
                else//非法物品想和槽里的物品交换
                {
                    this.transform.position = originalParent.position;
                    this.transform.SetParent(originalParent);
                }
            }
            else if (rayHit.name == "UI_背包栏" && equipment.isPutOn)//命中了背包且本身在装备槽中
            {
                Debug.Log("脱掉并放回装备");
                //rayHit.GetComponentInParent<PackageUtil>().ReturnEquipmentFromSlot(this.equipment);
                PackageUtil.Instance.ReturnEquipmentFromSlot(this.equipment);
                equipment.onTakeOffEquipment(PackageUtil.playerAttribute);
                Destroy(this.gameObject);
            }
            else//命中的不是合法Tag
            {
                this.transform.position = originalParent.position;
                this.transform.SetParent(originalParent);
            }
        }
        else
        {
            this.transform.position = originalParent.position;
            this.transform.SetParent(originalParent);
        }


        //能够重回新被拖动
        this.GetComponent<Image>().raycastTarget = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        originalParent = this.transform.parent;
        //更改层级 取消阻挡射线
        this.transform.SetParent(PackageUtil.parentPackage.transform);
        this.transform.SetAsLastSibling();
        this.GetComponent<Image>().raycastTarget = false;
    }

    /// <summary>
    /// 交换背包里的两个物品
    /// </summary>
    void PackageExchangeItem(PackageItem thisItem,PackageItem targetItem,Transform originalParent)
    {
        thisItem.transform.SetParent(targetItem.transform.parent);
        thisItem.transform.position = targetItem.transform.position;

        targetItem.transform.SetParent(originalParent);
        targetItem.transform.position = originalParent.position;
    }

    /// <summary>
    /// 交换装备槽和背包中的两个物品
    /// </summary>
    /// <param name="thisItem"></param>
    /// <param name="targetItem"></param>
    /// <param name="originalParent"></param>
    /// <param name="isThisInSlot"> thisItem是否是槽里的物品 </param>
    void SlotExchangeItem(PackageItem thisItem, PackageItem targetItem, Transform originalParent,bool isThisInSlot)
    {
        thisItem.transform.SetParent(targetItem.transform.parent);
        thisItem.transform.position = targetItem.transform.position;

        targetItem.transform.SetParent(originalParent);
        targetItem.transform.position = originalParent.position;

        if (isThisInSlot)
        {
            thisItem.equipment.onTakeOffEquipment(PackageUtil.playerAttribute);
            targetItem.equipment.onPutOnEquipment(PackageUtil.playerAttribute);
        }
        else
        {
            targetItem.equipment.onTakeOffEquipment(PackageUtil.playerAttribute);
            thisItem.equipment.onPutOnEquipment(PackageUtil.playerAttribute);
        }

    }


    //点击事件
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        PackageUtil.descriptionText.text = equipment.description;
    }
}
