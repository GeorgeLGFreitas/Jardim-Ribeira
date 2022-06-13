using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class SaveSystem 
{
    private const string SAVE_EXTENSION = "txt";

    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/StreamingAssets/"; //+ "Saves/"; 
    private static bool isInit = false;
    
    
    //Verifica se a pasta existe
    public static void Init()
    {
        if(!isInit)
        {
            isInit = true;
            // Verifica se a pasta existe
            if(!Directory.Exists(SAVE_FOLDER))
            {
                //Cria pasta de SAVE
                Directory.CreateDirectory(SAVE_FOLDER);
            }
        }
    }

    //Salva e da overwrite se nescessario
    public static void Save(string fileName, string saveString, bool overwrite)
    {
        //Init();
        string saveFileName = fileName;
        if(!overwrite)
        {
            //O numero do save tem que ser unico para não sobrecrever o save antigo
            int saveNumber = 1;
            while (File.Exists(SAVE_FOLDER + saveFileName + "." + SAVE_EXTENSION))
            {
                saveNumber++;
                saveFileName = fileName + "_" + saveNumber;
            }
            //saveFileName é unico
        }
        File.WriteAllText(SAVE_FOLDER + saveFileName + "." + SAVE_EXTENSION, saveString);
    }
    
    //Da load em um arquivo especifico
    public static string Load(string fileName)
    {
        //Init();
        if(File.Exists(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION);
            return saveString;
        }
        else
        {
            return null;
        }
    }

    //Da load para o arquivo mais recente
    public static string LoadMostRecentFile()
    {
        //Init();
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        //Pega todos os arquivos salvos
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        // Passa por todos os arquivos salvos e idintifica o mais recente
        FileInfo mostRecentFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            if(mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }
            else
            {
                if(fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }

        //Se tiver algum arquivo de save , carregar o arquivo, se não retorna nulo
        if(mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;

        }
        else
        {
            return null;
        }
    }

    public static void SaveObject (object saveObject)
    {
        SaveObject("save", saveObject, false);
    }

    public static void SaveObject(string fileName, object saveObject, bool overwrite)
    {
        //Init();
        string json = JsonUtility.ToJson(saveObject);
        Save(fileName, json, overwrite);
    }

    public static TSaveObject LoadMostRecentObject<TSaveObject>()
    {
        //Init();
        string saveString = LoadMostRecentFile();
        if(saveString != null)
        {
            TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
            return saveObject;
        }
        else
        {
            return default(TSaveObject);
        }
    }

    public static TSaveObject LoadtObject<TSaveObject>(string fileName)
    {
        //Init();
        string saveString = Load(fileName);
        if(saveString != null)
        {
            TSaveObject saveObject = JsonUtility.FromJson<TSaveObject>(saveString);
            return saveObject;
        }
        else
        {
            return default(TSaveObject);
        }
    }

}
