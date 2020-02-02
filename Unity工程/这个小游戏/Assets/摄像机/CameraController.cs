using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    CameraMode cameraMode;

    private float PlayerX, PlayerY,thisX,thisY;
    private Transform playerTransform;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    private float  CameraSpeed=0.1f,maxDistance=10f;

    [SerializeField]
    private float BoderH = 32, BoderW = 16;


    void Awake()
    {
        intinal(player);
    }

    /// <summary>
    /// 初始化摄像机位置到GameObject
    /// </summary>
    public void intinal(GameObject playerGameObject)
    {
        player = playerGameObject;
        playerTransform = player.GetComponent<Transform>();
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);

        if (cameraMode == CameraMode.simpleChild)
        {
            this.transform.parent = player.transform;
        }
    }


    enum CameraMode
    {
        simpleChild,
        smoothFollow,
        withBoard,
    }

    private Vector3 target;
    void Update () 
    {
        switch (cameraMode)
        {
            case CameraMode.simpleChild:
                return;
            case CameraMode.smoothFollow:
                PlayerX = playerTransform.position.x;
                PlayerY = playerTransform.position.y;

                target = new Vector3(PlayerX, PlayerY, -10);
                //transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * CameraSpeed);

                if (Vector2.Distance(playerTransform.position, transform.position) >= maxDistance)
                {
                    target = new Vector3(PlayerX, PlayerY, -10);
                    float Speed = Vector2.Distance(transform.position, target) * CameraSpeed;
                    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
                }
                break;
            case CameraMode.withBoard:
                PlayerX = playerTransform.position.x;
                PlayerY = playerTransform.position.y;

                thisX = this.transform.position.x;
                thisY = this.transform.position.y;
                if (PlayerY - thisY > BoderH)
                {
                    transform.position = new Vector3(thisX, thisY + (PlayerY - thisY - BoderH), -10);
                }
                else if (PlayerY - thisY < -BoderH)
                {
                    transform.position = new Vector3(thisX, thisY + (PlayerY - thisY + BoderH), -10);
                }

                if (PlayerX - thisX > BoderW)
                {
                    transform.position = new Vector3(thisX + (PlayerX - thisX - BoderW), transform.position.y, -10);
                }
                else if (PlayerX - thisX < -BoderW)
                {
                    transform.position = new Vector3(thisX + (PlayerX - thisX + BoderW), transform.position.y, -10);
                }
                break;
            default:
                break;
        }


        //PlayerX = playerTransform.position.x;
        //PlayerY = playerTransform.position.y;

        //thisX = this.transform.position.x;
        //thisY = this.transform.position.y;

        //if (Vector2.Distance(playerTransform.position, transform.position) >= maxDistance)
        //{
        //    target = new Vector3(PlayerX, PlayerY, -10);
        //    float Speed = Vector2.Distance(transform.position, target) * CameraSpeed;
        //    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
        //}

        //switch (cameraMode)
        //{
        //    case wittBoard:
        //        if (PlayerY - thisY > BoderH)
        //        {
        //            transform.position = new Vector3(thisX, thisY + (PlayerY - thisY - BoderH), -10);
        //        }
        //        else if (PlayerY - thisY < -BoderH)
        //        {
        //            transform.position = new Vector3(thisX, thisY + (PlayerY - thisY + BoderH), -10);
        //        }

        //        if (PlayerX > thisX)
        //        {
        //            transform.position = new Vector3(PlayerX, transform.position.y, -10);
        //        }
        //        else if (PlayerX - thisX < -BoderW)
        //        {
        //            transform.position = new Vector3(thisX + (PlayerX - thisX + BoderW), transform.position.y, -10);
        //        }
        //        break;
        //    case 2:
        //        transform.position = new Vector3(PlayerX, PlayerY, -10);
        //        break;
        //    case 3:
        //        target = new Vector3(PlayerX, PlayerY, -10);
        //        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * CameraSpeed);
        //        break;
        //    case 4:
        //        target = new Vector3(PlayerX, PlayerY, -10);
        //        float Speed = Mathf.Pow(Vector2.Distance(transform.position, target), 2) * CameraSpeed;
        //        float Speed = Vector2.Distance(transform.position, target) * CameraSpeed;
        //        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Speed);
        //        break;
        //}

    }

}
