using System.Collections.Generic;
using UnityEngine;
namespace CwssCommon
{
    /// <summary>
    /// 记录级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 正确
        /// </summary>
        Correct,
        /// <summary>
        /// 警告
        /// </summary>
        Warning,
        /// <summary>
        /// 通用的错误
        /// </summary>
        Error,
        /// <summary>
        /// 标记错误
        /// </summary>
        SignError,

        /// <summary>
        /// 系统级别的表述
        /// </summary>
        System,
    }

    public class LogData
    {
        /// <summary>
        /// 记录级别
        /// </summary>
        public LogLevel Level;

        /// <summary>
        /// 记录内容
        /// </summary>
        public string Log;
        /// <summary>
        /// 备注：描述原因等信息
        /// </summary>
        public string Note;

        public LogData()
        {
        }
        public LogData(string describe)
        {
            //this.type = LogType.Normal;
            this.Level = LogLevel.Correct;
            this.Log = describe;
        }
        public LogData(LogLevel logLevel, string describe)
        {
            //this.type = type;
            this.Level = logLevel;
            this.Log = describe;
        }
        public LogData(LogLevel logLevel, string describe, string note)
        {
            this.Level = logLevel;
            this.Log = describe;
            this.Note = note;
        }

        public string FullText
        {
            get
            {
                if (Level == LogLevel.SignError || Level == LogLevel.Error)
                    return $"{Log} 错误，{Note}";
                return Log;
            }
        }

    }

    /// <summary>
    /// 实训结果
    /// </summary>
    public class TraningResult
    {
        public int TotalScore;
        public int GetScore;
        public int GetStarts;
        public bool IsCorrectProcess;//流程是否正确

        /// <summary>
        ///操作记录
        /// </summary>
        /// <value></value>
        public List<LogData> LogDatas { get; set; }
      

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="level">级别：正确，错误等级别 </param>
        /// <param name="describe">记录描述</param>
        /// <param name="note">正确描述</param>
        public void AddLog(LogLevel level, string describe, string note = null)
        {
            if (this.LogDatas == null) LogDatas = new List<LogData> { };
            LogDatas.Add(new LogData(level, describe, note));
            if (level == LogLevel.Error)
                if (IsCorrectProcess) IsCorrectProcess = false;
        }

        public void OnInit()
        {
            Debug.Log("OnInit");
            TotalScore = 0;
            GetScore = 0;
            GetStarts = 0;
            if (LogDatas == null) LogDatas = new List<LogData>();
            else LogDatas.Clear();
        }
    }
}
