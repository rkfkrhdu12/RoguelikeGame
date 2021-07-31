using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMasageManager
{
    public void Init()
    {
        UnityGoogleSheet.Load<UIMasageSheet.Data>();

        var msgList = UIMasageSheet.Data.DataList;
        foreach (var line in msgList)
        {
            _msgList.Add(line.index.ToString(), line.msg);
            _msgListCode.Add(line.index, line.msg);
        }
    }

    #region Public Variable

    public Dictionary<string, string> MsgList { get { return _msgList; } }
    public Dictionary<int, string> MsgListCode { get { return _msgListCode; } }
    #endregion

    #region Private Variable

    private Dictionary<string, string> _msgList = new Dictionary<string, string>();
    private Dictionary<int, string> _msgListCode = new Dictionary<int, string>();

    #endregion
}
