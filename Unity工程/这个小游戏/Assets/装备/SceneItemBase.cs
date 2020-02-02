using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景物体基类
/// </summary>
public class SceneItemBase : MonoBehaviour
{
    /// <summary>
    /// 是否被捡起了，用以保存场景
    /// </summary>
    protected bool isBeenPicked { set; get; }
    protected static PackageUtil packageUtil;

    void Awake()
    {
        packageUtil = GameObject.Find("UI_背包").GetComponent<PackageUtil>();
    }
}
