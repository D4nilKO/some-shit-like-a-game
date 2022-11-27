using System;
using System.Collections.Generic;
using UnityEngine;

//http://websketches.ru/blog/2d-igra-na-unity-podrobnoye-rukovodstvo-p4

public class DanyaBg : MonoBehaviour
{
    private GameObject gamer;
    [SerializeField] private GameObject bG;
    private GameObject parrentPoint;

    private float backgroundOriginalSizeX;
    private float backgroundOriginalSizeY;

    private Transform playerPos;
    private float playerPosXTrue;
    private float playerPosYTrue;
    private int playerPosX;
    private int playerPosY;
    private int counterX;
    private int counterY;
    [SerializeField] private List<Transform> backgroundPart;
    [SerializeField] private int numberOfTilesSide;
    private int n;

    private void Start()
    {
        parrentPoint = gameObject;
        gamer = Player.playerGameObject;
        n = numberOfTilesSide;

        //gamer = GameObject.FindWithTag("Player"); ?????????????????? ????? ? ????? ??? ???? ???? ???????
        //_bG = GameObject.FindWithTag("backGround");


        var sr = bG.GetComponent<SpriteRenderer>();
        var originalSize = sr.size;
        var lossyScale = bG.transform.lossyScale;
        backgroundOriginalSizeX = originalSize.x * lossyScale.x;
        backgroundOriginalSizeY = originalSize.y * lossyScale.y;

        // for (int i = (int)backgroundOriginalSizeY;
        //      i >= -(int)backgroundOriginalSizeY;
        //      i -= (int)backgroundOriginalSizeY)
        // {
        //     var v31 = new Vector3(-backgroundOriginalSizeX, i, 5);
        //     var v32 = new Vector3(0, i, 5);
        //     var v33 = new Vector3(backgroundOriginalSizeX, i, 5);
        //     Instantiate(bG, v31, Quaternion.identity, parrentPoint.transform);
        //     Instantiate(bG, v32, Quaternion.identity, parrentPoint.transform);
        //     Instantiate(bG, v33, Quaternion.identity, parrentPoint.transform);
        // }

        // print(n);
        // print((n - 1) / 2f);
        // print(-(n - 1) / 2f);

        for (var i = (n - 1) / 2f; i >= (-(n - 1) / 2f); i -= 1)
        {
            for (var j = (-(n - 1)) / 2f; j <= (n - 1) / 2f; j += 1)
            {
                var pos = new Vector3(j * backgroundOriginalSizeX, i * backgroundOriginalSizeY, 5);
                Instantiate(bG, pos, Quaternion.identity, parrentPoint.transform);
            }
        }

        backgroundPart = new List<Transform>();
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            backgroundPart.Add(child);
        }
    }


    private void Update()
    {
        // ???????? ????, ??? ???????? ??????? ???? ???????
        if (Math.Abs(gamer.transform.position.y - playerPosYTrue) > 1)
        {
            playerPosYTrue = gamer.transform.position.y;
            playerPosY = (int)playerPosYTrue / ((int)backgroundOriginalSizeY);
            GroundShift1();
        }

        if ((Math.Abs(gamer.transform.position.x - playerPosXTrue) > 1))
        {
            playerPosXTrue = gamer.transform.position.x;
            playerPosX = (int)playerPosXTrue / ((int)backgroundOriginalSizeX);
            GroundShift1();
        }
    }

    private void GroundShift1()
    {
        if (playerPosY > counterY)
        {
            Up();
        }

        if (playerPosY < counterY)
        {
            Down();
        }

        if (playerPosX > counterX)
        {
            Right();
        }

        if (playerPosX < counterX)
        {
            Left();
        }
    }

    private void Up()
    {
        var start = n * n - n;
        var end = n * n - 1;
        for (int i = start; i <= end; i++)
        {
            var curBg = backgroundPart[i];
            var position = curBg.position;
            position = new Vector3(position.x, position.y + (backgroundOriginalSizeY * n),
                position.z);
            curBg.position = position;
            backgroundPart.Move(i, i - start);
        }

        counterY++;
    }

    private void Down()
    {
        var start = n - 1;
        var end = 0;
        for (int i = start; i >= end; i--)
        {
            var curBg = backgroundPart[i];
            var position = curBg.position;
            position = new Vector3(position.x, position.y - (backgroundOriginalSizeY * n),
                position.z);
            curBg.position = position;
            backgroundPart.Move(i, i - n + n * n);
            //print(-n + n * n);
        }

        counterY--;
    }

    private void Right()
    {
        var start = 0;
        var end = n * n - n;
        for (int i = start; i <= end; i += n)
        {
            var curBg = backgroundPart[i];
            var position = curBg.position;
            position = new Vector3(position.x + (backgroundOriginalSizeX * n), position.y,
                position.z);
            curBg.position = position;
            backgroundPart.Move(i, i + n - 1);
        }

        counterX++;
    }

    private void Left()
    {
        var start = n * n - 1;
        var end = n - 1;
        //print(start + " - " + end);
        for (int i = start; i >= end; i -= n)
        {
            //print(i);
            var curBg = backgroundPart[i];
            var position = curBg.position;
            position = new Vector3(position.x - (backgroundOriginalSizeX * n), position.y,
                position.z);
            curBg.position = position;
            backgroundPart.Move(i, i - n + 1);
        }

        counterX--;
    }

    // private void GroundShift()
    // {
    //     //UP
    //     if (playerPosY > counterY)
    //     {
    //         for (int i = 6; i <= 8; i++)
    //         {
    //             var curBg = backgroundPart[i];
    //             curBg.position = new Vector3(curBg.position.x, curBg.position.y + (backgroundOriginalSizeY * n),
    //                 curBg.position.z);
    //             backgroundPart.Move(i, i - 6);
    //         }
    //
    //         counterY++;
    //     }
    //
    //     //DOWN
    //     if (playerPosY < counterY)
    //     {
    //         for (int i = 2; i >= 0; i--)
    //         {
    //             var curBg = backgroundPart[i];
    //             curBg.position = new Vector3(curBg.position.x, curBg.position.y - (backgroundOriginalSizeY * 3),
    //                 curBg.position.z);
    //             backgroundPart.Move(i, i + 6);
    //         }
    //
    //         counterY--;
    //     }
    //
    //     //Right
    //     if (playerPosX > counterX)
    //     {
    //         for (int i = 0; i <= 6; i = i + 3)
    //         {
    //             var curBg = backgroundPart[i];
    //             curBg.position = new Vector3(curBg.position.x + (backgroundOriginalSizeX * 3), curBg.position.y,
    //                 curBg.position.z);
    //             backgroundPart.Move(i, i + 2);
    //         }
    //
    //         counterX++;
    //     }
    //
    //     //Left
    //     if (playerPosX < counterX)
    //     {
    //         for (int i = 8; i >= 2; i = i - 3)
    //         {
    //             var curBg = backgroundPart[i];
    //             curBg.position = new Vector3(curBg.position.x - (backgroundOriginalSizeX * 3), curBg.position.y,
    //                 curBg.position.z);
    //             backgroundPart.Move(i, i - 2);
    //         }
    //
    //         counterX--;
    //     }
    // }
}