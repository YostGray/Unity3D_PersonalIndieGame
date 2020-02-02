using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 普通鞋子装备
/// </summary>
public class NormalShoes : EquipmentBase
{
    public NormalShoes()
    {
        equipmentName = "普通鞋子";
        equipmentSprite = Resources.Load<Sprite>("sprites/鞋子");
        description = "普通的鞋子，比较柔软\n穿上可以跑步";
        equipmentType = EquipmentType.foot;
    }

    override public void onPutOnEquipment(PlayerAttribute playerAttribute)
    {
        playerAttribute.canRun = true;
        playerAttribute.footKeyName = "普通";
        base.onPutOnEquipment(playerAttribute);
    }

    override public void onTakeOffEquipment(PlayerAttribute playerAttribute)
    {
        playerAttribute.canRun = false;
        playerAttribute.footKeyName = "裸体";
        //todo 脱光裤子后 把裤子置空
        //GameObject.Find("裤子").GetComponent<Image>().sprite = ;
        base.onTakeOffEquipment(playerAttribute);
    }
}
