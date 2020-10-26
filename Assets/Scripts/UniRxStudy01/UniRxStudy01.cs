using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

public class UniRxStudy01 : MonoBehaviour
{
    public Button testButton;

    //ReactiveProperty响应式属性 : 可以代替一些变量   IntReactiveProperty 比 ReactiveProperty<int>泛型更容易序列化
    public IntReactiveProperty mReactiveProperty = new IntReactiveProperty();

    // Start is called before the first frame update
    void Start()
    {

        mReactiveProperty.Subscribe(_ =>
        {
            Debug.LogError("进行反应性属性调用:" + mReactiveProperty.Value);

        })
        ;

        mReactiveProperty.Value = 111;

        //模拟按钮点击事件
        testButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                Debug.LogError("按钮点击");
            })
            .AddTo(this);

        //模拟Update
        Observable.EveryUpdate()
            .Where(_ =>//过滤预计
            {
                return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
            })
            .Subscribe(_ =>//接收事件传递
            {
                Debug.LogError($"监听{this.name}");
            })
            .AddTo(this);

        Observable.Timer(TimeSpan.FromSeconds(5f))//延迟调用
            .Subscribe(_ =>
            {
                Debug.LogError("延迟调用");
            });

    }

    // Update is called once per frame
    void AAA()
    {

    }
}
