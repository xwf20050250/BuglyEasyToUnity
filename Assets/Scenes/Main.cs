using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button nullBtn;
    public Button errorBtn;
    public Button enableLogBtn;
    public Button debugBuglyBtn;
    public Button warningBtn;

    private static string TAG = "UPDATE_SYSTEM";
    private static string InitTag
    {
        get
        {
            return string.Format(@"[{0} {1}]: ", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"), TAG);
        }
    }

    private void Start()
    {
        nullBtn.onClick.AddListener(OnBtnNullClicked);
        errorBtn.onClick.AddListener(OnBtnErrorClicked);
        enableLogBtn.onClick.AddListener(OnBtnEnableLogClicked);
        debugBuglyBtn.onClick.AddListener(OnBtnDebugBuglyClicked);
        warningBtn.onClick.AddListener(OnBtnWarningClicked);
        BuglyAgent.ConfigDebugMode(true);
        BuglyAgent.InitWithAppId("0384ca4213");
        BuglyCustom.Init();
        BuglyCustom.SetCustomLog(BuglyCustom.E_TYPE.E_BUILD_MARK, "E_BUILD_MARK");
        BuglyAgent.SetLogCallbackExtrasHandler(BuglyCustom.GetCustomDict);
        BuglyAgent.EnableExceptionHandler();
        BuglyAgent.OnReportLogCallback += OnBuglyReportLogCallback;
        Debug.unityLogger.logEnabled = true;
        Debug.unityLogger.filterLogType = LogType.Log;
    }

    private void OnBuglyReportLogCallback(string logs)
    {
        Debug.Log($"OnBuglyReportLogCallback::logs-{logs}");
    }

    private void OnBtnNullClicked()
    {
        Debug.Log("Test Bugly NullReferenceException");
        GameObject go = null;
        go.name = "";
    }

    private void OnBtnErrorClicked()
    {
        Debug.LogErrorFormat($"{InitTag}11111111111111");
    }

    private void OnBtnWarningClicked()
    {
        Debug.LogWarningFormat($"{InitTag}222222222222");
    }

    private void OnBtnEnableLogClicked()
    {
        Debug.unityLogger.logEnabled = !Debug.unityLogger.logEnabled;
        enableLogBtn.transform.Find("Text").GetComponent<Text>().text = $"EnableLog-{Debug.unityLogger.logEnabled}";
    }

    bool m_debugBugly = true;
    private void OnBtnDebugBuglyClicked()
    {
        m_debugBugly = !m_debugBugly;
        BuglyAgent.ConfigDebugMode(m_debugBugly);
        debugBuglyBtn.transform.Find("Text").GetComponent<Text>().text = $"DebugBugly-{m_debugBugly}";
    }
}
