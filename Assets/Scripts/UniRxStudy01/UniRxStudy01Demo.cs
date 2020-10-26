using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

//MVP模式
//ctrl层
public class UniRxStudy01Demo : MonoBehaviour
{
    //当前model层
    private EnemyModel mEnemyModel;
    //demo Btn
    public Button demoBtn;
    //demo Text
    public Text demoText;

    //Merge操作
    private Button mergeBtn01;
    private Button mergeBtn02;
    private Button mergeBtn03;

    // Start is called before the first frame update
    void Start()
    {
        //赋值  这个操作还是觉得不妥
        mEnemyModel = new EnemyModel(100);
        
        demoBtn.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("按钮点击");
            mEnemyModel.HP.Value -= 99;
        });

        //通知Text更新
        mEnemyModel.HP.SubscribeToText(demoText);
        //通知按钮隐藏
        mEnemyModel.isInteractable
            .Where(isIn => isIn)
            .Select(nteractable => !nteractable)
            .Subscribe(isD => {
                Debug.LogError($"SELECT值 = {isD}");
                demoBtn.interactable = isD;
            });

        //Merge使用
        mergeBtn01 = this.transform.Find("mergeBtn01").GetComponent<Button>();
        mergeBtn02 = this.transform.Find("mergeBtn02").GetComponent<Button>();
        mergeBtn03 = this.transform.Find("mergeBtn03").GetComponent<Button>();
        
        var mergeBtn01Obs = mergeBtn01.OnClickAsObservable().Select(_=>111);
        var mergeBtn02Obs = mergeBtn02.OnClickAsObservable().Select(_ =>222);
        var mergeBtn03Obs = mergeBtn03.OnClickAsObservable().Select(_ =>333);

        Observable.Merge(mergeBtn01Obs,mergeBtn02Obs,mergeBtn03Obs)
            .Subscribe(isOn => {
                Debug.LogError($"被点击了 = {isOn}");
            });
    }
}

//model层
public class EnemyModel
{
    //是否隐藏按钮
    public ReadOnlyReactiveProperty<bool> isInteractable { get; }

    //当前血量
    public IntReactiveProperty HP { get; set; }

    public EnemyModel(int hp)
    {
        //初始化 
        HP = new IntReactiveProperty(hp);
        isInteractable = HP.Select(HP => HP <= 0).ToReadOnlyReactiveProperty();
    }
}
