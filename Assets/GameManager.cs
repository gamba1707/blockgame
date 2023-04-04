using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("ブロックの状態")]
    [SerializeField] private bool block_move;

    

    //ゲーム開始直後に処理を行う
    private void Awake()
    {
        //変数Iがnullならば
        if (I == null)
        {
            //Iに自身（GameManager）を代入
            I = this;
        }
    }

    public bool Block_move // プロパティ
    {
        get { return block_move; }  // 通称ゲッター。呼び出した側がscoreを参照できる
        set { block_move = value; } // 通称セッター。value はセットする側の数字などを反映する
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
