using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_pointer : MonoBehaviour
{
    //通常時の床と選択時のマテリアル
    [SerializeField] Material[] blockmaterials = new Material[2];

    [SerializeField] GameObject cubeObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //それぞれのブロックにあるEventTroggerから呼び出される

    
}
