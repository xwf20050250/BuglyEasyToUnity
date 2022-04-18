using UnityEngine;
using System.Collections.Generic;

public class BuglyCustom
{
    public enum E_TYPE
    {
        E_PLAYERID,
        E_PLAYERNAME,
        E_SCENEID,
        E_SYSTEMMEM,
        E_GRAPHICSMEM,
        E_SCREENSOLUTION,
        E_BUILD_MARK,
        E_MAX,
    };
    static string[] mTypeList = new string[(int)E_TYPE.E_MAX]
    {
        "PlayerID",
        "PlayerName",
        "SceneID",
        "SystemMem",
        "GraphicsMem",
        "ScreenSolution",
        "BuildMark",
    };
    static Dictionary<string, string> mCustomLog = new Dictionary<string, string>();

    static public void Init()
    {
        for (int i = 0; i < (int)(E_TYPE.E_MAX); i++)
        {
            SetCustomLog((E_TYPE)i, "Null");
        }
    }

    static public Dictionary<string, string> GetCustomDict()
    {
        return mCustomLog;
    }

    static public void SetCustomLog(E_TYPE eType, string ctx)
    {
        if (!mCustomLog.ContainsKey(mTypeList[(int)eType]))
        {
            mCustomLog.Add(mTypeList[(int)eType], ctx);
        }
        else
        {
            mCustomLog[mTypeList[(int)eType]] = ctx;
        }
    }
}
