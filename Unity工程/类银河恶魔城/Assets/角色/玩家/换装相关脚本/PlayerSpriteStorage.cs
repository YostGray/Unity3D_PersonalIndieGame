using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 存储所有的主角的换装图片，用数组而不是容器是方便序列化
/// 感觉用数据库加反射会好不少，但是俺不会
/// </summary>
public class PlayerSpriteStorage : MonoBehaviour
{
    [SerializeField]
    private headSkin[] heads;
    [SerializeField]
    private hair[] hairs;
    [SerializeField]
    private body[] bodys;
    [SerializeField]
    private foot[] foots;

    public Dictionary<string, headSkin> headDictionary = new Dictionary<string, headSkin>();
    public Dictionary<string, hair> hairDictionary = new Dictionary<string, hair>();
    public Dictionary<string, body> bodyDictionary = new Dictionary<string, body>();
    public Dictionary<string, foot> footDictionary = new Dictionary<string, foot>();


    void Start()
    {
        foreach (var item in heads)
        {
            headDictionary.Add(item.keyName, item);
        }
        foreach (var item in hairs)
        {
            hairDictionary.Add(item.keyName, item);
        }
        foreach (var item in bodys)
        {
            bodyDictionary.Add(item.keyName, item);
        }
        foreach (var item in foots)
        {
            footDictionary.Add(item.keyName, item);
        }
    }


    [System.Serializable]
    public struct headSkin
    {
        public string keyName;
        public Sprite headStand, headUp, headDown;
    }
    public enum headMotionType
    {
        stand,
        up,
        down
    }


    [System.Serializable]
    public struct hair
    {
        public string keyName;
        public Sprite hairStand, hairUp, hairDown;
    }
    public enum hairMotionType
    {
        stand,
        up,
        down
    }


    [System.Serializable]
    public struct body
    {
        public string keyName;
        public Sprite bodyStand, bodyWalk, bodyCrouch;
        public Sprite leftHandStand, leftHandWalkFront, leftHandWalkBack, leftHandcrouch, 
            leftHandHoldWeaponFront, leftHandHoldWeaponFrontRecoil, leftHandHoldWeaponUp, leftHandHoldWeaponUpRecoil, 
            leftHandHoldWeaponObliqueUp, leftHandHoldWeaponObliqueUpRecoil;
        public Sprite rightHandStand, rightHandWalkFront, rightHandWalkBack, rightHandcrouch,
            rightHandHoldWeaponFront, rightHandHoldWeaponFrontRecoil, rightHandHoldWeaponUp, rightHandHoldWeaponUpRecoil,
            rightHandHoldWeaponObliqueUp, rightHandHoldWeaponObliqueUpRecoil;
    }
    public enum bodyMotionType
    {
        stand,
        walkFront,
        walkMid,
        walkBack,
        crouch,

    }
    public enum handMotionType
    {
        stand,
        walkFront,
        walkMid,
        walkBack,
        crouch,
        holdgun_front,
        holdgun_up,
        holdgun_obliqueUp,
        holdgun_front_Recoil,
        holdgun_up_Recoil,
        holdgun_obliqueUp_Recoil,
    }


    [System.Serializable]
    public struct foot
    {
        public string keyName;
        public Sprite footStand, footWalkFront,footWalkBack,footWalkMid_Jump,footJumpFall;
        public Sprite footCrouch, footCrouchWalk1, footCrouchWalk2;
    }
    public enum footMotionType
    {
        stand,
        walkFront,
        walkMid,
        walkBack,
        jump,
        fall,
        crouch,
        crouchWalk1,
        crouchWalk2,
    }


}
