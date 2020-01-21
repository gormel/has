using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Skills;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSelectionButton : MonoBehaviour, IPointerDownHandler
{
    public Root Root;
    public SkillSelectionPanel Panel;
    public int SkillSlot;
    public Image IconTarget;
    public Sprite EmptyIcon;
    public Image CooldownIndicator;

    void Start()
    {
        IconTarget.sprite = Panel.IconDatabase.GetIcon(Root.SelectedSkills[SkillSlot]?.Model<Skill>()) ?? EmptyIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(OnClick());
    }

    private IEnumerator OnClick()
    {
        yield return StartCoroutine(Panel.SelectSkill(Root.PlayerView.Model<Player>().KnownSkills));
        if (Panel.SelectedSkill != null)
        {
            Root.SelectedSkills[SkillSlot] = Panel.SelectedSkill;
        }
        IconTarget.sprite = Panel.IconDatabase.GetIcon(Panel.SelectedSkill?.Model<Skill>()) ?? EmptyIcon;
    }

    void Update()
    {
        var skill = Root.SelectedSkills[SkillSlot]?.Model<Skill>();
        if (skill != null)
        {
            CooldownIndicator.fillAmount = 1 - skill.CooldownPercent;
        }
    }
}
