﻿using UnityEngine;
using System.Collections;

public class BarrelCollisionScript : Photon.MonoBehaviour
{
    public string[] BulletPrefabs;
    public Texture2D BarFrontTexture;
    public Texture2D BuletIcon;
    public string BarBackgroundTexture;
    public GameObject DestroyedPrefab;

    private bool __local;
    public void SetLocal(bool local) { __local = local; }


    void Start()
    {
        if (GameController.instance.gameType == GameType.NETWORK)
        {
            UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().SpawnBarrelEnd(this.gameObject);
        }
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!__local) return;

        if (collision.gameObject.tag == "Bullet")
        {
            if (GameController.instance.gameType == GameType.NETWORK)
            {
                SendEventHitBarrel();
            }
            else
            {
                ReceiveEventHitBarrel();
            }
        }
    }

    [RPC]
    protected void ReceiveEventHitBarrel()
    {
        Debug.Log("RPC: ReceiveEventHitBarrel");
        UIGlobalVariablesScript.Singleton.GunGameScene.GetComponent<GunsMinigameScript>().OnBulletHitBarrel(this);
    }

    protected void SendEventHitBarrel()
    {
        GetComponent<PhotonView>().RPC("ReceiveEventHitBarrel", PhotonTargets.All);
    }
}
