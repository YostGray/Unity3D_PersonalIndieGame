using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家行为脚本
/// 被控制脚本调用，使得玩家做出不同的行为
/// </summary>
public class PlayerActions : MonoBehaviour 
{
    [SerializeField]
	GameObject player,weapon;
	Rigidbody2D playerRigidbody2D;
	PlayerAttribute playerAttribute;
	Animator playerAnimator;
    PlayerAnimation playerAnimation;

    private LayerMask groundLayerMask;
    private LayerMask interactiveLayerMask;


    void Start ()
	{
		playerRigidbody2D = player.GetComponent<Rigidbody2D> ();
		playerAttribute =player.GetComponent<PlayerAttribute> ();
		playerAnimator = player.GetComponent<Animator> ();
        playerAnimation = player.GetComponent<PlayerAnimation>();

        groundLayerMask = LayerMask.GetMask("Ground");
        interactiveLayerMask = LayerMask.GetMask("Interactive");
    }

	public void PlayerMove(bool isLeft,bool isRunning)
	{
        if (playerAttribute.canMove)
        {
            if (playerAnimator.GetBool("isCrouched"))
            {
                PlayerCrouchedMove(isLeft);
                return;
            }

            if (isRunning && playerAttribute.canRun)
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("走路"))
                    playerAnimator.speed = 1.5f;

                if (isLeft && playerAttribute.canMove && isFaceWall(FaceMode.headFace, isLeft) &&
                   isFaceWall(FaceMode.bodyFace, isLeft) && isFaceWall(FaceMode.footFace, isLeft))
                    playerRigidbody2D.velocity = new Vector2(-playerAttribute.moveVelocity * 1.5f, playerRigidbody2D.velocity.y);
                else if (playerAttribute.canMove && isFaceWall(FaceMode.headFace, isLeft) &&
                    isFaceWall(FaceMode.bodyFace, isLeft) && isFaceWall(FaceMode.footFace, isLeft))
                    playerRigidbody2D.velocity = new Vector2(playerAttribute.moveVelocity * 1.5f, playerRigidbody2D.velocity.y);
            }
            else
            {
                if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("走路"))
                    playerAnimator.speed = 1f;

