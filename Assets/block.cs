using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    private Animator anim;
    //生成させることが出来るか（上にブロックがある場合false）
    [SerializeField] private bool pointer = true;
    //通常時の床と選択時のマテリアル
    [SerializeField] Material[] blockmaterials = new Material[3];
    //プレハブのブロック
    [SerializeField] GameObject cubeObject;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer= GetComponent<MeshRenderer>();
        anim = GetComponentInChildren<Animator>();
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
        if (pointer)
        {
            GameObject pointerObject = (data as PointerEventData).pointerClick;

            switch ((data as PointerEventData).pointerId)
            {
                case -1:
                    Debug.Log("Left Click");
                    Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
                    m_Renderer.material = blockmaterials[2];
                    GameManager.I.Block_move=true;
                    pointer = false;
                    break;
                case -2:
                    Debug.Log("Right Click");
                    Ray ray = new Ray(transform.position, -transform.up);
                    Debug.DrawRay(transform.position, -transform.up * 1.0f, Color.red);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1.0f))
                    {
                        Debug.Log(hit.collider.gameObject);
                        anim.SetTrigger("down");
                        GameManager.I.Block_move = true;
                        hit.collider.gameObject.SetActive(false);
                    }
                    break;
                case -3:
                    Debug.Log("Middle Click");
                    break;
            }
            
        }
    }

    //計算の誤差で正常な位置に戻らないことがあるためアニメーション終了時に一応修正
    IEnumerator adjust()
    {
        yield return new WaitForSeconds(0.02f);
        GameManager.I.Block_move = false;
        gameObject.transform.position=new Vector3(transform.position.x, Mathf.Round(transform.position.y*100.0f)/100.0f,transform.position.z);
        Debug.Log(transform.position);
    }
}
