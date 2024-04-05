using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Muks.WeightedRandom;
using System;

public class TestScene : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UITestScene _uiTestScene;

    [SerializeField] private RandomStruct[] _randomStruct;

    private WeightedRandom<string> _weightedRandom;

    public event Action<string> OnGetRandomHandler;


    private void Start()
    {
        _weightedRandom = new WeightedRandom<string>();

        for(int i = 0, count = _randomStruct.Length; i < count; i++)
        {
            _weightedRandom.Add(_randomStruct[i].Name, _randomStruct[i].Weight);
        }

        _uiTestScene.Init(this);
    }


    public string GetRandom()
    {
        string item = _weightedRandom.GetRamdomItemAfterSub();

        OnGetRandomHandler?.Invoke(item);
        return item;
    }


    public WeightedRandom<string> GetWeightedRandomClass()
    {
        return _weightedRandom;
    }

}
