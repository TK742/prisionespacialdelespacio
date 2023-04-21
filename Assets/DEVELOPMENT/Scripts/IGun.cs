using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IGun : MonoBehaviour
{
    [SerializeField] Image overheatImage;
    [SerializeField] TextMeshProUGUI pointsText;

    ParticleSystem bullet;

    float overheatIndex;
    float overheatStep = 0.15f;
    float overheatThreshold = 0.8f;
    float overheatTimer = 1.5f;
    bool overheated;

    Coroutine cooldown;

    private void Start() => IManager.OnPointsChange += UpdatePoints;
    private void OnDestroy() => IManager.OnPointsChange -= UpdatePoints;
    private void UpdateImage() => overheatImage.fillAmount = overheatIndex;
    private void UpdatePoints(int points) => pointsText.text = points.ToString();

    public void Shoot()
    {
        if (overheated) 
            return;

        if (cooldown != null)
        {
            StopCoroutine(cooldown);
            cooldown = null;
        }

        //bullet.Play();

        cooldown = StartCoroutine(ICooldown());
        overheatIndex += overheatStep;
        UpdateImage();

        if (overheatIndex >= 1f)
            overheated = true;
    }

    private IEnumerator ICooldown()
    {
        yield return new WaitForSeconds(overheatTimer);
        while (overheatIndex > 0)
        {
            overheatIndex -= overheatStep * Time.deltaTime;
            UpdateImage();

            if (overheatIndex <= overheatThreshold)
                overheated = false;

            yield return null;
        }

        cooldown = null;
    }
}
