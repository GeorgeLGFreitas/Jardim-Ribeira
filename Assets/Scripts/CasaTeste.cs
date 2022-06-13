using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class CasaTeste : MonoBehaviour
{
    public static bool atualizaCasa;
    public bool poço, clorador, cisterna, waterAir;
    public bool esgoto, lixeira, fossa, jardim;
    public float pointsAgua, pointsEsgoto;
    private GameObject waterIndicator, esgotoIndicator;
    public string[] lvlCasas;
    private float media;
    private ParticleSystem particleUpgrade;
    private int levelProgress;

    public Text pointsAguaText, pointsEsgotoText;

    private void Awake()
    {
        atualizaCasa = true;
        waterIndicator = this.transform.GetChild(1).gameObject;
        esgotoIndicator = this.transform.GetChild(2).gameObject;
        particleUpgrade = this.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();
        levelProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (atualizaCasa) AtualizaCasa();
    }

    public void AtualizaCasa()
    {
        //parte da água
        if (!poço && !clorador && !cisterna && !waterAir) pointsAgua = 0;   //sem nada
        if ((poço && !clorador && !cisterna && !waterAir) ||(!poço && !clorador && cisterna && !waterAir)) pointsAgua = 25;   //apenas poço / apenas cisterna
        if ((!poço && clorador && cisterna && !waterAir) || (poço && !clorador && cisterna && !waterAir)) pointsAgua = 50;    //cisternaClorador
        if ((poço && clorador && cisterna && !waterAir) || (poço && !clorador && cisterna && waterAir)) pointsAgua = 75;     //poço + cisternaClorador
        if ((poço && clorador && cisterna && waterAir) || (!poço && clorador && cisterna && waterAir)) pointsAgua = 100;    //todos / todos menos o poço

        //parte do esgoto
        if (!lixeira && !esgoto && !fossa && !jardim) pointsEsgoto = 0;     //sem nada
        if((lixeira && !esgoto && !fossa && !jardim) || (!lixeira && esgoto && !fossa && !jardim)) pointsEsgoto = 25;      //apenas lixeira ou apenas esgoto
        if((lixeira && esgoto && !fossa && !jardim) || (!lixeira && esgoto && fossa && !jardim) || (!lixeira && !esgoto && fossa && !jardim)) pointsEsgoto = 50;       //lixeira + esgoto / esgoto + fossa / apenas fossa
        if((lixeira && esgoto && fossa && !jardim) || (lixeira && !esgoto && fossa && !jardim) || (!lixeira && !esgoto && fossa && jardim) || (!lixeira && esgoto && fossa && jardim) || (lixeira && esgoto && fossa && jardim)) pointsEsgoto = 75;      //todos menos jardim / lixeira + fossa / fossa + jardim / esgoto + fossa + jardim / todos incluindo esgoto
        if (lixeira && !esgoto && fossa && jardim) pointsEsgoto = 100;      //se tiver com todos sem o esgoto


        SetSpriteCasa(pointsAgua, pointsEsgoto);
        Invoke("SetAtualizaCasaFalse", 1f);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        CanoTeste cano = col.GetComponent<CanoTeste>();

        if(cano != null)
        {
            if (cano.GetEsgotoOn()) esgoto = true;

            if (cano.GetCisternaOn()) this.cisterna = true;

            if (cano.GetFossaOn()) this.fossa = true;

            if (cano.GetCloradorOn()) this.clorador = true;

            if (cano.GetJardimOn()) this.jardim = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        CanoTeste cano = col.GetComponent<CanoTeste>();

        if (cano != null && cano.GetEsgotoOn()) esgoto = false;
        if (cano != null && cano.GetCisternaOn()) cisterna = false;
        if(cano != null && cano.GetFossaOn()) fossa = false;
        if (cano != null && cano.GetCloradorOn()) clorador = false;
        if (cano != null && cano.GetJardimOn()) jardim = false;
    }

    public void SetWaterIndicator(bool on){ waterIndicator.SetActive(on); }
    public void SetEsgotoIndicator(bool on) { esgotoIndicator.SetActive(on); }

    public void SetAtualizaCasaFalse()
    {
        atualizaCasa = false;           //pra não ficar atualizando a casa toda hora! (dirty flag)
        esgoto = cisterna = fossa = clorador = jardim = false;      //desliga todas as condições por um momento para checar, que nem o lance do CheckWater(); dos canos
    }

    public void SetSpriteCasa(float pointsAgua, float pointsEsgoto)
    {
        media = (pointsAgua + pointsEsgoto) / 2f;

        if (media >= 0 && media <= 49)
        {
            this.gameObject.transform.GetChild(0).GetComponent<GetSpriteAtlas>().SetSpriteAtlas(lvlCasas[0]);
        }
        if (media >= 50 && media <= 99)
        {
            levelProgress++;
            this.gameObject.transform.GetChild(0).GetComponent<GetSpriteAtlas>().SetSpriteAtlas(lvlCasas[1]);
            if(levelProgress == 1) particleUpgrade.Play();
        }
        if (media >= 100)
        {
            levelProgress++;
            this.gameObject.transform.GetChild(0).GetComponent<GetSpriteAtlas>().SetSpriteAtlas(lvlCasas[2]);
            if(levelProgress == 2) particleUpgrade.Play();
        }

    }
}
