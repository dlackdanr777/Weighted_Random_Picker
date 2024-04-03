using System.Collections.Generic;
using UnityEngine;

namespace Muks.WeightedRandom
{
    /// <summary>
    /// ����ġ ���� �̱� �ý��� Ŭ����
    /// </summary>
    public class WeightedRandom<T>
    {
        public WeightedRandom()
        {
            _dic = new Dictionary<T, int>();
        }


        private Dictionary<T, int> _dic;


        /// <summary>
        /// ����ġ ����Ʈ�� �����۰� ������ �߰���
        /// </summary>
        public void Add(T item, int value)
        {
            //������ ������ �����Ѵ�.
            if (value < 0)
            {
                Debug.LogError("������ �� �� �����ϴ�.");
                return;
            }

            //���� ��ųʸ��� Ű�� �����ϸ�?
            if (_dic.ContainsKey(item))
            {
                //�ش� Ű�� ���� ��ġ�� �����Ѵ�.
                _dic[item] += value;
            }
            //�������� ������?
            else
            {
                //�������� �߰��Ѵ�.
                _dic.Add(item, value);
            }
        }


        /// <summary>
        /// ����ġ ����Ʈ�� �������� ������ ���� ������ ����, ���� ������ �� ũ�� ����Ʈ���� �������� ��
        /// </summary>
        public void Sub(T item, int value)
        {
            //������ ������ �����Ѵ�.
            if (value < 0)
            {
                Debug.LogError("������ �� �� �����ϴ�.");
                return;
            }

            //���� ��ųʸ��� Ű�� �����ϸ�?
            if (_dic.ContainsKey(item))
            {
                //Ű�� ���� ũ�Ⱑ �� ũ��?
                if (_dic[item] > value)
                {
                    //�ش� Ű�� ���� ��ġ�� �����Ѵ�.
                    _dic[item] -= value;
                }
                //���ų� ������?
                else
                {
                    //�����Ѵ�.
                    Remove(item);
                }

            }
            else
            {
                Debug.LogError("�������� �������� �ʽ��ϴ�.");
            }
        }

        /// <summary>
        /// ����Ʈ���� �������� ����
        /// </summary>
        public void Remove(T item)
        {
            //���� ��ųʸ��� Ű�� �����ϸ�?
            if (_dic.ContainsKey(item))
            {
                //�ش� Ű�� �����͸� �����Ѵ�.
                _dic.Remove(item);
            }
            else
            {
                Debug.LogError("�������� �������� �ʽ��ϴ�.");
            }
        }

        /// <summary>
        /// ���� ����Ʈ�� �ִ� �������� ����ġ�� ��� ���� ��ȯ
        /// </summary>
        public int GetTotalWeight()
        {
            int totalWeight = 0;

            //��ųʸ��� �Էµ� ��� ������ ����ġ ���� ���Ѵ�.
            foreach (int value in _dic.Values)
            {
                totalWeight += value;
            }

            return totalWeight;
        }


        /// <summary>
        /// ������ ����Ʈ�� �ִ� ��� �������� ����ġ�� ������ ��ȯ�Ͽ� ��ȯ (0, 1 ����)
        /// </summary>
        public Dictionary<T, float> GetPercent()
        {
            Dictionary<T, float> _tempDic = new Dictionary<T, float>();
            float totalWeight = GetTotalWeight();

            foreach (var item in _dic)
            {
                _tempDic.Add(item.Key, item.Value / totalWeight);
            }

            return _tempDic;
        }

        /// <summary>
        /// ������ ����Ʈ���� �������� �������� �̾� ��ȯ(���� �������� ���� -1)
        /// </summary>
        public T GetRamdomItemBySub()
        {
            //��ųʸ��� ����ִ� ������ ������ 0���ϸ�
            if (_dic.Count <= 0)
            {
                Debug.LogError("����Ʈ�� �������� �����ϴ�. �̱� �Ұ���");
                return default;
            }

            //�� ����ġ�� �����´�.
            int weight = 0;
            int totalWeight = GetTotalWeight();

            //�� ����ġ ���� 0~1f�� ���� ���� ���� �������� ���Ѵ�.
            int pivot = Mathf.RoundToInt(totalWeight * Random.Range(0.0f, 1.0f));

            //��ųʸ��� ��ȸ�ϸ� ����ġ�� ���ϴ� ������ �̻��� �Ǹ� �� �������� ��ȯ�Ѵ�.
            foreach (var item in _dic)
            {
                weight += item.Value;
                if (pivot <= weight)
                {
                    
                    _dic[item.Key] -= 1;
                    return item.Key;
                }
            }
            return default;
        }


        /// <summary>
        /// ������ ����Ʈ���� �������� �������� �̾� ��ȯ
        /// </summary>
        public T GetRamdomItem()
        {
            //��ųʸ��� ����ִ� ������ ������ 0���ϸ� ����
            if (_dic.Count <= 0)
            {
                Debug.LogError("����Ʈ�� �������� �����ϴ�. �̱� �Ұ���");
                return default;
            }

            //�� ����ġ�� �����´�.
            int totalWeight = GetTotalWeight();
            int weight = 0;

            //�� ����ġ ���� 0~1f�� ���� ���� ���� �������� ���Ѵ�.
            int pivot = Mathf.RoundToInt(totalWeight * Random.Range(0.0f, 1.0f));

            //��ųʸ��� ��ȸ�ϸ� ����ġ�� ���ϴ� ������ �̻��� �Ǹ� �� �������� ��ȯ�Ѵ�.
            foreach (var item in _dic)
            {
                weight += item.Value;
                if (pivot <= weight)
                {
                    return item.Key;
                }
            }
            return default;
        }

        /// <summary>
        /// ������ ����Ʈ�� ��ȯ
        /// </summary>
        public Dictionary<T, int> GetList()
        {
            return _dic;
        }
    }
}
