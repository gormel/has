using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Skills;
using UnityEngine.UI;

public class SkillSubmitButton : MonoBehaviour, IPointerDownHandler
{
    public SkillSelectionPanel Panel;
    public SkillView Skill;
    public Image IconTarget;

    public void OnPointerDown(PointerEventData eventData)
    {
        Panel.SubmitSkill(Skill);
    }
}
