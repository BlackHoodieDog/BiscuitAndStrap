﻿//================================================================================
//
//  CrateWeaponStatsBehaviour
//
//  箱武器スタッツの挙動
//
//================================================================================

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coffee.UIExtensions;

public class CrateWeaponStatsBehaviour : MonoBehaviour{

    /**************************************************
        Fields / Properties
    **************************************************/

    /// <summary>
    /// 武器のリスト
    /// </summary>
    public List<Weapon> weapons{
        get;
        set;
    }

    /// <summary>
    /// 武器リストの添え字
    /// </summary>
    private int weaponIndex{
        get;
        set;
    }

    //  References
    //==============================

    /// <summary>
    /// 背景の画像
    /// </summary>
    [field: SerializeField, RenameField("Stats Background Image")]
    private Image statsBackgroundImage{
        get;
        set;
    }

    /// <summary>
    /// 武器画像
    /// </summary>
    [field: SerializeField, RenameField("Weapon Image")]
    private GameObject weaponImage{
        get;
        set;
    }

    /// <summary>
    /// ランクのテキスト
    /// </summary>
    [field: SerializeField, RenameField("Rank Text")]
    private TextMeshProUGUI rankText{
        get;
        set;
    }

    /// <summary>
    /// スコアのテキスト
    /// </summary>
    [field: SerializeField, RenameField("Score Text")]
    private TextMeshProUGUI scoreText{
        get;
        set;
    }

    /// <summary>
    /// 与ダメージのテキスト
    /// </summary>
    [field: SerializeField, RenameField("Damage Text")]
    private TextMeshProUGUI damageText{
        get;
        set;
    }

    /// <summary>
    /// 発射レートのテキスト
    /// </summary>
    [field: SerializeField, RenameField("Fire Rate Text")]
    private TextMeshProUGUI fireRateText{
        get;
        set;
    }

    /// <summary>
    /// 弾速のテキスト
    /// </summary>
    [field: SerializeField, RenameField("Bullet Speed Text")]
    private TextMeshProUGUI bulletSpeedText{
        get;
        set;
    }

    /// <summary>
    /// クリティカル発生確率のテキスト
    /// </summary>
    [field: SerializeField, RenameField("Critical Chance Text")]
    private TextMeshProUGUI criticalChanceText{
        get;
        set;
    }

    /// <summary>
    /// 背景のスプライトリスト
    /// </summary>
    [field: SerializeField, RenameField("Stats Background Sprites")]
    private List<Sprite> statsBackgroundSprites{
        get;
        set;
    }

    /// <summary>
    /// OKボタン
    /// </summary>
    [field: SerializeField, RenameField("OK Button Text")]
    private TextMeshProUGUI okButton{
        get;
        set;
    }

    /**************************************************
        User Defined Functions
    **************************************************/

    /// <summary>
    /// 表示する武器の設定
    /// </summary>
    /// <param name="weapons">武器リスト</param>
    public void SetWeapons(List<Weapon> weapons){
        this.weapons = weapons;
        weaponIndex = 0;
    }

    /// <summary>
    /// 武器ステータスの表示
    /// </summary>
    public void ShowWeaponStats(){
        Weapon weapon = weapons[weaponIndex];

        statsBackgroundImage.sprite = statsBackgroundSprites[(int)(weapon.rank)];

        rankText.text = weapon.rank.ToString();
        scoreText.text = "Score: " + weapon.score;

        damageText.text = weapon.projectileDamage.ToString();
        fireRateText.text = String.Format("{0:f2}/s", (1.0f / weapon.shotDelay));
        bulletSpeedText.text = String.Format("{0:f2}/s", (weapon.projectileSpeed * 30.0f));
        criticalChanceText.text = String.Format("{0:f2}%", (weapon.criticalChance) * 100.0f);

        weaponImage.GetComponent<ShinyEffectForUGUI>().Play(1.5f);

        if(weapons.Count <= weaponIndex + 1){
            okButton.text = "OK";
        }
        else{
            okButton.text = "Next";
        }
    }

    /// <summary>
    /// 次の武器ステータスの表示
    /// </summary>
    public void GoNext(){
        weaponIndex++;

        if(weapons.Count <= weaponIndex){
            MenuManager.instance.ToggleCrateWeaponStats(false);
        }
        else{
            ShowWeaponStats();
        }
    }

}
