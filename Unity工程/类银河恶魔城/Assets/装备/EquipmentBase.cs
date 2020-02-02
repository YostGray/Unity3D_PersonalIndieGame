using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可被玩家装备的装备基类
/// </summary>
public class EquipmentBase
{
    public string equipmentName { set; get; }//装备的名字
    public bool isPutOn { set; get; }//是否已经穿上了
    //public GameObject packageEquipment = null;//对应背包中的装备
    public Sprite equipmentSprite { set; get; }
    public string description { set; get; }
    public EquipmentType equipmentType;

    public EquipmentBase()
    {
        equipmentName = "未命名";
        isPutOn = false;
        description = "未撰写说明";
        equipmentType = EquipmentType.unknow;
    }

    // 穿上此道具
    virtual public void onPutOnEquipment(PlayerAttribute playerAttribute)
    {
        Debug.Log("Equipment:穿上"+equipmentName);
        isPutOn = true;
    }

    // 脱下此道具
    virtual public void onTakeOffEquipment(PlayerAttribute playerAttribute)
    {
        Debug.Log("Equipment:脱下" + equipmentName);
        isPutOn = false;
    }
}

public enum EquipmentType
{
    unknow,
    foot,
    body,
    weapon,
}
