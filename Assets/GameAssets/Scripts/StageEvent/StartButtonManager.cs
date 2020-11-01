using UnityEngine;
using Noon.AudioManagement;

public class StartButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject InputHandlerObj;
    [SerializeField] private StartPointManager m_startPoint;
    [SerializeField] private GameObject m_coverGlass;
    [SerializeField] private Vector3 ButtonPos;
    [SerializeField] private float ButtonSize;

    private IInputHandler m_inputHandler;
    private bool isFarst = false;
    private bool runGlassRotate;
    private float angleRate = 0;

    private bool hasLaser = true;

    // Start is called before the first frame update
    void Start()
    {
        m_inputHandler = InputHandlerObj.GetComponent<IInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (runGlassRotate) {

            float nextAngle = Mathf.LerpAngle(180,150,angleRate);
            m_coverGlass.transform.eulerAngles = new Vector3(0,0,nextAngle);

            angleRate += 0.01f;

            if (nextAngle == 150) runGlassRotate = false;
        }

        if (m_inputHandler.OnTouch) {
            if (m_inputHandler.IsDiffState) {

                Vector3 mousePos = m_inputHandler.CursorPos;

                if ((mousePos - ButtonPos).magnitude <= ButtonSize) {

                    if (!isFarst) {
                        isFarst = true;
                        runGlassRotate = true;
                        AudioManager.Instance.ShotSE("glass_action");
                    } else {
                        if (hasLaser) {
                            hasLaser = false;
                            m_startPoint.FireLaser();
                        }
                    }
                }

            }
        }
        
    }

    public void SetStartPoint(StartPointManager obj) {
        m_startPoint = obj;
    }

    public void Init() {
        isFarst = false;
        runGlassRotate = false;
        angleRate = 0;
        m_coverGlass.transform.eulerAngles = new Vector3(0, 0, 180);
        hasLaser = true;
    }
}
