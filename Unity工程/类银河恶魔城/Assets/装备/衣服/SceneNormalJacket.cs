using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNormalJacket : SceneItemBase
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        NormalJacket normalJacket = new NormalJacket();

        if (collider.tag == "Player")
        {
            //Debug.Log("拾取了鞋子，装备后可以跑步了！");
            packageUtil.AddEquipmentToPackage(normalJacket);

            //TODO 标记场景中自己没有了 存档用
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
