using Assets.Scripts.Core;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View;
using Assets.Scripts.View.Common;
using Assets.Scripts.View.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.View.Skills;
using UnityEngine;

public class Root : MonoBehaviour
{
    public GameObject MapViewPrefab;
    public GameObject PlayerPrefab;
    public Camera MainCamera;

    public MonsterPrefabDatabase MonsterInfos;

    public GameObject HealthBar;
    public GameObject ManaBar;

    public SkillView[] SelectedSkills { get; } = new SkillView[4];
    public SkillPrefabDatabase SkillInfos;
    public IReadOnlyCollection<SkillView> AllSkills { get; private set; }

    private Game mGame;

    internal MapView MapView { get; set; }
    internal PlayerView PlayerView { get; set; }

    private void Awake()
    {
        mGame = new Game();
        //init

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
    }

    public void GameOver()
    {
        
    }
}
