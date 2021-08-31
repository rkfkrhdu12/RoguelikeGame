using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : Room
{
    [SerializeField, Header("npc식당 테이블들의 부모 오브젝트")]
    // 여러개의 테이블을 조절하기 위함
    // 하나의 오브젝트로 4개의 고정된 좌표보다는 테이블이라는 오브젝트의 1개의 고정된 좌표가 조금더 활용성과 수정이 용이함.
    private GameObject[] _pubNPCParentObjects = null;

    [SerializeField, Header("노점상 오브젝트")]
    private SmallShop _smallShopObject = null;

    private void OnEnable()
    {
        PubNPCEnable();
        SmallShopEnable();
    }

    // 식당에 손님이 얼마나 배치되며 어떤 상호작용을 할지
    private void PubNPCEnable()
    {
        if (_pubNPCParentObjects == null) return;
    }

    // 노점상이 열릴것인지, 열린다면 어떤 아이템을 판매할 것인지
    private void SmallShopEnable()
    {
        if (_smallShopObject == null) return;
    }

}
