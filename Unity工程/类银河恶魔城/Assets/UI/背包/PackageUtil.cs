using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 操作背包的工具类
/// </summary>
public class PackageUtil : MonoBehaviour
{
    private static PackageUtil _Instance;
    public static PackageUtil Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = GameObject.Find("UI_背包").GetComponent<PackageUtil>();
            }
            return _Instance;
        }
    }

    public static PlayerAttribute playerAttribute;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject solotPrefab;//物品槽预制体

    public static GameObject parentPackage;
    public static Text descriptionText;


    private int currentColumn = 0;
    RectTransform contentRectTransform;


    /// <summary>
    /// 玩家背包中添加装备
    /// </summary>
    /// <param name="equipment"></param>
    /// <param name="player"></param>
    public void AddEquipmentToPackage(EquipmentBase equipment)
    {
        playerAttribute.equipmentList.Add(equipment);

        //TODO 使用对象池以节省性能
        GameObject newEquipment = Instantiate(solotPrefab, content.transform);
        newEquipment.GetComponentInChildren<PackageItem>().setEquipment(equipment);//把背包图标与真实装备对应上

        RefreshPackageSize();
    }

    /// <summary>
    /// 从装备槽向背包放回装备
    /// </summary>
    public void ReturnEquipmentFromSlot(EquipmentBase equipment)
    {
        GameObject newEquipment = Instantiate(solotPrefab, content.transform);
        newEquipment.GetComponentInChildren<PackageItem>().setEquipment(equipment);//把背包图标与真实装备对应上
        RefreshPackageSize();
    }

    /// <summary>
    /// 根据背包里东西的数量改变内容量的大小
    /// </summary>
    public void RefreshPackageSize()
    {
        if (Mathf.Ceil(playerAttribute.equipmentList.Count/4.0f) != currentColumn)
        {
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, Mathf.Max(730, 120 * Mathf.Ceil(playerAttribute.equipmentList.Count / 4.0f)));
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, contentRectTransform.anchoredPosition.y + 100);
            currentColumn = (int)Mathf.Ceil(playerAttribute.equipmentList.Count / 4.0f);
        }
    }

    public bool isShowingPackage = true;
    public void HidePackage()
    {
        isShowingPackage = false;
        this.GetComponent<RectTransform>().localPosition =  new Vector3(0,-1480,0);
    }
    public void ShowPackage()
    {
        isShowingPackage = true;
        this.GetComponent<RectTransform>().localPosition = new Vector3();
    }


	void Awake()
    {
        contentRectTransform = content.GetComponent<RectTransform>();
        playerAttribute = GameObject.Find("玩家").GetComponent<PlayerAttribute>();
        parentPackage = GameObject.Find("UI_背包");
        descriptionText = GameObject.Find("UI_说明文字").GetComponent<Text>();
        HidePackage();
    }
}
