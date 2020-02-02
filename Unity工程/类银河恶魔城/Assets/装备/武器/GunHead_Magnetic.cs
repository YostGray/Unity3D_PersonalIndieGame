using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHead_Magnetic : MonoBehaviour
{
    private PlayerActions playeractions;
    private PlayerAttribute playerAttribute;
    private Animator animator;
    private SpriteRenderer thisRender;

    [SerializeField]
    private Sprite[] front, up, obliqueUp;

    private PlayerActions.GunDirMode dirMode;
    private GunBody gunbody;

    private bool isPulling = false;
    private CircleCollider2D circleCollider2D;

    void Awake()
    {
        playerAttribute = this.GetComponentInParent<PlayerAttribute>();
        playeractions = this.GetComponentInParent<PlayerActions>();
        animator = this.GetComponent<Animator>();
        thisRender = this.GetComponent<SpriteRenderer>();
        gunbody = this.GetComponentInParent<GunBody>();

        circleCollider2D = this.GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        playeractions.ChangeGunDirEvent += ChangeDir;
        playeractions.WeaponFireEvent += Fire;
        playeractions.WeaponStoragePowerEvent += StoragePower;
    }

    void OnDisable()
    {
        playeractions.ChangeGunDirEvent -= ChangeDir;
        playeractions.WeaponFireEvent -= Fire;
        playeractions.WeaponStoragePowerEvent -= StoragePower;
    }

    private int scaleNum = 20;
    private Vector2[] MagnetPositons = { new Vector2(10,-4), new Vector2(7,4), new Vector2(-2,8) };//前 斜上 上
    private float distance=100f;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (isPulling && collider.tag=="ironBlock")  
        {
            Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
            Vector2 colliderPosition = collider.transform.position,
                thisPosition = transform.position;
            switch (dirMode)
            {
                case PlayerActions.GunDirMode.front:
                    Debug.DrawLine(thisPosition + MagnetPositons[0] * transform.lossyScale.x, colliderPosition);
                    rigidbody.velocity = scaleNum * (thisPosition + MagnetPositons[0] * transform.lossyScale.x - colliderPosition).normalized;
                    break;
                case PlayerActions.GunDirMode.up:
                    Debug.DrawLine(thisPosition + MagnetPositons[2] * transform.lossyScale.x, colliderPosition);
                    rigidbody.velocity = scaleNum * (thisPosition + MagnetPositons[2] * transform.lossyScale.x - colliderPosition).normalized;
                    break;
                case PlayerActions.GunDirMode.ObliqueUp:
                    Debug.DrawLine(thisPosition + MagnetPositons[1] * transform.lossyScale.x, colliderPosition);
                    rigidbody.velocity = scaleNum * (thisPosition + MagnetPositons[1] * transform.lossyScale.x - colliderPosition).normalized;
                    break;
                default:
                    break;
            }
        }
    }

    void Fire()
    {
        isPulling = false;
        StopCoroutine("AddPower");
        Debug.Log("磁力枪 威力:" + gunbody.Power);
        gunbody.SetPower(0);
        animator.speed = 1;
    }

    void StoragePower()
    {
        StartCoroutine("AddPower");
        isPulling = true;
    }

    IEnumerator AddPower()
    {
        for (int i = 0; i < playerAttribute.gunPowerLimit; i++)
        {
            yield return new WaitForSeconds(0.5f);
            gunbody.AddPower();
            animator.speed = gunbody.Power/2 + 1;
        }
    }

    private void ChangeDir(PlayerActions.GunDirMode gundirmode)
    {
        dirMode = gundirmode;
        switch (dirMode)
        {
            case PlayerActions.GunDirMode.front:
                circleCollider2D.offset = MagnetPositons[0];
                break;
            case PlayerActions.GunDirMode.up:
                circleCollider2D.offset = MagnetPositons[2];
                break;
            case PlayerActions.GunDirMode.ObliqueUp:
                circleCollider2D.offset = MagnetPositons[1];
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 在动画中改变样子
    /// </summary>
    /// <param name="num"></param>
    private void ChangeSprite(int num)
    {
        switch (dirMode)
        {
            case PlayerActions.GunDirMode.front:
                thisRender.sprite = front[num-1];
                break;
            case PlayerActions.GunDirMode.up:
                thisRender.sprite = up[num-1];
                break;
            case PlayerActions.GunDirMode.ObliqueUp:
                thisRender.sprite = obliqueUp[num-1];
                break;
            default:
                break;
        }
    }
}
