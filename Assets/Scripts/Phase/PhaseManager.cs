using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PhaseState 
{
    NoneMyState,
    Phase_Ready, //스테이지 시작 전 세팅 페이즈
    Phase_Running, //Running은 일반적인 스테이지 진행
    Phase_ChooseAbility, //레벨업하거나 보스 잡았을 경우
    Phase_MiddleBoss, //중간보스
    Phase_FinalBoss, //최종보스
    Phase_Clear
}

public class PhaseManager : SceneSingleton<PhaseManager> //페이즈 매니저가 게임매니저 역할 수행하도록 하기
{
    private PhaseStateMachine _stateMachine;
    //public List<Transform> _destinations = new();
    
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _stateMachine = GetComponent<PhaseStateMachine>();
    }

    /*public (int, Vector3) GetDestination(int index)
    {
        int resultIndex = index;
        if (resultIndex >= _destinations.Count)
        {
            resultIndex = 0;
        }

        return (resultIndex, _destinations[resultIndex].position);
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
