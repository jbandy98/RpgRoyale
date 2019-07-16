using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHeroPanelController : MonoBehaviour {

    public Dropdown classDropDown;
    public List<Job> jobs;
    public Text heroName;
    public Text roles;
    public Text description;

	// Use this for initialization
	void Start () {
        jobs = LobbySceneController.jobs;

        List<Dropdown.OptionData> classOptions = new List<Dropdown.OptionData>();
        foreach (Job job in jobs) {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = job.jobName;
            classOptions.Add(optionData);           
        }
        classDropDown.AddOptions(classOptions);
        ClassChanged();
	}

    public void ClassChanged()
    {
        string jobName = classDropDown.options[classDropDown.value].text;
        foreach (Job job in jobs)
        {
            if (job.jobName.Equals(jobName))
            {
                // found the right job - update the other components
                roles.text = job.roles;
                description.text = job.description;
            }
        }
    }
}
