using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用以在相应的时候改变玩家不同部位的图片
/// tip:动画编辑器优先级高于这些代码
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer hairRenderer, lefthandClothRenderer, lefthandSkinRenderer, clothRenderer, bodySkinRenderer,
        headRenderer, righthandClothRenderer, righthandSkinRenderer, trousersRenderer, footSkinRenderer;
    private PlayerSpriteStorage spriteStorage;
    private PlayerAttribute playerAttribute;

    bool isOverwriteHand = false;

    void Start()
    {
        spriteStorage = GameObject.FindGameObjectWithTag("变装仓库").GetComponent<PlayerSpriteStorage>();
        playerAttribute = this.GetComponent<PlayerAttribute>();
    }

    private void animation_stand()
    {
        changeHair(PlayerSpriteStorage.hairMotionType.stand);
        changeHead(PlayerSpriteStorage.headMotionType.stand);
        changeBody(PlayerSpriteStorage.bodyMotionType.stand);
        changeFoot(PlayerSpriteStorage.footMotionType.stand);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.stand);

    }

    private void animation_walkFront()
    {
        changeHair(PlayerSpriteStorage.hairMotionType.stand);
        changeHead(PlayerSpriteStorage.headMotionType.stand);
        changeBody(PlayerSpriteStorage.bodyMotionType.walkFront);
        changeFoot(PlayerSpriteStorage.footMotionType.walkFront);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.walkFront);
    }

    private void animation_walkMid()
    {
        changeFoot(PlayerSpriteStorage.footMotionType.walkMid);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.walkMid);
    }

    private void animation_walkBack()
    {
        changeFoot(PlayerSpriteStorage.footMotionType.walkBack);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.walkBack);
    }

    private void animation_jump()
    {
        changeHead(PlayerSpriteStorage.headMotionType.up);
        changeHair(PlayerSpriteStorage.hairMotionType.up);
        changeBody(PlayerSpriteStorage.bodyMotionType.stand);
        changeFoot(PlayerSpriteStorage.footMotionType.jump);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.stand);

    }

    private void animation_fall()
    {
        changeHead(PlayerSpriteStorage.headMotionType.down);
        changeHair(PlayerSpriteStorage.hairMotionType.down);
        changeBody(PlayerSpriteStorage.bodyMotionType.walkFront);
        changeFoot(PlayerSpriteStorage.footMotionType.fall);
        if (!isOverwriteHand)
            changeHand(PlayerSpriteStorage.handMotionType.stand);
    }


    private void animation_crouchStand()
    {
        changeHead(PlayerSpriteStorage.headMotionType.down);
        changeHair(PlayerSpriteStorage.hairMotionType.down);


        changeBody(PlayerSpriteStorage.bodyMotionType.crouch);
        changeFoot(PlayerSpriteStorage.footMotionType.crouch);
        changeHand(PlayerSpriteStorage.handMotionType.crouch);
    }
    private void animation_crouchWalk1()
    {
        changeFoot(PlayerSpriteStorage.footMotionType.crouchWalk1);
    }
    private void animation_crouchWalk2()
    {
        changeFoot(PlayerSpriteStorage.footMotionType.crouchWalk2);
    }

    private void animation_holdgunUp()
    {
        changeHand(PlayerSpriteStorage.handMotionType.holdgun_up);
    }
    private void animation_holdgunFront()
    {
        changeHand(PlayerSpriteStorage.handMotionType.holdgun_front);
    }
    private void animation_holdgunObliqueUp()
    {
        changeHand(PlayerSpriteStorage.handMotionType.holdgun_obliqueUp);
    }




    private void changeHead(PlayerSpriteStorage.headMotionType type)
    {
        switch (type)
        {
            case PlayerSpriteStorage.headMotionType.stand:
                if (spriteStorage.headDictionary.ContainsKey(playerAttribute.faceKeyName))
                {
                    headRenderer.sprite = spriteStorage.headDictionary[playerAttribute.faceKeyName].headStand;

                }
                break;
            case PlayerSpriteStorage.headMotionType.up:
                if (spriteStorage.headDictionary.ContainsKey(playerAttribute.faceKeyName))
                {
                    headRenderer.sprite = spriteStorage.headDictionary[playerAttribute.faceKeyName].headUp;
                }
                break;
            case PlayerSpriteStorage.headMotionType.down:
                if (spriteStorage.headDictionary.ContainsKey(playerAttribute.faceKeyName))
                {
                    headRenderer.sprite = spriteStorage.headDictionary[playerAttribute.faceKeyName].headDown;
                }
                break;
            default:
                break;
        }
    }
    private void changeHair(PlayerSpriteStorage.hairMotionType type)
    {
        switch (type)
        {
            case PlayerSpriteStorage.hairMotionType.stand:
                if (spriteStorage.hairDictionary.ContainsKey(playerAttribute.hairKeyName))
                {
                    hairRenderer.sprite = spriteStorage.hairDictionary[playerAttribute.hairKeyName].hairStand;
                }
                break;
            case PlayerSpriteStorage.hairMotionType.up:
                if (spriteStorage.hairDictionary.ContainsKey(playerAttribute.hairKeyName))
                {
                    hairRenderer.sprite = spriteStorage.hairDictionary[playerAttribute.hairKeyName].hairUp;
                }
                break;
            case PlayerSpriteStorage.hairMotionType.down:
                if (spriteStorage.hairDictionary.ContainsKey(playerAttribute.hairKeyName))
                {
                    hairRenderer.sprite = spriteStorage.hairDictionary[playerAttribute.hairKeyName].hairDown;
                }
                break;
            default:
                break;
        }
    }
    private void changeBody(PlayerSpriteStorage.bodyMotionType type)
    {
        switch (type)
        {
            case PlayerSpriteStorage.bodyMotionType.stand:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    clothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].bodyStand;
                }
                //else
                //{
                //    foreach (var item in spriteStorage.bodyDictionary.Keys)
                //    {
                //        Debug.Log(item+"!="+ playerAttribute.bodyKeyName);
                //    }
                //}
                break;
            case PlayerSpriteStorage.bodyMotionType.walkFront:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    clothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].bodyWalk;
                }
                break;
            case PlayerSpriteStorage.bodyMotionType.walkMid:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    clothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].bodyWalk;
                }
                break;
            case PlayerSpriteStorage.bodyMotionType.walkBack:
                //也许可以省略
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    clothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].bodyWalk;
                }
                break;
            case PlayerSpriteStorage.bodyMotionType.crouch:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    clothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].bodyCrouch;
                }
                break;
            default:
                break;
        }
    }
    private void changeHand(PlayerSpriteStorage.handMotionType type)
    {
        switch (type)
        {
            case PlayerSpriteStorage.handMotionType.stand:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandStand;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandStand;
                }
                break;
            case PlayerSpriteStorage.handMotionType.walkFront:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandWalkFront;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandWalkFront;
                }
                break;
            case PlayerSpriteStorage.handMotionType.walkMid:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandStand;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandStand;
                }
                break;
            case PlayerSpriteStorage.handMotionType.walkBack:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandWalkBack;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandWalkBack;
                }
                break;
            case PlayerSpriteStorage.handMotionType.crouch:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandcrouch;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandcrouch;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_front:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponFront;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponFront;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_front_Recoil:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponFrontRecoil;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponFrontRecoil;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_obliqueUp:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponObliqueUp;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponObliqueUp;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_obliqueUp_Recoil:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponObliqueUpRecoil;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponObliqueUpRecoil;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_up:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponUp;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponUp;
                }
                break;
            case PlayerSpriteStorage.handMotionType.holdgun_up_Recoil:
                if (spriteStorage.bodyDictionary.ContainsKey(playerAttribute.bodyKeyName))
                {
                    lefthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].leftHandHoldWeaponUpRecoil;
                    righthandClothRenderer.sprite = spriteStorage.bodyDictionary[playerAttribute.bodyKeyName].rightHandHoldWeaponUpRecoil;
                }
                break;
            default:
                break;
        }
    }
    private void changeFoot(PlayerSpriteStorage.footMotionType type)
    {
        switch (type)
        {
            case PlayerSpriteStorage.footMotionType.stand:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footStand;
                }
                break;
            case PlayerSpriteStorage.footMotionType.walkFront:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footWalkFront;
                }
                break;
            case PlayerSpriteStorage.footMotionType.walkMid:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footWalkMid_Jump;
                }
                break;
            case PlayerSpriteStorage.footMotionType.walkBack:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footWalkBack;
                }
                break;
            case PlayerSpriteStorage.footMotionType.jump:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footWalkMid_Jump;
                }
                break;
            case PlayerSpriteStorage.footMotionType.fall:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footJumpFall;
                }
                break;
            case PlayerSpriteStorage.footMotionType.crouch:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footCrouch;
                }
                break;
            case PlayerSpriteStorage.footMotionType.crouchWalk1:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footCrouchWalk1;
                }
                break;
            case PlayerSpriteStorage.footMotionType.crouchWalk2:
                if (spriteStorage.footDictionary.ContainsKey(playerAttribute.footKeyName))
                {
                    trousersRenderer.sprite = spriteStorage.footDictionary[playerAttribute.footKeyName].footCrouchWalk2;
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 切换手部的层级 同时使得衣服的手臂不会随 站立 走动 跳跃 改变
    /// </summary>
    /// <param name="key">为真时 右手会在前面</param>
    public void switchHand(bool key)
    {
        if (key)
        {
            righthandSkinRenderer.sortingOrder = 11;
            righthandClothRenderer.sortingOrder = 12;
            isOverwriteHand = true;
        }
        else if (righthandSkinRenderer.sortingOrder!=3/* && !playerAttribute.isHoldingGun*/)
        {
            righthandSkinRenderer.sortingOrder = 3;
            righthandClothRenderer.sortingOrder = 4;
            isOverwriteHand = false;
        }
    }
}
