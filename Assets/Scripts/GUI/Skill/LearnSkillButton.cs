using System.Collections;
using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Skills;
using UnityEngine;
using UnityEngine.EventSystems;

public class LearnSkillButton : MonoBehaviour, IPointerDownHandler
{
    public Root Root;
    public SkillSelectionPanel Panel;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(OnClick());
    }

    private IEnumerator OnClick()
    {
        yield return StartCoroutine(Panel.SelectSkill(Root.AllSkills
            .Select(v => v.Model<Skill>())
            .Where(s => SkillRequarements.Check(s, Root.PlayerView.Model<Player>()))
            .Except(Root.PlayerView.Model<Player>().KnownSkills)));

        if (Panel.SelectedSkill != null)
            Root.PlayerView.Model<Player>().LearnSkill(Panel.SelectedSkill.Model<Skill>());
    }
}