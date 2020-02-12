using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJacket : EquipmentBase
{
    public NormalJacket()
    {
        equipmentName = "普通夹克";
        equipmentSprite = Resources.Load<Sprite>("sprites/普通夹克");
        description = "普通的夹克，没什么用\n看上去比较帅";
        equipmentType = EquipmentType.body;
    }
    override public void onPutOnEquipment(PlayerAttribute playerAttribute)
    {
        playerAttribute.bodyKeyName = "夹克";
        base.onPutOnEquipment(playerAttribute);
    }

    override public void onTakeOffEquipment(PlayerAttribute playerAttribute)
    {
        playerAttribute.bodyKeyName = "裸体";
        //todo 脱光裤子后 把裤子置空
        GameObject.Find("衣服").GetComponent<SpriteRenderer>().sprite = null;
        base.onTakeOffEquipment(playerAttribute);
    }
}
