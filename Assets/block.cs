using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    //生成させることが出来るか（上にブロックがある場合false）
    private bool pointer;
    //通常時の床と選択時のマテリアル
    [SerializeField] Material[] blockmaterials = new Material[3];
    //プレハブのブロック
    [SerializeField] GameObject cubeObject;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer= GetComponent<MeshRenderer>();
        pointer= true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ポインターが入ると呼ばれる（色を選択状態に変える）
    public void OnPointerEnter(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        Debug.Log(pointerObject.gameObject.name);
        Debug.Log(pointer);
        //上に置ける場合は青、置けない場合は赤
        if(pointer)m_Renderer.material = blockmaterials[1];
        else m_Renderer.material = blockmaterials[2];
    }

    //ポインターが出ていくときに呼ばれる（色を戻す）
    public void OnPointerExit(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        m_Renderer.material = blockmaterials[0];
    }

    public void OnPointerClick(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerClick;
        pointer= false;
        m_Renderer.material = blockmaterials[2];
        Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
        
    }
}
