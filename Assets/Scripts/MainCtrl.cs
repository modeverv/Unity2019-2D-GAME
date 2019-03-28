using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCtrl : MonoBehaviour
{
    enum GAMEMODE
    {
        TITLE,
        PLAY,
        DEMO,
        END
    }

    GAMEMODE nowMode; //現在のゲームモード

    public Transform titleInfoGroup;
    public Player player;
    public Transform goalMaker;
    public Transform endInfoGroup;
    public Transform esaGroup;

    // Start is called before the first frame update
    void Start()
    {
        nowMode = GAMEMODE.TITLE;
        titleInfoGroup.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        switch(nowMode)
        {
            case GAMEMODE.TITLE:
                // タイトルモードの時
                if (Input.GetButtonDown("Jump"))
                {
                    nowMode = GAMEMODE.PLAY;
                    //タイトルロゴ消す
                    titleInfoGroup.gameObject.SetActive(false);
                    //player動かす
                    player.isStop = false;
                }
                break;
            case GAMEMODE.PLAY:
                if(player.transform.position.x > goalMaker.position.x)
                {
                    player.isStop = true;
                    if(player.transform.position.y < goalMaker.position.y)
                    {
                        nowMode = GAMEMODE.DEMO;
                        endInfoGroup.gameObject.SetActive(true);
                        player.DemoStart();
                    }
                }
                break;
            case GAMEMODE.DEMO:　
                nowMode = GAMEMODE.END;
                break;
            case GAMEMODE.END:
                // タイトルモードの時
                if (Input.GetButtonDown("Jump"))
                {
                    nowMode = GAMEMODE.TITLE;
                    titleInfoGroup.gameObject.SetActive(true);
                    endInfoGroup.gameObject.SetActive(false);
                    for(int i = 0, l = esaGroup.childCount; i < l; i++)
                    {
                        esaGroup.GetChild(i).gameObject.SetActive(true);
                    }
                    player.Reset();
                }
                break;
        }
    }
}
