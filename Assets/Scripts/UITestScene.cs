using Muks.WeightedRandom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestScene : MonoBehaviour
{
    [SerializeField] private Text _getItemText;
    [SerializeField] private Text _remainingItemsText;
    [SerializeField] private Button _choiceButton;


    private TestScene _testScene;
    private Dictionary<string, int> _itemDic;

    public void Init(TestScene testScene)
    {
        _testScene = testScene;
        WeightedRandom<string> random = _testScene.GetWeightedRandomClass();
        _itemDic = random.GetList();

        _choiceButton.onClick.AddListener(OnChoiceButtonClicked);
        _testScene.OnGetRandomHandler += UpdateUI;

        _remainingItemsText.text = string.Empty;
        foreach (string key in _itemDic.Keys)
        {
            int value = _itemDic[key];
            _remainingItemsText.text += key + ": " + value + "\n";
        }
    }


    private void UpdateUI(string item)
    {
        _getItemText.text = item + " Get!";
        _remainingItemsText.text = string.Empty;

        foreach(string key in _itemDic.Keys)
        {
            int value = _itemDic[key];
            _remainingItemsText.text += key + ": " + value + "\n";
        }
    }


    private void OnChoiceButtonClicked()
    {
        if (_itemDic.Count <= 0)
            return;

        _testScene.GetRandom();
    }
}
