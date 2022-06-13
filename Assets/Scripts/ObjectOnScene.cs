using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ObjectOnScene : MonoBehaviour
{
    public GridMapConstructor gmc;
    private WaveScore waveScore;
    [SerializeField] private SpriteAtlas atlas;

    public GameObject canoReto, canoL, canoT, poço, lixeira, cisterna, fossa, waterAir, clorador, jardim;
    public GameObject poçoArea;
    private GameObject actualToolSelected;
    public GameObject toolPreview, buttonsToolPreview;
    public BoxCollider2D colliderToolPreview;
    private float actualRotation, actualPrice;
    private bool canPlace, canDestroy, canUpgrade;

    public bool deleta;
    private Vector2 touchWorldPos;
    public Camera cam;

    public ParticleSystem particleConstruction, particleDestruction;

    // Start is called before the first frame update
    void Start()
    {
        waveScore = GetComponent<WaveScore>();

        canPlace = true;
        canDestroy = false;
        actualPrice = 1000;
        this.deleta = false;
    }

    // Update is called once per frame
    void Update()
    {
        PreviewTool();
        RemoveObject();
        if(deleta == true)
        {
            
        }
        
        UpgradeObject();
        
    }

    public void PreviewTool()
    {
        if (Input.touchCount > 0 && actualToolSelected !=null && !canUpgrade && canPlace)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) { touchWorldPos = cam.ScreenToWorldPoint(touch.position); }

            HeatMapGridObject heatMapGridObject = gmc.grid.GetGridObject(touchWorldPos);


            if (heatMapGridObject != null && heatMapGridObject.GetValueNormalized() == 0)
            {
                toolPreview.SetActive(true);
                toolPreview.transform.position = gmc.grid.GetWorldPosition(heatMapGridObject.x, heatMapGridObject.y) + new Vector3(gmc.grid.cellSize, gmc.grid.cellSize) * .5f;
                //toolPreview.transform.position = new Vector3(toolPreview.transform.position.x, toolPreview.transform.position.y, -5);
                buttonsToolPreview.SetActive(false);
            }
        }
    }

    public void PlaceObject() //quando aperta no botão verde
    {
        HeatMapGridObject heatMapGridObject = gmc.grid.GetGridObject(toolPreview.transform.position);

        if (actualToolSelected != null && waveScore.GetMoney() >= actualPrice)
        {
            heatMapGridObject.AddValue(5);
            GameObject e = Instantiate(actualToolSelected) as GameObject;
            e.transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, actualRotation);
            e.transform.position = gmc.grid.GetWorldPosition(heatMapGridObject.x, heatMapGridObject.y) + new Vector3(gmc.grid.cellSize, gmc.grid.cellSize) * .5f;
            PlayParticle(heatMapGridObject, 1);

            Invoke("DelaySetCanPlace", 0.5f);
            colliderToolPreview.enabled = true;
            toolPreview.SetActive(false);
            actualRotation = 0f;
            toolPreview.transform.GetChild(0).GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, actualRotation);

            //Invoke("AtualizaBarras", 1f);
            //waveScore.AtualizaMediaBarras();

            

            waveScore.SpentMoney(-actualPrice);

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Dinheiro gastando");

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Colocando Construçoes S confirmar");

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Colocando Construçoes");
        }
        else if (actualToolSelected != null && waveScore.GetMoney() < actualPrice)
        {
            waveScore.moneyText.GetComponent<Animator>().SetTrigger("withoutMoney");
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Remover Construção");
        }

        Invoke("AtualizaBarras", 1.5f);
        CasaTeste.atualizaCasa = true;
    }

    public void RemoveObject()
    {
        if (actualToolSelected == null && canDestroy && !canUpgrade && this.deleta == true)
        {
            toolPreview.SetActive(false);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Stationary:
                        touchWorldPos = cam.ScreenToWorldPoint(touch.position);
                        print(touch.position);
                        RaycastHit2D hitInfo = Physics2D.Raycast(touchWorldPos, cam.transform.forward);

                        if (hitInfo.collider != null && canDestroy && hitInfo.collider.gameObject.layer == 8 && waveScore.GetMoney() >= actualPrice)
                        {
                            HeatMapGridObject heatMapGridObject = gmc.grid.GetGridObject(hitInfo.collider.gameObject.transform.position);
                            heatMapGridObject.AddValue(-5);
                            PlayParticle(heatMapGridObject, 2);

                            Destroy(hitInfo.collider.gameObject);

                            waveScore.SpentMoney(-actualPrice);
                            CanoTeste.checkWater = true;

                            //Invoke("AtualizaBarras", 1.5f);
                            //CasaTeste.atualizaCasa = true;

                            deleta = false;
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Remover Construção");
                        }
                       
                        else if (hitInfo.collider != null && canDestroy && hitInfo.collider.gameObject.layer == 8 && waveScore.GetMoney() < actualPrice)
                        {
                            waveScore.moneyText.GetComponent<Animator>().SetTrigger("withoutMoney");
                            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX's/SFX- Remover Construção");
                        }
                        break;
                }
                Invoke("AtualizaBarras", 1.5f);
                CasaTeste.atualizaCasa = true;
            }
            
        }
        
    }

    public void UpgradeObject()
    {
        if (canUpgrade)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchWorldPos = cam.ScreenToWorldPoint(touch.position);
                        RaycastHit2D hitInfo = Physics2D.Raycast(touchWorldPos, cam.transform.forward);

                        if (actualToolSelected == clorador && hitInfo.collider != null && canUpgrade && hitInfo.collider.gameObject.layer == 8 && waveScore.GetMoney() >= actualPrice)
                        {
                            if (hitInfo.collider.gameObject.CompareTag("Cisterna"))
                            {
                                hitInfo.collider.transform.GetChild(0).tag = "CisternaClorador";
                                hitInfo.collider.GetComponent<GetSpriteAtlas>().SetSpriteAtlas("Cisterna Clorador");

                                toolPreview.SetActive(false);
                                waveScore.SpentMoney(-actualPrice);
                                Invoke("AtualizaBarras", 1.5f);
                                CasaTeste.atualizaCasa = true;
                            }
                        }
                        else if (hitInfo.collider != null && canUpgrade && hitInfo.collider.CompareTag("Cisterna") && waveScore.GetMoney() < actualPrice)
                        {
                            waveScore.moneyText.GetComponent<Animator>().SetTrigger("withoutMoney");
                        }


                        if (actualToolSelected == jardim && hitInfo.collider != null && canUpgrade && hitInfo.collider.gameObject.layer == 8 && waveScore.GetMoney() >= actualPrice)
                        {
                            if (hitInfo.collider.gameObject.CompareTag("Fossa"))
                            {
                                hitInfo.collider.transform.GetChild(0).tag = "FossaJardim";
                                hitInfo.collider.GetComponent<GetSpriteAtlas>().SetSpriteAtlas("Jardim Filtrante");

                                toolPreview.SetActive(false);
                                //waveScore.AtualizaMediaBarras();
                                waveScore.SpentMoney(-actualPrice);
                                Invoke("AtualizaBarras", 1.5f);
                                CasaTeste.atualizaCasa = true;
                            }
                        }
                        else if (hitInfo.collider != null && canUpgrade && hitInfo.collider.CompareTag("Fossa") && waveScore.GetMoney() < actualPrice)
                        {
                            waveScore.moneyText.GetComponent<Animator>().SetTrigger("withoutMoney");
                        }

                        break;
                }

                //Invoke("AtualizaBarras", 1.5f);
                //CasaTeste.atualizaCasa = true;
            }

            //Invoke("AtualizaBarras", 1.5f);
            //CasaTeste.atualizaCasa = true;
        }
    }

    public void OpenToolPreviewMenu()
    {
        canPlace = false;
        colliderToolPreview.enabled = false;
        buttonsToolPreview.SetActive(true);
    }

    public void CloseToolMenu()
    {
        canPlace = true;
        colliderToolPreview.enabled = true;
        buttonsToolPreview.SetActive(false);
    }

    public void CloseAnyTool()
    {
        if (canDestroy || canUpgrade) UI_Menus.uiMenus.DisplayTextDestroyTool(false, "");
        canPlace = true;
        canDestroy = false;
        canUpgrade = false;
        actualToolSelected = null;
        colliderToolPreview.enabled = true;
        buttonsToolPreview.SetActive(false);
        toolPreview.SetActive(false);
        actualToolSelected = null;
        actualRotation = 0f;

        UI_Menus.uiMenus.changeToolButton1.SetActive(true);
        poçoArea.SetActive(false);
    }

    public void SetActualToolSelected(string toolName)
    {
        poçoArea.SetActive(false);

        switch (toolName)
        {
            case "destroy":
                actualToolSelected = null;
                canDestroy = true;
                UI_Menus.uiMenus.DisplayTextDestroyTool(true, "Pressione sobre objeto para removê-lo");
                actualPrice = 20;
                break;

            case "canoReto":
                actualToolSelected = canoReto;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = canoReto.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 20;
                break;

            case "canoL":
                actualToolSelected = canoL;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = canoL.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 20;
                break;

            case "canoT":
                actualToolSelected = canoT;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = canoT.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 30;
                break;

            case "poço":
                actualToolSelected = poço;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = poço.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 40;
                poçoArea.SetActive(true);
                break;

            case "cisterna":
                actualToolSelected = cisterna;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = cisterna.GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 300;
                break;

            case "clorador":
                actualToolSelected = clorador;
                canUpgrade = true;
                UI_Menus.uiMenus.DisplayTextDestroyTool(true, "Selecione uma cisterna para melhorar");
                actualPrice = 100;
                break;

            case "waterAir":
                actualToolSelected = waterAir;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = waterAir.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 500;
                poçoArea.SetActive(true);
                break;

            case "lixeira":
                actualToolSelected = lixeira;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = lixeira.transform.GetChild(0).GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 60;
                poçoArea.SetActive(true);
                break;

            case "fossa":
                actualToolSelected = fossa;
                toolPreview.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = fossa.GetComponent<GetSpriteAtlas>().GetSpriteatlas();
                actualPrice = 400;
                break;

            case "jardim":
                actualToolSelected = jardim;
                canUpgrade = true;
                UI_Menus.uiMenus.DisplayTextDestroyTool(true, "Selecione uma fossa para melhorar");
                actualPrice = 100;
                break;
        }
    }

    public void RotateObject()
    {
        actualRotation = toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation.z;

        if (actualToolSelected == canoReto)
        {
            switch (actualRotation)
            {
                case 0:
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, 90f);
                    actualRotation = 90f;
                    break;

                case 0.7071068f:    //90f in rotation Z/0.7071068f
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, 0f);
                    actualRotation = 0f;
                    break;
            }
        }


        if (actualToolSelected == canoL || actualToolSelected == canoT)
        {
            switch (actualRotation)
            {
                case 0:
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, 90f);
                    actualRotation = 90f;
                    break;

                case 0.7071068f:    //90f in rotation Z
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, 180f);
                    actualRotation = 180f;
                    break;

                case 1:
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, -90f);
                    actualRotation = -90f;
                    break;

                case -0.7071068f:   //270f
                    toolPreview.GetComponentInChildren<SpriteRenderer>().GetComponent<Transform>().transform.rotation = Quaternion.Euler(toolPreview.transform.rotation.x, toolPreview.transform.rotation.y, 0);
                    actualRotation = 0;
                    break;
            }

        }

    }

    private void PlayParticle(HeatMapGridObject heatMapGridObject, int number)
    {

        if (number == 1)
        {
            particleConstruction.gameObject.SetActive(false);
            particleConstruction.gameObject.transform.position = gmc.grid.GetWorldPosition(heatMapGridObject.x, heatMapGridObject.y) + new Vector3(gmc.grid.cellSize, gmc.grid.cellSize) * .5f;
            particleConstruction.gameObject.transform.position = new Vector2(particleConstruction.gameObject.transform.position.x, particleConstruction.gameObject.transform.position.y - 2f);
            particleConstruction.gameObject.SetActive(true);
            particleConstruction.Play();
        }
        else if(number == 2)
        {
            particleDestruction.gameObject.SetActive(false);
            particleDestruction.gameObject.transform.position = gmc.grid.GetWorldPosition(heatMapGridObject.x, heatMapGridObject.y) + new Vector3(gmc.grid.cellSize, gmc.grid.cellSize) * .5f;
            particleDestruction.gameObject.transform.position = new Vector2(particleDestruction.gameObject.transform.position.x, particleDestruction.gameObject.transform.position.y - 2f);
            particleDestruction.gameObject.SetActive(true);
            particleDestruction.Play();
        }
    }

    private void DelaySetCanPlace()
    {
        this.canPlace = true;
        colliderToolPreview.enabled = true;
    }

    public GameObject GetActualTool() { return this.actualToolSelected; }

    public void AtualizaBarras()
    {
        waveScore.AtualizaMediaBarras();
    }

    public void Deleta()
    {
        this.deleta = true;
       
    }
   
    
}
