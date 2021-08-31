using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager
{
    public const string dbDomain = "kh9413.dothome.co.kr/";

    private const string _downloadFoodListURL = dbDomain + "DownloadFoodList.php";
    private const string _uploadFoodListURL = dbDomain + "UploadFoodList.php";

    string _id = "";

    FoodCollector _foodCollector = null;
    WaitForSeconds _uploadWaitTime = new WaitForSeconds(60.0f);

    string _prevFoodListstr = "";

    bool _isDownload = true;

    public void Init(string id)
    {
        _id = id;

    }

    public IEnumerator FoodCollectionUpload()
    {
        while (_foodCollector == null)
        {
            _foodCollector = GameManager.Instance.FoodCollector;
            yield return null;
        }

        if (!_isDownload)
        {
            FoodManager foodManager = GameManager.Instance.FoodManager;

            int foodcount = 0;
            char[] foodList = new char[foodManager.Foods.Count + 1];
            int i;

            for (i = 0; i < foodList.Length; ++i)
            {
                foodList[i] = '0';
            }

            for (i = 0; i < _foodCollector.CollectFoodList.Count; ++i)
            {
                ++foodcount;
                foodList[_foodCollector.CollectFoodList[i].index] = '1';
            }


            string foodListstr = new string(foodList);
            if (_prevFoodListstr != foodListstr)
            {
                _prevFoodListstr = foodListstr;

                WWWForm form = new WWWForm();
                form.AddField("UserID", _id);
                form.AddField("FoodCount", foodcount);
                form.AddField("List", foodListstr);

                UnityWebRequest webrequest = UnityWebRequest.Post(_uploadFoodListURL, form);
                yield return webrequest.SendWebRequest();

                if (webrequest.downloadHandler.text != "true")
                {
                    LogManager.Log("DB UpLoadData Error : " + webrequest.downloadHandler.text);
                }
            }
        }
        yield return null;
    }

    public IEnumerator FoodCollectionDownload()
    {
        while (_foodCollector == null)
        {
            _foodCollector = GameManager.Instance.FoodCollector;
            yield return null;
        }

        _isDownload = true;

        WWWForm form = new WWWForm();
        form.AddField("UserID", _id);

        UnityWebRequest webrequest = UnityWebRequest.Post(_downloadFoodListURL, form);
        yield return webrequest.SendWebRequest();

        var str = webrequest.downloadHandler.text.Split(',');
        if (str[0] == "true")
        {
            int foodCount = int.Parse(str[1]);
            _prevFoodListstr = str[2];
            var foodList = str[2].ToCharArray();

            int count = 0;
            for (int i = 0; i < foodList.Length; ++i)
            {
                if (foodList[i] == '1')
                {
                    ++count;

                    _foodCollector.Collect(i);
                }
            }
        }
        else
        {
            LogManager.Log(webrequest.downloadHandler.text);
        }

        _isDownload = false;

        yield return null;
    }
}
