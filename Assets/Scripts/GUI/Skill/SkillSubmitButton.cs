using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Skills;

public class SkillSubmitButton : MonoBehaviour, IPointerDownHandler
{
    public SkillSelectionPanel Panel;
    public SkillView Skill;

    public void OnPointerDown(PointerEventData eventData)
    {
        Panel.SubmitSkill(Skill);
    }
}
