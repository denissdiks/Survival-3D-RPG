using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeeds : MonoBehaviour
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float hungerHealthDecay;
    public float thirstHealthDecay;

    private void Start()
    {
        // set the start values
        health.currentValue = health.startValue;
        hunger.currentValue = hunger.startValue;
        thirst.currentValue = thirst.startValue;
        sleep.currentValue = sleep.startValue;
    }

    private void Update()
    {
        //reduce needs over time
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenerate * Time.deltaTime);

        //reduce health if hungry
        if (hunger.currentValue == 0)
        {
            health.Subtract(hungerHealthDecay * Time.deltaTime);
        }

        //reduce health if thirsty
        if (thirst.currentValue == 0)
        {
            health.Subtract(thirstHealthDecay * Time.deltaTime);
        }

        //update UI bars
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();
    }

    public void Heal(float amount)
    {

    }

    public void Eat(float amount)
    {

    }

    public void Drink(float amount)
    {

    }

    public void Sleep(float amount)
    {

    }

    public void TakeDamage(int amount)
    {

    }

    public void Die()
    {

    }
}

[System.Serializable]
public class Need
{
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float regenerate;
    public float decayRate;
    public Image uiBar;

    public void Add (float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0);
    }

    public float GetPercentage()
    {
        return currentValue / maxValue;
    }
}
