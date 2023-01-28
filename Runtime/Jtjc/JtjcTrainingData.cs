using System.Collections.Generic;
using System;
using UniRx;

namespace CwssCommon
{
    /// <summary>
    /// 静态检车培训数据
    /// </summary>
    public class JtjcTrainingData
    {
        //任务计时
        public ReactiveProperty<int> Time { get; set; } = new ReactiveProperty<int>();


        /// <summary>
        /// 当前练习/考试使用的配置
        /// </summary>
        public PracticeConf CurPracticeConf { get; set; }

        /// <summary>
        /// 功能：用于存储静态检车中已检查的设备名称
        /// key：组名
        /// value 组下的设备类型集合，将已检查的可选操作设备的名称加入这个集合，后面用于判断节点的类型名称是否在这个集合中，在说明这个类型的已经检查了
        /// </summary>
        public Dictionary<string, HashSet<string>> optinalSignSet = new Dictionary<string, HashSet<string>>();

        /// <summary>
        /// 步骤列表 key：flow order
        /// </summary>
        public ReactiveCollection<Tuple<int, string>> StepList { get; set; } = new ReactiveCollection<Tuple<int, string>>();

        /// <summary>
        /// 根流程图中的Order：当前步骤名
        /// </summary>
        public IntReactiveProperty CurStepOrder { get; set; }  = new IntReactiveProperty(-1);



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

        /// <summary>
        /// 实训结果
        /// </summary>
        // public TraningResult trainingResult = new TraningResult();

        public int GetUseTime(int totalTime)
        {
            return totalTime - Time.Value;
        }

        public bool GetSignedResualt(string group, string deviceType)
        {
            if (optinalSignSet.ContainsKey(group))
                return optinalSignSet[group].Contains(deviceType);
            return false;
        }
    }
}



