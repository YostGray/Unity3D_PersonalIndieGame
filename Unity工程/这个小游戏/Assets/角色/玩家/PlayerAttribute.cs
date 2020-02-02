using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家属性
/// </summary>
public class PlayerAttribute : MonoBehaviour
{
    public int hp = 1;
    public bool canMove = true, canJump = true, canWallJump = false,canRun = false;//能力开关
    public bool canHoldGun = true;//控制能不能拿起武器，蹲下时不行
    public float moveVelocity = 5.0f;
    public float jumpVelocity = 10.0f;
    public string footKeyName = "裸体", bodyKeyName = "裸体", hairKeyName = "普通",faceKeyName="普通";//TODO 不要用字面值，用枚举

    //public List<Bullet> bulletList;
    //public List<Iteam> iteamList;
    //public Dictionary<string,EquipmentBase> equipmentList = new Dictionary<string,EquipmentBase>();
    public List<EquipmentBase> equipmentList = new List<EquipmentBase>();


    public int gunPowerLimit = 1;//1 - 4 指枪的蓄力水平

    //TODO要写的方法
    //解析存档
    //更改能力
    //...
}
