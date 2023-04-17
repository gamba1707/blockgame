using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class block : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    private Animator anim;
    //自分にポインターが当たっているか
    [SerializeField] private bool onpointer=false;
    //生成させることが出来るか（上にブロックがある場合false）
    [SerializeField] private bool addblock = true;
    //通常時の床と選択時のマテリアル(灰色、青、赤)
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
        onpointer= true;
        Debug.Log(pointerObject.gameObject.name);
        Debug.Log(addblock);
        //上に置ける場合は青、置けない場合は赤
        if(addblock) m_Renderer.material = blockmaterials[1];
        else m_Renderer.material = blockmaterials[2];
    }

    //ポインターが出ていくときに呼ばれる（色を戻す）
    public void OnPointerExit(BaseEventData data)
    {
        GameObject pointerObject = (data as PointerEventData).pointerEnter;
        onpointer= false;
        m_Renderer.material = blockmaterials[0];
    }

    //物体に当たっているときにマウスのボタンを押した
    public void OnPointerClick(BaseEventData data)
    {
        //箱を出現させることが出来る箱である＆＆ブロックが動いている途中ではない
        if (addblock && !GameManager.I.Block_move)
        {
            GameObject pointerObject = (data as PointerEventData).pointerClick;

            switch ((data as PointerEventData).pointerId)
            {
                case -1:
                    //左クリック処理
                    Debug.Log("Left Click");
                    Instantiate(cubeObject, pointerObject.transform.position, Quaternion.identity);
                    m_Renderer.material = blockmaterials[2];
                    GameManager.I.Block_move=true;//マネージャーにブロックが動いている最中だと知らせる
                    addblock = false;
                    break;
                case -2:
                    //右クリック処理
                    Debug.Log("Right Click");
                    Ray ray = new Ray(transform.position, -transform.up);
                    Debug.DrawRay(transform.position, -transform.up * 1.0f, Color.red);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1.0f))
                    {
                        Debug.Log("当たったobj:"+hit.collider.gameObject+"タグ："+hit.collider.tag);
                        anim.SetTrigger("down");//下がるモーション再生
                        GameManager.I.Block_move = true;//マネージャーにブロックが動いている最中だと知らせる
                        //触れたものがcubeのタグを持っているものならオブジェクトを非表示にする
                        if (hit.collider.transform.parent.gameObject.CompareTag("cube"))
                        {
                            hit.collider.transform.parent.gameObject.SetActive(false);
                        }else if (hit.collider.gameObject.CompareTag("cube"))
                        {
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                    break;
                case -3:
                    Debug.Log("Middle Click");
                    break;
            }
            
        }
    }

    //計算の誤差で正常な位置に戻らないことがあるためアニメーション終了時に呼び出し一応修正
    IEnumerator adjust()
    {
        yield return new WaitForSeconds(0.02f);
        GameManager.I.Block_move = false;//ブロックが動いていない（生成してもいい）
        float y = Mathf.Round(transform.position.y / 1.5f);//1.5の倍数の位置に必ず生成されるので1.5＊？を計算
        //少しのずれなので多少強引でもずれを解消
        gameObject.transform.position=new Vector3(transform.position.x, y*1.5f,transform.position.z);
        
        Debug.Log("修正位置："+transform.position);
    }

    public void OnPlayerEnter()
    {
        Debug.Log("false: "+this.gameObject.name);
        addblock = false;
        if(onpointer) m_Renderer.material = blockmaterials[2];
    }
    public void OnPlayerExit()
    {
        Debug.Log("true: " + this.gameObject.name);
        addblock = true;
        if (onpointer) m_Renderer.material = blockmaterials[1]; 
    }
}
