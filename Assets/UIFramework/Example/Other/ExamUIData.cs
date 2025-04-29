using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewUIData", menuName = "UIData/UIDataAsset")]
public class ExamUIData : ScriptableObject
{
    [Serializable]
    public class UIData
    {
        public string playerName;
        public int gold;
        public int diamond;
        [Range(1, 15)]
        public int level;
        [Range(0,10)]
        public int sugar;

        private string old_playerName;
        private int old_gold;
        private int old_diamond;
        private int old_level;
        private int old_sugar;

        private void OnEnable()
        {
            old_playerName = playerName;
            old_gold = gold;
            old_diamond = diamond;
            old_level = level;
            old_sugar = sugar;
        }

        private void OnValidate()
        {
            if(old_playerName != playerName)
            {
                DataCenter.Instance.UpdateData("playerName");
                old_playerName=playerName;
            }
            if(old_gold != gold)
            {
                DataCenter.Instance.UpdateData("gold");
                old_gold=gold;
            }
            if(old_diamond != diamond)
            {
                DataCenter.Instance.UpdateData("diamond");
                old_diamond=diamond;
            }
            if (old_level != level)
            {
                DataCenter.Instance.UpdateData("level");
                old_level = level;
            }
            if(old_sugar != sugar)
            {
                DataCenter.Instance.UpdateData("sugar");
                old_sugar = sugar;
            }
        }
    }

    public UIData uiData;
}
