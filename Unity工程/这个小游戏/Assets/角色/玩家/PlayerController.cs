using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	PlayerActions playerActions;
    [SerializeField]
    PackageUtil Package;
	void Start () 
	{
		playerActions = this.GetComponent<PlayerActions>();
	}

    void Update()
    {
        //TODO 切换是否一直跑步的功能

        //蹲下
        if (Input.GetKey(KeyCode.S))
            playerActions.PlayerCrouch(true);
        else
            playerActions.PlayerCrouch(false);


        //移动
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A))
                playerActions.PlayerMove(true, true);
            else if (Input.GetKey(KeyCode.D))
                playerActions.PlayerMove(false, true);
        }
        else
        { 
            if (Input.GetKey(KeyCode.A))
                playerActions.PlayerMove(true,false);
            else if (Input.GetKey(KeyCode.D))
                playerActions.PlayerMove(false,false);
        }
        if (Input.GetKeyDown(KeyCode.J))
            playerActions.PlayerJump();

        //武器拿起
        if (Input.GetKeyDown(KeyCode.U))
        {
            playerActions.PlayerHoldWeapon();
        }

        //武器朝向
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerActions.PlayerWeaponDir(PlayerActions.GunDirMode.up);
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                playerActions.PlayerWeaponDir(PlayerActions.GunDirMode.ObliqueUp);
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            playerActions.PlayerWeaponDir(PlayerActions.GunDirMode.front);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //Debug.Log("K被按下了");
            playerActions.WeaponStoragePower();
            playerActions.GrapeSomething();
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            //Debug.Log("K被松开了");
            playerActions.WeaponFire();
            playerActions.ReleaseSomething();
        }

        //打开背包
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Package.isShowingPackage)
                Package.HidePackage();
            else
                Package.ShowPackage();
        }
    }
}
