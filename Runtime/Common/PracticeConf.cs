using Sirenix.OdinInspector;
using UnityEngine;

namespace CwssCommon
{
    [CreateAssetMenu(menuName = "数据表/练习任务配置")]
    public class PracticeConf : ScriptableObject
    {
       // public BaseGraph Graph;

        /// <summary>
        /// 任务时间(分钟)
        /// </summary>
        [SuffixLabel("分钟"), TableColumnWidth(10)]
        public int Time = 20;

        public bool UseStandaloneDialogue;       

    }
}