                if (isLeft && playerAttribute.canMove && isFaceWall(FaceMode.headFace, isLeft) &&
                    isFaceWall(FaceMode.bodyFace, isLeft) && isFaceWall(FaceMode.footFace, isLeft))
                    playerRigidbody2D.velocity = new Vector2(-playerAttribute.moveVelocity, playerRigidbody2D.velocity.y);
                else if (playerAttribute.canMove && isFaceWall(FaceMode.headFace, isLeft) &&
                    isFaceWall(FaceMode.bodyFace, isLeft) && isFaceWall(FaceMode.footFace, isLeft))
                    playerRigidbody2D.velocity = new Vector2(playerAttribute.moveVelocity, playerRigidbody2D.velocity.y);
            }
        }
    }

	public void PlayerJump()
	{
        if (playerAttribute.canJump)
        {
            if (!playerAnimator.GetBool("isCrouched"))//蹲下不能跳跃
            {
                if (playerAttribute.canJump && isOntheGround())
                    playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, playerAttribute.jumpVelocity);
            }
        }
	}

    public void PlayerCrouch(bool isCrouch)
    {
        if (isCrouch)
        {
            playerAnimator.SetBool("isCrouched", isCrouch);
            PlayerHoldWeapon(false);
            playerAttribute.canHoldGun = false;
        }
        else//站起来
        {
            if (playerAnimator.GetBool("isCrouched") && canStandUp())
            {
                playerAnimator.SetBool("isCrouched", isCrouch);
                playerAttribute.canHoldGun = true;
            }
        }
    }

    private void PlayerCrouchedMove(bool isLeft)
    {
        if (isLeft && playerAttribute.canMove && isFaceWall(FaceMode.footFace, isLeft) &&
            isFaceWall(FaceMode.bodyFace, isLeft))
            playerRigidbody2D.velocity = new Vector2(-playerAttribute.moveVelocity * 0.8f, playerRigidbody2D.velocity.y);
        else if (playerAttribute.canMove && isFaceWall(FaceMode.footFace, isLeft) &&
            isFaceWall(FaceMode.bodyFace, isLeft))
            playerRigidbody2D.velocity = new Vector2(playerAttribute.moveVelocity * 0.8f, playerRigidbody2D.velocity.y);
    }



    /// <summary>
    /// 将手部动画覆盖
    /// 使得武器可见
    /// </summary>
    public void PlayerHoldWeapon(bool setkey)
    {
        if (setkey && playerAttribute.canHoldGun)
        {
            playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Hand Layer"), 1);//更改手部动画层权重
            playerAnimator.SetBool("isHoldWeapon", true);
            weapon.SetActive(true);

            playerAnimation.switchHand(true);
        }
        else
        {
            playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Hand Layer"), 0);//更改手部动画层权重
            playerAnimator.SetBool("isHoldWeapon", false);
            weapon.SetActive(false);

            playerAnimation.switchHand(false);
        }
    }
    /// <summary>
    /// 无参数版本，在两种状态(是否持枪)下切换
    /// </summary>
    public void PlayerHoldWeapon()
    {
        if (playerAnimator.GetBool("isHoldWeapon"))
        {
            PlayerHoldWeapon(false);
        }
        else if(playerAttribute.canHoldGun)
        {
            PlayerHoldWeapon(true);
        }

    }
    public void PlayerWeaponDir(GunDirMode dirMode)
    {
        if (ChangeGunDirEvent!=null)
        {
            ChangeGunDirEvent(dirMode);
        }
        switch (dirMode)
        {
            case GunDirMode.front:
                playerAnimator.SetBool("gunFront", true);
                playerAnimator.SetBool("gunUp", false);
                playerAnimator.SetBool("gunObliqueUp", false);
                break;
            case GunDirMode.up:
                playerAnimator.SetBool("gunFront", false);
                playerAnimator.SetBool("gunUp", true);
                playerAnimator.SetBool("gunObliqueUp", false);
                break;
            case GunDirMode.ObliqueUp:
                playerAnimator.SetBool("gunFront", false);
                playerAnimator.SetBool("gunUp", false);
                playerAnimator.SetBool("gunObliqueUp", true);
                break;
            default:
                break;
        }
    }
    public enum GunDirMode
    {
        front,
        up,
        ObliqueUp,
    }
    public delegate void ChangeGunDirDelegate(GunDirMode gundirmode);
    public ChangeGunDirDelegate ChangeGunDirEvent;

    public void WeaponFire()
    {
        if (WeaponFireEvent!=null)
        {
            WeaponFireEvent();
        }
    }
    public void WeaponStoragePower()
    {
        if (WeaponStoragePowerEvent!=null)
        {
            WeaponStoragePowerEvent();
        }
    }
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate WeaponFireEvent;
    public delegate void WeaponStoragePowerDelegate();
    public WeaponStoragePowerDelegate WeaponStoragePowerEvent;


    /// <summary>
    /// 能被拉的物体实现的接口
    /// </summary>
    private ICouldBePull pullInterface;
    public void GrapeSomething()
    {
        if (playerAnimator.GetBool("isHoldWeapon") || playerAnimator.GetBool("isCrouched"))
            return;

        RaycastHit2D hit;
        if (this.transform.lossyScale.x > 0)
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, -1), Vector2.right, 9f, interactiveLayerMask);//调整方向
            Debug.DrawLine(transform.position + new Vector3(0, -1), new Vector2(transform.position.x + 9f, transform.position.y - 1), Color.green, 1f);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, -1), Vector2.left, 9f, interactiveLayerMask);//调整方向
            Debug.DrawLine(transform.position + new Vector3(0, -1), new Vector2(transform.position.x - 9f, transform.position.y - 1), Color.green, 1f);
        }
        if (hit.collider != null)
        {
            pullInterface = hit.collider.GetComponent<ICouldBePull>();
            if (pullInterface != null)
            {
                pullInterface.BeGraped(this.gameObject);

                playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Hand Layer"), 1);//更改手部动画层权重
                playerAnimator.SetBool("isPulling", true);
                playerAnimation.switchHand(true);
                //上面动画相关
            }
            else
            {
                Debug.Log("此物体无法拖动(可能是未实现ICouldBePull)");
            }
        }
    }
    public void ReleaseSomething()
    {
        if (playerAnimator.GetBool("isHoldWeapon") || playerAnimator.GetBool("isCrouched"))
            return;
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Hand Layer"), 0);//更改手部动画层权重
        playerAnimator.SetBool("isPulling", false);
        playerAnimation.switchHand(false);
        //上面动画相关

        if (pullInterface != null)
        {
            pullInterface.BeReleased();
            pullInterface = null;
        }

    }



    /// <summary>
    ///检查是否在地面
    /// </summary>
    /// <returns></returns>
    private bool isOntheGround()
	{
		float deep = 18f;
		Vector2 position1, position2;
		Vector2 position = this.transform.position;


        position1 = new Vector2 (position.x - 3.5f, position.y);
		position2 = new Vector2 (position.x + 3.5f, position.y);

		RaycastHit2D 	
		hit1 = Physics2D.Raycast(position1, Vector2.down, deep, groundLayerMask | interactiveLayerMask),
		hit2 = Physics2D.Raycast (position2, Vector2.down, deep, groundLayerMask | interactiveLayerMask);

        Debug.DrawLine( position1, new Vector2(position1.x,position1.y-deep),Color.yellow,1);
		Debug.DrawLine( position2, new Vector2(position2.x,position2.y-deep),Color.yellow,1);

		return (hit1.collider != null || hit2.collider != null);
	}

    /// <summary>
    /// 能不能站起来，检查头上有没有墙壁
    /// </summary>
    /// <returns></returns>
    private bool canStandUp()
    {
        float deep = 9f;
        Vector2 position1;
        Vector2 position = this.transform.position;

        position1 = new Vector2(position.x, position.y);

        RaycastHit2D
        hit1 = Physics2D.Raycast(position1, Vector2.up, deep, groundLayerMask | interactiveLayerMask);

        Debug.DrawLine(position1, new Vector2(position1.x, position1.y + deep), Color.blue, 1);

        return (hit1.collider == null);
    }


	enum FaceMode
	{
		headFace,
		bodyFace,
		footFace
	}

	private bool isFaceWall(FaceMode faceMode,bool isFaceLeft)
	{
		float deep=8f;
		Vector2 startPosition = new Vector2(),
					position = this.transform.position;

		switch (faceMode) 
		{
		case FaceMode.bodyFace:
			startPosition = new Vector2(position.x, position.y - 1.5f);	// new Vector2 (position.x, position.y);
			break;
		case FaceMode.headFace:
			startPosition = new Vector2 (position.x, position.y + 8f);
			break;
		case FaceMode.footFace:
			startPosition = new Vector2 (position.x, position.y - 15.5f);
			break;
		}

		if (!isFaceLeft)
		{
			RaycastHit2D hit = Physics2D.Raycast (startPosition, Vector2.right, deep, groundLayerMask | interactiveLayerMask);
			Debug.DrawLine (startPosition, new Vector2 (startPosition.x+deep, startPosition.y), Color.red );
			return hit.collider == null;
		}
		else
		{
			RaycastHit2D hit = Physics2D.Raycast (startPosition, Vector2.left, deep, groundLayerMask | interactiveLayerMask);
			Debug.DrawLine (startPosition, new Vector2 (startPosition.x-deep, startPosition.y), Color.red );
			return hit.collider == null;
		}
	}

	void Update ()
	{

        //动画相关
		float velocityX = playerRigidbody2D.velocity.x;
		playerAnimator.SetFloat ("horizontalSpeed", Mathf.Abs (velocityX));
		playerAnimator.SetFloat ("verticalSpeed", playerRigidbody2D.velocity.y);


        /*
         * 处理翻转
         * 当不在拉东西的时候就能翻转
         */
        if (playerAnimator.GetBool("isPulling") != true)
        {
            if (velocityX < -0.1)
                player.transform.localScale = new Vector3(-1, 1, 1);
            else if (velocityX > 0.1)
                player.transform.localScale = new Vector3(1, 1, 1);
        }
	}


}
