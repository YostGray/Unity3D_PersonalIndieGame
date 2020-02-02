using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 告示牌
/// </summary>
public class Billboard : MonoBehaviour
{
    /// <summary>
    /// 告示牌显示的信息
    /// TODO 用输入输出系统替换特定的语句
    /// TODO 读配置文件以做到多语言
    /// </summary>
    [SerializeField]
    private string info;
    [SerializeField]
    private Sprite closed,opened;

    private GameObject billboardImage;
    private GameObject billboardText;

    void Start()
    {
        billboardImage = GameObject.Find("告示牌告示");
        billboardText = GameObject.Find("告示牌告示Text");
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 48, 0));
            billboardImage.transform.position = position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            billboardImage.SetActive(true);
            billboardText.GetComponent<Text>().text = info.Replace("\\n", "\n");
            Vector3 position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 16, 0));
            billboardImage.transform.position = position;
            this.GetComponent<SpriteRenderer>().sprite = opened;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            billboardImage.SetActive(false);
            this.GetComponent<SpriteRenderer>().sprite = closed;
        }
    }



    // Update is called once per frame
    void Update ()
    {
		
	}
}
