using Assets.Scripts.Core;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View;
using Assets.Scripts.View.Common;
using Assets.Scripts.View.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.View.Items;
using Assets.Scripts.View.Skills;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    public static Player SavedPlayer;//initialization method
    public static int Level;

    public GameObject MapViewPrefab;
    public GameObject PlayerPrefab;
    public Camera MainCamera;

    public MonsterPrefabDatabase MonsterInfos;

    public GameObject HealthBar;
    public Text HealthText;
    public GameObject ManaBar;
    public Text ManaText;

    public Text LevelCounter;

    public string LoseScene;
    public string WinScene;

    public SkillPrefabDatabase SkillInfos;
    public IReadOnlyCollection<SkillView> AllSkills { get; private set; }

    public ItemPrefabDatabase ItemInfos;

    private Game mGame;

    internal MapView MapView { get; set; }
    internal PlayerView PlayerView { get; set; }

    private CollectionObserver<Item> mItemsObserver;
    private Dictionary<Item, ItemView> mCreatedItems = new Dictionary<Item, ItemView>();

    private void Awake()
    {
        mGame = new Game(SavedPlayer, Level);
        mGame.LevelComplete += GameOnLevelComplete;

        var mapViewGO = Instantiate(MapViewPrefab);
        mapViewGO.transform.SetParent(transform);

        MapView = mapViewGO.GetComponentInChildren<MapView>();
        MapView.Load(mGame.Map, this);

        var playerInst = Instantiate(PlayerPrefab);
        playerInst.transform.SetParent(mapViewGO.transform);

        PlayerView = playerInst.GetComponentInChildren<PlayerView>();
        PlayerView.Load(mGame.Player, this);

        GenerateViewsByInfo(MapView.Monsters.transform, MonsterInfos, mGame.Monsters);
        var skills = new List<SkillView>();
        GenerateViewsByInfo(MapView.Skills.transform, SkillInfos, mGame.AllSkills, view => skills.Add(view as SkillView));
        AllSkills = skills.AsReadOnly();

        mItemsObserver = new CollectionObserver<Item>(mGame.Items.Keys);
        mItemsObserver.Added += ItemAdded;
        mItemsObserver.Removed += ItemRemoved;
    }

    private void ItemRemoved(object sender, CollectionChangedEventArgs<Item> e)
    {
        var go = mCreatedItems[e.Elem].gameObject;
        go.transform.SetParent(null);
        Destroy(go);
        mCreatedItems.Remove(e.Elem);
    }

    private void ItemAdded(object sender, CollectionChangedEventArgs<Item> e)
    {
        GenerateViewsByInfo(MapView.Items.transform, ItemInfos, new[] { e.Elem }, v =>
        {
            mCreatedItems[e.Elem] = v as ItemView;
            v.transform.localPosition = mGame.Items[e.Elem];
        });
    }

    private void GameOnLevelComplete(object sender, EventArgs e)
    {
        SavedPlayer = mGame.Player;
        Level++;
        SceneManager.LoadScene(WinScene);
    }

    private void GenerateViewsByInfo<TModel>(Transform parent, PrefabDatabase infos, IEnumerable<TModel> models, Action<BaseView> created = null) where TModel : class
    {
        foreach (var m in models)
        {
            var prefab = infos[m.GetType().AssemblyQualifiedName];
            if (prefab != null)
            {
                var inst = Instantiate(prefab);
                inst.transform.SetParent(parent);

                var view = inst.GetComponentInChildren<BaseView>();
                view.Load(m, this);

                created?.Invoke(view);
            }
        }
    }

    void Update()
    {
        MainCamera.transform.position = new Vector3(PlayerView.transform.position.x, PlayerView.transform.position.y, MainCamera.transform.position.z);
        mItemsObserver.Update();
        LevelCounter.text = $"Level: {Level + 1}";
    }

    public void GameOver()
    {
        SavedPlayer = mGame.Player;
        SceneManager.LoadScene(LoseScene);
    }
}
