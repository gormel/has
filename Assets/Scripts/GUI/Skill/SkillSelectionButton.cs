using System.Collections;
using System.Linq;
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
    public Text BindingKey;

    private Skill SelectedSkill => Root.PlayerView.Model<Player>().SelectedSkills[SkillSlot]?.Skill;

    void Start()
    {
        IconTarget.sprite = Panel.IconDatabase.GetIcon(SelectedSkill) ?? EmptyIcon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(OnClick());
    }

    private IEnumerator OnClick()
    {
        yield return StartCoroutine(Panel.SelectSkill(Root.PlayerView.Model<Player>().KnownSkills.Select(r => r.Skill)));
        if (Panel.SelectedSkill != null)
        {
            var player = Root.PlayerView.Model<Player>();
            player.SelectedSkills[SkillSlot] = player.GetSkillReference(Panel.SelectedSkill.Model<Skill>());
        }
        IconTarget.sprite = Panel.IconDatabase.GetIcon(Panel.SelectedSkill?.Model<Skill>()) ?? EmptyIcon;
    }

    void Update()
    {
        var skill = SelectedSkill;
        if (skill != null)
        {
            CooldownIndicator.fillAmount = 1 - skill.CooldownPercent;
        }

        BindingKey.text = Root.PlayerView.SkillKeys[SkillSlot].ToString();
    }
}
