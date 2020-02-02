using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBody : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gunBodyRender, gunBodyPoweRender, gunBodyBackRender;
    [SerializeField]
    private SpriteMask powerMask;
    [SerializeField]
    private Sprite gunbodyFront, gunbodyFront_power, gunbodyFront_back,
        gunbodyUp, gunbodyUp_power, gunbodyUp_back,
        gunbodyObliqueUp, gunbodyObliqueUp_power, gunbodyObliqueUp_back;

    private PlayerActions.GunDirMode mode;
    private PlayerActions playeractions;
    private PlayerAttribute playerAttribute;


    [SerializeField]
    private GameObject[] gunHeads;
    private GameObject currentGunHead;

    public int Power { set; get; }

    void Awake()
    {
        playeractions = this.GetComponentInParent<PlayerActions>();
        playerAttribute = this.GetComponentInParent<PlayerAttribute>();
        Power = 0;

        //测试磁力枪头
        currentGunHead = Instantiate(gunHeads[0], this.transform);
    }

	void OnEnable()
    {
        playeractions.ChangeGunDirEvent += ChangeDir;
    }

    void OnDisable()
    {
        playeractions.ChangeGunDirEvent -= ChangeDir;
    }

    void ChangeDir(PlayerActions.GunDirMode gundirmode)
    {
        mode = gundirmode;
        SetPower(Power);
        switch (gundirmode)
        {
            case PlayerActions.GunDirMode.front:
                gunBodyRender.sprite = gunbodyFront;
                gunBodyPoweRender.sprite = gunbodyFront_power;
                gunBodyBackRender.sprite = gunbodyFront_back;
                powerMask.sprite = gunbodyFront_back;
                break;
            case PlayerActions.GunDirMode.up:
                gunBodyRender.sprite = gunbodyUp;
                gunBodyPoweRender.sprite = gunbodyUp_power;
                gunBodyBackRender.sprite = gunbodyUp_back;
                powerMask.sprite = gunbodyUp_back;
                break;
            case PlayerActions.GunDirMode.ObliqueUp:
                gunBodyRender.sprite = gunbodyObliqueUp;
                gunBodyPoweRender.sprite = gunbodyObliqueUp_power;
                gunBodyBackRender.sprite = gunbodyObliqueUp_back;
                powerMask.sprite = gunbodyObliqueUp_back;
                break;
            default:
                break;
        }
    }

    public void AddPower()
    {
        SetPower(Power+1);
    }

    public void SetPower(int value)
    {
        if (value> playerAttribute.gunPowerLimit)
            value = playerAttribute.gunPowerLimit;
        else if (value<0)
            value = 0;

        Power = value;
        
        switch (mode)
        {
            case PlayerActions.GunDirMode.front:
                powerMask.transform.localPosition =new Vector3( - value * 2, 0);
                break;
            case PlayerActions.GunDirMode.up:
                powerMask.transform.localPosition =  new Vector3(0,  - value * 2);
                break;
            case PlayerActions.GunDirMode.ObliqueUp:
                powerMask.transform.localPosition =  new Vector3( - value * 2,  - value * 2);
                break;
            default:
                break;
        }
    }
}
