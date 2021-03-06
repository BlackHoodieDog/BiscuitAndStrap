﻿//================================================================================
//
//  Item
//
//  インベントリアイテムの規定クラス
//
//================================================================================

using UnityEngine;

[System.Serializable]
public class Item{

    /**************************************************
        Fields / Properties
    **************************************************/

    /// <summary>
    /// 名前
    /// </summary>
    [field: SerializeField, RenameField("Name")]
    public string name;

}
