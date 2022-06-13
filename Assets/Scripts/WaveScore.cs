using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveScore : MonoBehaviour
{
    private int healthPeople, sickPeople;
    private float money, moneyEarned;

    private float mediaAgua, mediaEsgoto, score;

    public Text moneyText, moneyFeedbackText;
    public TextMeshProUGUI healthText, sickText;
    public List<GameObject> casas;
    public Slider aguaSlider, saudeSlider, poluiSlider;

    private int[] rodada1, rodada2, rodada3;
    public int actualTurno, actualRodadaSize, allTurnosCount;
    private int rodadaCount;

    public Text wave, round;
    private bool festejoCultural, estiagem, enchente;

    public GameObject infoWave1;

    public GameObject tilemapInicio, tilemapCheia, tilemapSeca;
    public GameObject chuvaNormal, chuvaForte, gameOver, dialogues;
    public GameObject musica1, musica2, musica3, chuvaSomFraco, chuvaSomForte;

    public GameObject trovao;
    private float timer, timeToTrovao;

    private void Awake()
    {
        healthPeople = 70;
        sickPeople = 30;
        money = 500;
        moneyEarned = 0;

        rodada1 = new int[6];
        rodada2 = new int[7];
        rodada3 = new int[7];
        actualTurno = 1;
        allTurnosCount = actualTurno;
        rodadaCount = 1;

        timer = 0f;
        timeToTrovao = 5f;

        festejoCultural = false;
    }

    void Start()
    {
        SetSaude(healthPeople);
        AtualizaMediaBarras();

        actualRodadaSize = rodada1.Length;
    }

    private void Update()
    {
        moneyText.text = "R$ " + money.ToString("F2");
        healthText.text = healthPeople.ToString();

        wave.text = "Rodada " + rodadaCount + "/3";
        round.text = actualTurno.ToString() + "/" + actualRodadaSize;

        if (timer <= 10) timer += 1 * Time.deltaTime;
        if (timer >= timeToTrovao && !trovao.activeSelf && chuvaSomForte.activeSelf)
        {
            timer = 0;
            int number = Random.Range(0, 2);

            if (number == 0) 
            {
                trovao.SetActive(false);
                trovao.SetActive(true);
            }
            else { trovao.SetActive(false); }
        }
    }

    public void NextRound()
    {
        score = CalculaMedias("mediaGeral");
        moneyEarned = 0;

        CalculaScore(score);

        money += healthPeople * 4;
        
        moneyEarned = healthPeople * 4;
        moneyFeedbackText.text = "+" + moneyEarned.ToString("F2");
        moneyFeedbackText.gameObject.SetActive(true);
        moneyFeedbackText.GetComponent<Animator>().SetTrigger("moneyWon");

        SetSaude(healthPeople);
        SetAgua(mediaAgua);
        SetPolui(mediaEsgoto);

        if (healthPeople <= 0)
        {
         gameOver.SetActive(true);
         dialogues.SetActive(false);
        }

        actualTurno++;
        allTurnosCount++;
        LigaChuva(allTurnosCount);
        //Chama aqui o método de ligar dialogo e chuva através do Keyframe da nuvem

        if (allTurnosCount == 7) ChecaFestejoCultural();
        if (allTurnosCount == 14) ChecaEstiagem();
        if (allTurnosCount == 21) ChecaEnchente();

        if (actualTurno > actualRodadaSize)
        {
            rodadaCount++;
            actualTurno = 1;
            
            if(rodadaCount == 2){ actualRodadaSize = rodada2.Length; }
            else if(rodadaCount == 3){ actualRodadaSize = rodada3.Length;}
        }

        if (allTurnosCount >= 9)
        {
            tilemapInicio.SetActive(false);
            tilemapCheia.SetActive(false);
            tilemapSeca.SetActive(true);
        }

        if (allTurnosCount >= 14)
        {
            tilemapInicio.SetActive(false);
            tilemapCheia.SetActive(true);
            tilemapSeca.SetActive(false);
        }

        
    }
    public void LigaCoisas()
    {
        LigaDialogo(allTurnosCount);
    }

    private void LigaDialogo(int currentTurno)
    {
        switch (currentTurno)
        {
            case 2:
                infoWave1.SetActive(true);
                break;
            
            case 4:
                DialogueStorage.SharedInstance.LigaDialogo(0);
                break;

            case 6:
                DialogueStorage.SharedInstance.LigaDialogo(1);
                break;

            case 7:
                if(festejoCultural) DialogueStorage.SharedInstance.LigaDialogo(2);
                else DialogueStorage.SharedInstance.LigaDialogo(3);
                musica1.SetActive(false);
                musica2.SetActive(true);
                musica3.SetActive(false);
                break;

            case 8:
                DialogueStorage.SharedInstance.LigaDialogo(4);
                break;

            case 11:
                DialogueStorage.SharedInstance.LigaDialogo(5);
                break;

            case 14:
                if(estiagem) DialogueStorage.SharedInstance.LigaDialogo(6);
                else DialogueStorage.SharedInstance.LigaDialogo(7);
                musica1.SetActive(false);
                musica2.SetActive(false);
                musica3.SetActive(true);
                break;

            case 16:
                DialogueStorage.SharedInstance.LigaDialogo(8);
                break;

            case 17:
                DialogueStorage.SharedInstance.LigaDialogo(9);
                break;

            case 19:
                DialogueStorage.SharedInstance.LigaDialogo(10);
                break;

            case 20:
                DialogueStorage.SharedInstance.LigaDialogo(11);
                break;

            case 21:
                if(enchente) DialogueStorage.SharedInstance.LigaDialogo(12);
                else DialogueStorage.SharedInstance.LigaDialogo(13);
                break;

            default:
                break;
        }
    }

    private void LigaChuva(int currentTurno)
    {
        switch(currentTurno)
        {
            case 3:
                chuvaNormal.SetActive(true);
                chuvaSomFraco.SetActive(true);
                break;

            case 5:
                chuvaNormal.SetActive(true);
                chuvaSomFraco.SetActive(true);
                break;

            case 7:
                chuvaNormal.SetActive(true);
                chuvaSomFraco.SetActive(true);
                break;

            case 8:
                chuvaNormal.SetActive(true);
                chuvaSomFraco.SetActive(true);
                break;

            case 14:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 15:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 16:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 17:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 18:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 19:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 20:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            case 21:
                chuvaForte.SetActive(true);
                chuvaSomForte.SetActive(true);
                break;

            default:
                chuvaNormal.SetActive(false);
                chuvaForte.SetActive(false);
                chuvaSomFraco.SetActive(false);
                chuvaSomForte.SetActive(false);
                break;
        }
    }

    public float CalculaMedias(string media)
    {
        float totalPointsAgua = 0;
        float totalPointsEsgoto = 0;

        casas.Clear();
        casas.AddRange(GameObject.FindGameObjectsWithTag("Casa"));

        for (int i = 0; i < casas.Count; i++)
        {
            totalPointsAgua += casas[i].GetComponent<CasaTeste>().pointsAgua;
            totalPointsEsgoto += casas[i].GetComponent<CasaTeste>().pointsEsgoto;
        }

        if (media == "mediaAgua"){ return mediaAgua = totalPointsAgua / casas.Count; }
        else if (media == "mediaEsgoto"){ return mediaEsgoto = totalPointsEsgoto / casas.Count; }
        else
        {
            mediaAgua = totalPointsAgua / casas.Count;
            mediaEsgoto = totalPointsEsgoto / casas.Count;

            return (mediaAgua + mediaEsgoto) / 2;
        }
    }

    public void CalculaScore(float score)
    {
        switch(score)
        {
            case float n when (n >= 100):
                CurePeople(10);
                break;

            case float n when (n >= 95 && n < 100):
                CurePeople(10);
                break;

            case float n when (n >= 90 && n < 95):
                CurePeople(10);
                break;

            case float n when (n >= 85 && n < 90):
                CurePeople(10);
                break;

            case float n when (n >= 80 && n < 85):
                CurePeople(8);
                break;

            case float n when (n >= 75 && n < 80):
                CurePeople(8);
                break;

            case float n when (n >= 70 && n < 75):
                CurePeople(4);
                break;

            case float n when (n >= 65 && n < 70):
                CurePeople(4);
                break;

            case float n when (n >= 60 && n < 65):
                CurePeople(2);
                break;

            case float n when (n >= 55 && n < 60):
                CurePeople(2);
                break;

            ////

            case float n when (n >= 50 && n < 55):
                MakeSickPeople(2);
                break;

            case float n when (n >= 45 && n < 50):
                MakeSickPeople(2);
                break;

            case float n when (n >= 40 && n < 45):
                MakeSickPeople(4);
                break;

            case float n when (n >= 35 && n < 40):
                MakeSickPeople(4);
                break;

            case float n when (n >= 30 && n < 35):
                MakeSickPeople(8);
                break;

            case float n when (n >= 25 && n < 30):
                MakeSickPeople(8);
                break;

            case float n when (n >= 20 && n < 25):
                MakeSickPeople(8);
                break;

            case float n when (n >= 15 && n < 20):
                MakeSickPeople(8);
                break;

            case float n when (n >= 10 && n < 15):
                MakeSickPeople(8);
                break;

            case float n when (n >= 5 && n < 10):
                MakeSickPeople(8);
                break;

            case float n when (n >= 0 && n < 5):
                MakeSickPeople(8);
                break;
        }
    }

    public void CurePeople(int valueToCure)
    {
        if (sickPeople >= valueToCure)
        {
            healthPeople += valueToCure;
            sickPeople -= valueToCure;
        }
        else if (sickPeople < valueToCure)
        {
            int number = GetSickPeople();
            healthPeople += number;
            sickPeople -= number;
        }
    }

    public void MakeSickPeople(int valueToSick)
    {
        if(healthPeople >= valueToSick)
        {
            sickPeople += valueToSick;
            healthPeople -= valueToSick;
        }
        else if(healthPeople < valueToSick)
        {
            int number = GetHealthPeople();
            sickPeople += number;
            healthPeople -= number;
        }
    }

    private void ChecaFestejoCultural()
    {
        int casasComLixeira = 0;
        casas.Clear();
        casas.AddRange(GameObject.FindGameObjectsWithTag("Casa"));

        for(int i = 0; i < casas.Count; i++)
        {
            if (casas[i].GetComponent<CasaTeste>().lixeira) casasComLixeira++;
        }

        if(casasComLixeira >= casas.Count)
        {
            festejoCultural = true;
        }
        else
        {
            MakeSickPeople(10);
        }
    }

    private void ChecaEstiagem()
    {
        int casasComCisterna = 0;
        casas.Clear();
        casas.AddRange(GameObject.FindGameObjectsWithTag("Casa"));

        for(int i = 0; i < casas.Count; i++)
        {
            if (casas[i].GetComponent<CasaTeste>().cisterna) casasComCisterna++;
        }

        if(casasComCisterna >= casas.Count)
        {
            estiagem = true;
        }
        else
        {
            MakeSickPeople(15);
        }
    }

    private void ChecaEnchente()
    {
        int casasComEsgoto = 0;
        casas.Clear();
        casas.AddRange(GameObject.FindGameObjectsWithTag("Casa"));

        for(int i = 0; i < casas.Count; i++)
        {
            if (casas[i].GetComponent<CasaTeste>().esgoto) casasComEsgoto++;
        }

        if(casasComEsgoto > 0 && CalculaMedias("") < 80) 
        { 
            enchente = false; 
            MakeSickPeople(20);
        }
        else if(casasComEsgoto <= 0 && CalculaMedias("") >= 80) { enchente = true; }
    }
    
    public int GetHealthPeople() { return this.healthPeople; }
    public int GetSickPeople() { return this.sickPeople; }

    public void AtualizaMediaBarras()
    {
        SetAgua(CalculaMedias("mediaAgua"));
        SetPolui(CalculaMedias("mediaEsgoto"));
    }

    public void SpentMoney(float actualPrice)
    {
        money += actualPrice;
        moneyFeedbackText.text = actualPrice.ToString("F2");
        moneyFeedbackText.gameObject.SetActive(true);
        moneyFeedbackText.GetComponent<Animator>().SetTrigger("moneySpent");
    }

    public void SetSaude(int saude){ saudeSlider.value = saude; }
    public void SetAgua(float agua){ aguaSlider.value = agua; }
    public void SetPolui(float polui){ poluiSlider.value = polui; }
    public float GetMoney() { return this.money; }

    
}
