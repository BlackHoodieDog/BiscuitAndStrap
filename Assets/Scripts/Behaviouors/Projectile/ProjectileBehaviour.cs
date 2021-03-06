﻿//================================================================================
//
//  ProjectileBehaviour
//
//  射出物の基底クラス
//
//================================================================================

using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour{

    /**************************************************
        Fields / Properties
    **************************************************/

    /// <summary>
    /// 与えるダメージ
    /// </summary>
    protected int damage{
        get;
        set;
    }

    /// <summary>
    /// クリティカル発生確率
    /// </summary>
    protected float criticalChance{
        get;
        set;
    }

    /// <summary>
    /// 自動消滅までの時間
    /// </summary>
    private float lifeTime{
        get;
        set;
    }

    /// <summary>
    /// ダメージポップアップのプレハブ
    /// </summary>
    [field: SerializeField, RenameField("Damage Popup Prefab")]
    private GameObject damagePopupPrefab{
        get;
        set;
    }

    /**************************************************
        User Defined Functions
    **************************************************/

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="damage">ダメージ量</param>
    /// <param name="criticalChance">クリティカル発生確率</param>
    virtual public void Initialize(int damage, float criticalChance){
        this.damage = damage;
        this.criticalChance = criticalChance;
        lifeTime = 5.0f;
    }

    /// <summary>
    /// 発射
    /// </summary>
    /// <param name="direction">向き</param>
    /// <param name="speed">弾速</param>
    virtual public void Shoot(Vector3 direction, float speed){
        Destroy(transform.gameObject, lifeTime);
    }

    /// <summary>
    /// ダメージの付与
    /// </summary>
    /// <param name="target">対象</param>
    private void DealDamage(CharacterBehavior target){
        float randomNumber = UnityEngine.Random.value;

        if(randomNumber <= criticalChance){
            target.TakeDamage(damage * 2, GetComponent<Rigidbody2D>().velocity.normalized);

            GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
            damagePopup.GetComponent<DamagePopupBehaviour>().Initialize(damage * 2, true);

            GameManager.instance.DealDamage(damage * 2);
        }
        else{
            target.TakeDamage(damage, GetComponent<Rigidbody2D>().velocity.normalized);

            GameObject damagePopup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
            damagePopup.GetComponent<DamagePopupBehaviour>().Initialize(damage, false);

            GameManager.instance.DealDamage(damage);
        }
    }

    /// <summary>
    /// 物体に衝突した際の処理
    /// </summary>
    /// <param name="collider">コライダー</param>
    public void OnTriggerEnter2D(Collider2D collider){
        switch(collider.gameObject.tag){
            case "Platform":
                //Destroy(transform.gameObject);

                break;
            case "Enemy":
                DealDamage(collider.gameObject.GetComponent<CharacterBehavior>());
                Destroy(transform.gameObject);

                break;
        }
    }

    /// <summary>
    /// 画面(ゲームエリア)を離れた際の処理
    /// </summary>
    /// <param name="collision">コリジョン</param>
    public void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Game Area"){
            Destroy(transform.gameObject);
        }
    }

}
