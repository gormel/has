using UnityEngine;
using System.Collections;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.Core;

public class SkillSelectionPanel : MonoBehaviour
{
    public GameObject ButtonsRoot;
    public GameObject SubmitButtonPrefab;
    public Root Root;
    public Skill SelectedSkill { get; private set; }

    private bool mSkillSubmited;

    public IEnumerator SelectSkill()
    {
        while (ButtonsRoot.transform.childCount > 0)
            Destroy(ButtonsRoot.transform.GetChild(0).gameObject);

        foreach (var skill in Root.PlayerView.Model<Player>().KnownSkills)
        {
            var buttonInst = Instantiate(SubmitButtonPrefab);
            buttonInst.transform.SetParent(ButtonsRoot.transform);
            var submitButton = buttonInst.GetComponent<SkillSubmitButton>();
            submitButton.Panel = this;
            submitButton.Skill = skill;
        }

        SelectedSkill = null;
        mSkillSubmited = false;
        gameObject.SetActive(true);
        yield return new WaitUntil(() => mSkillSubmited);
        gameObject.SetActive(false);
        yield return null;
    }

    public void SubmitSkill(Skill skill)
    {
        SelectedSkill = skill;
        mSkillSubmited = true;
    }
}
