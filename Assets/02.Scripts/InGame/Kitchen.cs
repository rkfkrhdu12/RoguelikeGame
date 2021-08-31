using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Room
{
    [SerializeField, Header("npc�Ĵ� ���̺���� �θ� ������Ʈ")]
    // �������� ���̺��� �����ϱ� ����
    // �ϳ��� ������Ʈ�� 4���� ������ ��ǥ���ٴ� ���̺��̶�� ������Ʈ�� 1���� ������ ��ǥ�� ���ݴ� Ȱ�뼺�� ������ ������.
    private GameObject[] _pubNPCParentObjects = null;

    [SerializeField, Header("������ ������Ʈ")]
    private SmallShop _smallShopObject = null;

    private void OnEnable()
    {
        PubNPCEnable();
        SmallShopEnable();
    }

    // �Ĵ翡 �մ��� �󸶳� ��ġ�Ǹ� � ��ȣ�ۿ��� ����
    private void PubNPCEnable()
    {
        if (_pubNPCParentObjects == null) return;
    }

    // �������� ����������, �����ٸ� � �������� �Ǹ��� ������
    private void SmallShopEnable()
    {
        if (_smallShopObject == null) return;
    }

}
