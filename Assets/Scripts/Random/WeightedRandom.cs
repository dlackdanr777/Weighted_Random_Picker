using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Muks.WeightedRandom
{

    /// <summary>����ġ ���� �̱� �ý��� Ŭ����</summary>
    public class WeightedRandom<T>
    {
        private Dictionary<T, int> _itemDic;


        public WeightedRandom()
        {
            _itemDic = new Dictionary<T, int>();
        }


        /// <summary>����ġ ����Ʈ�� �����۰� ������ �߰���</summary>
        public void Add(T item, int value)
        {
            //������ ������ �����Ѵ�.
            if (value < 0)
            {
                Debug.LogError("������ �� �� �����ϴ�.");
                return;
            }

            if (_itemDic.ContainsKey(item))
                _itemDic[item] += value;

            else
                _itemDic.Add(item, value);
        }


        /// <summary>����ġ ����Ʈ�� �������� ������ ���� ������ ����, ���� ������ �� ũ�� ����Ʈ���� �������� ��</summary>
        public void Sub(T item, int value)
        {
            //������ ������ �����Ѵ�.
            if (value < 0)
            {
                Debug.LogError("������ �� �� �����ϴ�.");
                return;
            }

            //���� ��ųʸ��� Ű�� �����ϸ�?
            if (_itemDic.ContainsKey(item))
            {
                //Ű�� ���� ũ�Ⱑ �� ũ��?
                if (value < _itemDic[item])
                    _itemDic[item] -= value;

                else
                    Remove(item);
            }
            else
            {
                Debug.LogError("�������� �������� �ʽ��ϴ�.");
            }
        }


        /// <summary> ����Ʈ���� �������� ���� </summary>
        public void Remove(T item)
        {
            //���� ��ųʸ��� Ű�� �����ϸ�?
            if (_itemDic.ContainsKey(item))
            {
                //�ش� Ű�� �����͸� �����Ѵ�.
                _itemDic.Remove(item);
            }
            else
            {
                Debug.LogError("�������� �������� �ʽ��ϴ�.");
            }
        }


        /// <summary>���� ����Ʈ�� �ִ� �������� ����ġ�� ��� ���� ��ȯ</summary>
        public int TotalWeight()
        {
            int totalWeight = 0;

            //��ųʸ��� �Էµ� ��� ������ ����ġ ���� ���Ѵ�.
            foreach (int value in _itemDic.Values)
            {
                totalWeight += value;
            }

            return totalWeight;
        }


        /// <summary>������ ����Ʈ�� �ִ� ��� �������� ����ġ�� ������ ��ȯ�Ͽ� ��ȯ (0, 1 ����)</summary>
        public Dictionary<T, float> GetPercent()
        {
            Dictionary<T, float> _tempDic = new Dictionary<T, float>();
            float totalWeight = TotalWeight();

            foreach (var item in _itemDic)
            {
                _tempDic.Add(item.Key, item.Value / totalWeight);
            }

            return _tempDic;
        }


        /// <summary> ������ ����Ʈ���� �������� �������� �̾� ��ȯ(���� �������� ���� -1) </summary>
        public T GetRamdomItemAfterSub()
        {
            //��ųʸ��� ����ִ� ������ ������ 0���ϸ�
            if (_itemDic.Count <= 0)
            {
                Debug.LogError("����Ʈ�� �������� �����ϴ�. �̱� �Ұ���");
                return default;
            }

            //�� ����ġ�� �����´�.
            int weight = 0;
            int totalWeight = TotalWeight();

            //�� ����ġ ���� 0~1f�� ���� ���� ���� �������� ���Ѵ�.
            int pivot = Mathf.RoundToInt(totalWeight * RandomRange(0.0f, 1.0f));

            //��ųʸ��� ��ȸ�ϸ� ����ġ�� ���ϴ� ������ �̻��� �Ǹ� �� �������� ��ȯ�Ѵ�.
            foreach (var item in _itemDic)
            {
                weight += item.Value;
                if (pivot <= weight)
                {
                    
                    _itemDic[item.Key] -= 1;

                    if (_itemDic[item.Key] <= 0)
                        Remove(item.Key);

                    return item.Key;
                }
            }
            return default;
        }


        /// <summary> ������ ����Ʈ���� �������� �������� �̾� ��ȯ </summary>
        public T GetRamdomItem()
        {
            //��ųʸ��� ����ִ� ������ ������ 0���ϸ� ����
            if (_itemDic.Count <= 0)
            {
                Debug.LogError("����Ʈ�� �������� �����ϴ�. �̱� �Ұ���");
                return default;
            }

            //�� ����ġ�� �����´�.
            int totalWeight = TotalWeight();
            int weight = 0;

            //�� ����ġ ���� 0~1f�� ���� ���� ���� �������� ���Ѵ�.
            int pivot = Mathf.RoundToInt(totalWeight * RandomRange(0.0f, 1.0f));

            //��ųʸ��� ��ȸ�ϸ� ����ġ�� ���ϴ� ������ �̻��� �Ǹ� �� �������� ��ȯ�Ѵ�.
            foreach (var item in _itemDic)
            {
                weight += item.Value;
                if (pivot <= weight)
                {
                    return item.Key;
                }
            }
            return default;
        }


        /// <summary> ������ ����Ʈ�� ��ȯ </summary>
        public Dictionary<T, int> GetList()
        {
            return _itemDic;
        }


        /// <summary>RandomNumberGenerator�� �̿�, ���� ���� ������ ��ȯ�ϴ� �Լ�</summary>
        private int RandomRange(int min, int max)
        {
            if(max < min)
            {
                Debug.LogError("Min���� Max������ �����ϴ�.");
                return -10000;
            }
            int randInt = RandomNumberGenerator.GetInt32(min, max);
            return randInt;
        }


        /// <summary>RandomNumberGenerator�� �̿�, ���� ���� ������ ��ȯ�ϴ� �Լ�</summary>
        private float RandomRange(float min, float max)
        {
            if (max < min)
            {
                Debug.LogError("Min���� Max������ �����ϴ�.");
                return -1;
            }

            byte[] bytes = new byte[4];
            
            using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            float randValue = BitConverter.ToSingle(bytes, 0);
            float range = max - min;
            float rendFloat = (randValue * range) + min;

            return rendFloat;
        }

    }
}
