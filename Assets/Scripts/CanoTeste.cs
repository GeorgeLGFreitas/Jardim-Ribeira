using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoTeste : MonoBehaviour
{
    private bool esgoto, cisterna, fossa, clorador, jardim;
	public static bool checkWater;
	//public string canoLigado, canoDesligado;
	private GetSpriteAtlas atlas;
	public Sprite canoDesligado, canoLigado;
	private SpriteRenderer sprite;

    private void Awake()
    {
		checkWater = false;
        esgoto = cisterna = fossa = false;
		//atlas = GetComponent<GetSpriteAtlas>();
		sprite = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
		checkWater = true;
    }


    // Update is called once per frame
    void Update()
    {	
		if (checkWater){ StartCoroutine(CheckWater()); }
	}

    private void OnTriggerStay2D(Collider2D col)
	{
		CanoTeste canoConectado = col.GetComponent<CanoTeste>();

		if (!this.esgoto && col.CompareTag("Cano") && canoConectado.GetEsgotoOn()){ this.esgoto =  true; }
		else if (!this.esgoto && col.CompareTag("Cano") && !canoConectado.GetEsgotoOn()){ this.esgoto = false; }
		else if (col.CompareTag("Water")){ this.esgoto = true; }

		if (!this.cisterna && col.CompareTag("Cano") && canoConectado.GetCisternaOn()) { this.cisterna = true; }
		else if (!this.cisterna && col.CompareTag("Cano") && !canoConectado.GetCisternaOn()) { this.cisterna = false; }
		else if (col.CompareTag("Cisterna")) { this.cisterna = true; }

		if (!this.fossa && col.CompareTag("Cano") && canoConectado.GetFossaOn()) { this.fossa = true; }
		else if (!this.fossa && col.CompareTag("Cano") && !canoConectado.GetFossaOn()) { this.fossa = false; }
		else if (col.CompareTag("Fossa")) { this.fossa = true; }

		if (!this.clorador && col.CompareTag("Cano") && canoConectado.GetCloradorOn()) { this.clorador = this.cisterna = true; }
		else if (!this.clorador && col.CompareTag("Cano") && !canoConectado.GetCloradorOn()) { this.clorador = false; }
		else if (col.CompareTag("CisternaClorador")) { clorador = cisterna = true; }

		if (!this.jardim && col.CompareTag("Cano") && canoConectado.GetJardimOn()) { this.jardim = this.fossa = true; }
		else if (!this.jardim && col.CompareTag("Cano") && !canoConectado.GetJardimOn()) { this.jardim = false; }
		else if (col.CompareTag("FossaJardim")) { this.jardim = this.fossa = true; }



		CheckSprite();
		checkWater = false;
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		CanoTeste canoConectado = col.GetComponent<CanoTeste>();

		if(canoConectado != null && col.CompareTag("Water")){ this.esgoto = false; }

		if (col.CompareTag("Cisterna")) this.cisterna = false;

		if (col.CompareTag("Fossa")) this.fossa = false;

		if (col.CompareTag("CisternaClorador")) this.clorador = false;

		if (col.CompareTag("FossaJardim")) this.jardim = false;

		CasaTeste.atualizaCasa = true;
	}

	public IEnumerator CheckWater()
    {
		esgoto = cisterna = fossa = clorador = jardim = false;
		yield return null;
		//checkWater = false;
	}

	public void CheckSprite()
    {
		if ((esgoto || cisterna || fossa || clorador || jardim)) sprite.sprite = canoLigado;
		else sprite.sprite = canoDesligado;

    }

	public bool GetEsgotoOn() { return this.esgoto; }
	public bool GetCisternaOn() { return this.cisterna; }
	public bool GetFossaOn() { return this.fossa; }
	public bool GetCloradorOn() { return this.clorador; }
	public bool GetJardimOn() { return this.jardim; }
}
