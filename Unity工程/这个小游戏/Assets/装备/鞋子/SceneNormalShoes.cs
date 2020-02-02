using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 场景上的鞋子
/// </summary>
public class SceneNormalShoes : SceneItemBase
{
    //[SerializeField]
    //private PackageUtil packageUtil;

    void OnTriggerEnter2D(Collider2D collider)
    {
        EquipmentBase normalShoes = new NormalShoes();

        if (collider.tag == "Player")
        {
            //Debug.Log("拾取了鞋子，装备后可以跑步了！");

            packageUtil.AddEquipmentToPackage(normalShoes);

            //TODO 标记场景中自己没有了 存档用
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
