using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reflector;
using Laser;

//リファクタリング時用、メモ
//オブジェクト作成クラス、イベント発生クラス、初期起爆用クラスぐらいにわけてみましょうね
//IOnPointObjectの見直し

public class StageManager : MonoBehaviour
{
    [SerializeField] private StartButtonManager m_startButtonManager;

    [SerializeField] private GoalManager m_goalManager;
    [SerializeField] private StartPointManager m_startPointManager;
    [SerializeField] private Laser.Laser m_laserPrefab;
    [SerializeField] private AReflector m_infinityReflectorPrefab;

    [SerializeField] private AReflector m_reflecterPrefab;
    [SerializeField] private Wall m_wallPrefab;
    [SerializeField] private CoinManager m_coinManager;
    [SerializeField] private ResultUIManager m_resultManager;
    [SerializeField] private StageDataProvider m_dataProvider;
    [SerializeField] private PlayableReflecterManager m_playableReflectorManager;
    
    private StageData m_stageData;

    private PointsManager m_pointsManager;
    private bool isFirstInit = true;

    private ILaser m_laser;
    private GoalManager m_goal;
    private StartPointManager m_start;
    private CoinManager m_coin;
    private List<Wall> m_walls =  new List<Wall>();
    private List<AReflector> m_staticReflectors = new List<AReflector>();

    private Vector2 m_cellScale = new Vector2(10.0f, 10.0f);
    private Vector2 m_firstCellPos = new Vector2(-45, 85f);

    private int m_currentStageCondition = 0;

    public void Start() {
        m_stageData = m_dataProvider.GetData();
        m_pointsManager = new PointsManager(m_firstCellPos, m_cellScale);

        StageInit();

        m_startButtonManager.SetStartPoint(m_start);
    }

    public void StageClear() {

        m_currentStageCondition++;
        
        DataManager.Instance.CurrentData.stageDatas[StageDataProvider.m_currentNum - 1] |= (int)StageDataMask.CLEAR;
        if (StageDataProvider.m_currentNum != 20) {
            DataManager.Instance.CurrentData.stageDatas[StageDataProvider.m_currentNum] |= (int)StageDataMask.OPEN;
        }

        if (m_playableReflectorManager.CountRestReflector() == 0) {
            DataManager.Instance.CurrentData.stageDatas[StageDataProvider.m_currentNum - 1] |= (int)StageDataMask.ALL_ITE;
            m_currentStageCondition++;
        }
        m_resultManager.ShowClearUI(m_currentStageCondition);
    }

    public void GameOver() {
        m_resultManager.ShowGameOverUI();
    }

    private void StageInit() {
       

        m_laser = Instantiate(m_laserPrefab,new Vector3(500,500,0),Quaternion.identity);
        m_laser.DestroyEvent += m_resultManager.ShowGameOverUI;

        m_goal = Instantiate(m_goalManager ,Vector3.zero,Quaternion.identity);
        m_start = Instantiate(m_startPointManager,Vector3.zero,Quaternion.identity);

        m_pointsManager.SetObjPointData(m_goal,m_stageData.GoalData.pos, m_stageData.GoalData.state);
        m_pointsManager.SetObjPointData(m_start,m_stageData.StartData.pos, m_stageData.StartData.state);
        m_start.SetLaser(m_laser);

        for (int i = 0; i < m_stageData.WallPoses.Length; i++) {
            Wall wall = Instantiate(m_wallPrefab,new Vector3(0,0,0) ,Quaternion.identity);
            IOnPointObject obj = wall;
            m_walls.Add(wall);

            m_pointsManager.SetObjPointData(obj,m_stageData.WallPoses[i],0);
        }

        for (int i = 0; i < m_stageData.ReflectorsData.Length; i++) {
            AReflector reflector = Instantiate(m_reflecterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            IOnPointObject obj = reflector;
            m_staticReflectors.Add(reflector);

            obj.Init(Vector3.zero,-1, m_stageData.ReflectorsData[i].durable);

            m_pointsManager.SetObjPointData(obj, m_stageData.ReflectorsData[i].pos, m_stageData.ReflectorsData[i].state);
            reflector.SetCanMove(false);
        }

        for (int i = 0; i < m_stageData.InfinityReflectorsData.Length; i++) {
            AReflector reflector = Instantiate(m_infinityReflectorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            IOnPointObject obj = reflector;

            obj.Init(Vector3.zero, -1, 99);

            m_pointsManager.SetObjPointData(obj, m_stageData.InfinityReflectorsData[i].pos, m_stageData.InfinityReflectorsData[i].state);
            reflector.SetCanMove(false);


        }

        m_coin = Instantiate(m_coinManager,Vector3.zero,Quaternion.identity);
        m_pointsManager.SetObjPointData(m_coin,m_stageData.CoinPos,0);

        m_playableReflectorManager.CreateReflector(m_stageData.PlayerSetReflectors);
        m_playableReflectorManager.SetReflectorEvents(
            (a)=> { return; },
            m_pointsManager.SetObjPointData,
            m_pointsManager.NextState

            );

        if (!isFirstInit) {
            DisposeEvents();
        }

        RegistEvents();

    }

    private void StageReset() {
        m_currentStageCondition = 0;
    }

    private void RegistEvents() {
        m_goal.Finish += StageClear;
        m_start.LaserDestoryEvent += GameOver;
        m_coin.DestroyEvent += () => {
            m_currentStageCondition++;
            DataManager.Instance.CurrentData.stageDatas[StageDataProvider.m_currentNum - 1] |= (int)StageDataMask.GET_COIN;
        };
    }

    private void DisposeEvents() {
        m_goal.Finish -= StageClear;
        m_start.LaserDestoryEvent -= GameOver;
        m_coin.DestroyEvent -= () => m_currentStageCondition++;
    }
}
