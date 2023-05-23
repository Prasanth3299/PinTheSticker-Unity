using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using SQLiteDatabase;
using Model;




public class DatabaseManager : MonoBehaviour
{
    private string connectionString;
    public static DatabaseManager databaseManager;
    SQLiteDB database = SQLiteDB.Instance;
    void Awake()
    {
        if (databaseManager != null)
            Object.Destroy(databaseManager);
        else
            databaseManager = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnApplicationQuit()
    {
        // release all resource using by database.
        database.Dispose();
    }

    // Events
    void OnEnable()
    {
        SQLiteEventListener.onError += OnError;
    }

    void OnDisable()
    {
        SQLiteEventListener.onError -= OnError;
    }

    void OnError(string err)
    {
        Debug.Log(err);
    }



    public static void InitializeDB()
    {
        try
        {
#if UNITY_EDITOR
            databaseManager.database.DBLocation = Application.streamingAssetsPath;
#else
            databaseManager.database.DBLocation = Application.persistentDataPath;
#endif
            // databaseManager.database.DBName = Application.streamingAssetsPath + "/PinTheSticker.db";



            databaseManager.database.DBName = "PinTheSticker.db";

            //  Debug.Log("Database Directory : " + databaseManager.database.DBLocation);
            //  databaseManager.database.CreateDatabase(databaseManager.database.DBName, true);

            if (databaseManager.database.Exists)
            {
                Debug.Log("Database Exists");

                databaseManager.database.ConnectToDefaultDatabase(databaseManager.database.DBName, false);
            }





        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
    }


    public static void CreateTables()
    {
        try
        {

            if (databaseManager.database.Exists)
            {

                DBSchema schema = new DBSchema("category");
                schema.AddField("categoryid", SQLiteDB.DB_DataType.DB_INT, 0, false, true, true);
                schema.AddField("categoryname", SQLiteDB.DB_DataType.DB_VARCHAR, 100, true, false, false);
                schema.AddField("categorycount", SQLiteDB.DB_DataType.DB_INT, 0, true, false, false);
                schema.AddField("unlockflag", SQLiteDB.DB_DataType.DB_INT, 0, true, false, false);
                bool created = databaseManager.database.CreateTable(schema);
                Debug.Log("table " + created);

            }

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
    }


    public static void InitilizeData()
    {
        try
        {

            databaseManager.InsertCategories();

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
    }

    private void InsertCategories()
    {
        try
        {
            database.Insert("INSERT INTO category(categoryid,categoryname,categorycount,unlockflag) values(1,'Eyes',15,0)");


        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
    }

    public static int GetAdsRemovedFlag()
    {
        try
        {

            DBReader reader = databaseManager.database.Select("settings");


            while (reader != null && reader.Read())
            {
                Debug.Log(reader.GetIntValue("isadsremoved"));

                return reader.GetIntValue("isadsremoved");

            }

            return 0;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
            return 0;

        }
    }

    public static void RemoveAds()
    {
        try
        {
            List<SQLiteDB.DB_DataPair> dataList = new List<SQLiteDB.DB_DataPair>();

            // data to be updated
            SQLiteDB.DB_DataPair data = new SQLiteDB.DB_DataPair();
            data.fieldName = "isadsremoved";
            data.value = "1";

            dataList.Add(data);

            // row to be updated
            SQLiteDB.DB_ConditionPair condition = new SQLiteDB.DB_ConditionPair();
            condition.fieldName = "isadsremoved";
            condition.value = "0";
            condition.condition = SQLiteDB.DB_Condition.EQUAL_TO;

            int i = databaseManager.database.Update("settings", dataList, condition);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

        }
    }


    public static List<Shop> GetShopsValues()
    {
        try
        {

            DBReader reader = databaseManager.database.Select("SELECT * from shop");

            List<Shop> shops = new List<Shop>();

            while (reader != null && reader.Read())
            {
                //Debug.Log(reader.GetIntValue("shopid"));

                Shop shop = new Shop();
                shop.shopid = reader.GetIntValue("shopid");
                shop.shopname = reader.GetStringValue("shopname");
                shop.categoryids = reader.GetStringValue("categoryids");
                shop.cost = reader.GetFloatValue("cost");
                shop.musicoritem = reader.GetIntValue("musicoritem");
                shop.androidinappid1 = reader.GetStringValue("androidinappid1");
                shop.androidinappid2 = reader.GetStringValue("androidinappid2");
                shop.iosinappid1 = reader.GetStringValue("iosinappid1");
                shop.iosinappid2 = reader.GetStringValue("iosinappid2");
                shop.unlockflag = reader.GetIntValue("unlockflag");

                shops.Add(shop);
            }

            return shops;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }


    public static void SetShopPurchaseSucess(Shop shop)
    {
        try
        {
            //update shop table
            //update shop table
            List<SQLiteDB.DB_DataPair> dataList = new List<SQLiteDB.DB_DataPair>();

            // data to be updated
            SQLiteDB.DB_DataPair data = new SQLiteDB.DB_DataPair();
            data.fieldName = "unlockflag";
            data.value = "1";

            dataList.Add(data);

            // row to be updated
            SQLiteDB.DB_ConditionPair condition = new SQLiteDB.DB_ConditionPair();
            condition.fieldName = "shopid";
            condition.value = (shop.shopid).ToString();
            condition.condition = SQLiteDB.DB_Condition.EQUAL_TO;
            int res = databaseManager.database.Update("shop", dataList, condition);

            //parse categoru ids

            System.String[] categoryArray = (shop.categoryids).Split('#');
            Debug.Log(categoryArray);
            //check music or items
            if (shop.musicoritem == 0)
            {//items


                for (int i = 0; i < categoryArray.Length; i++)
                {
                    // Debug.Log(categoryArray[i]);



                    List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                    data2.fieldName = "unlockflag";
                    data2.value = "1";

                    dataList2.Add(data2);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                    condition2.fieldName = "categoryid";
                    condition2.value = categoryArray[i];
                    condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res2 = databaseManager.database.Update("items", dataList2, condition2);


                    //Update jackpot
                    List<SQLiteDB.DB_DataPair> dataList3 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data3 = new SQLiteDB.DB_DataPair();
                    data3.fieldName = "unlockflag";
                    data3.value = "1";

                    dataList3.Add(data3);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition3 = new SQLiteDB.DB_ConditionPair();
                    condition3.fieldName = "jackpotid";
                    condition3.value = categoryArray[i];
                    condition3.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res3 = databaseManager.database.Update("jackpot", dataList3, condition3);

                }

            }
            else//music
            {
                for (int i = 0; i < categoryArray.Length; i++)
                {
                    List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                    data2.fieldName = "unlockflag";
                    data2.value = "1";

                    dataList2.Add(data2);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                    condition2.fieldName = "musicid";
                    condition2.value = categoryArray[i];
                    condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res2 = databaseManager.database.Update("music", dataList2, condition2);

                    /*//Update jackpot
                    List<SQLiteDB.DB_DataPair> dataList3 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data3 = new SQLiteDB.DB_DataPair();
                    data3.fieldName = "unlockflag";
                    data3.value = "1";

                    dataList3.Add(data3);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition3 = new SQLiteDB.DB_ConditionPair();
                    condition3.fieldName = "jackpotid";
                    condition3.value = categoryArray[i];
                    condition3.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res3 = databaseManager.database.Update("jackpot", dataList3, condition3);*/
                }
            }





        }

        catch (System.Exception e)
        {
            Debug.Log(e.Message);


        }
    }


    public static List<Music> GetMusicsValues()
    {
        try
        {

            DBReader reader = databaseManager.database.Select("SELECT * from music");

            List<Music> musics = new List<Music>();

            while (reader != null && reader.Read())
            {
                //Debug.Log(reader.GetIntValue("musicid"));

                Music music = new Music();
                music.musicid = reader.GetIntValue("musicid");
                music.musicindex = reader.GetIntValue("musicindex");
                music.musicname = reader.GetStringValue("musicname");
                music.unlockflag = reader.GetIntValue("unlockflag");

                musics.Add(music);
            }

            return musics;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }
    public static void SaveinMusicGallery(int musicindex)
    {
        try
        {
            string query = "INSERT INTO musicgallery(musicindex) VALUES(";

            query = query + musicindex + ")";

            databaseManager.database.Insert(query);

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    public static List<Gallery> GetGalleryValues()
    {
        try
        {

            DBReader reader = databaseManager.database.Select("SELECT * from musicgallery");

            List<Gallery> musics = new List<Gallery>();

            while (reader != null && reader.Read())
            {

                Gallery music = new Gallery();
                music.galleryid = reader.GetIntValue("galleryid");
                music.musicindex = reader.GetIntValue("musicindex");


                musics.Add(music);
            }

            return musics;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }

    public static void DeleteFromMusicGallery(int galleryid)
    {
        SQLiteDB.DB_ConditionPair condition = new SQLiteDB.DB_ConditionPair();
        // delete from Users where Id = id
        condition.fieldName = "galleryid";
        condition.value = galleryid.ToString();
        condition.condition = SQLiteDB.DB_Condition.EQUAL_TO;

        int r = databaseManager.database.DeleteRow("musicgallery", condition);
    }



    public static List<Items> GetItems(int categoryid)
    {
        try
        {
            string query = "SELECT * from items WHERE categoryid=" + categoryid;
            DBReader reader = databaseManager.database.Select(query);

            List<Items> items = new List<Items>();

            while (reader != null && reader.Read())
            {


                Items item = new Items();
                item.itemid = reader.GetIntValue("itemid");
                item.itemindex = reader.GetIntValue("itemindex");
                item.categoryid = reader.GetIntValue("categoryid");
                item.unlockflag = reader.GetIntValue("unlockflag");

                items.Add(item);
            }

            return items;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }


    public static List<Items> GetFreeSpinItems()
    {
        try
        {
            string query = "SELECT * from items WHERE unlockflag=0 ORDER BY random() LIMIT 8";
            DBReader reader = databaseManager.database.Select(query);

            List<Items> items = new List<Items>();

            while (reader != null && reader.Read())
            {


                Items item = new Items();
                item.itemid = reader.GetIntValue("itemid");
                item.itemindex = reader.GetIntValue("itemindex");
                item.categoryid = reader.GetIntValue("categoryid");
                item.unlockflag = reader.GetIntValue("unlockflag");

                Debug.Log(item.itemid);

                items.Add(item);
            }



            return items;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }



   

    public static List<Jackpot> GetPaySpinItems()
    {
        try
        {

            DBReader reader = databaseManager.database.Select("SELECT * from jackpot WHERE unlockflag=0 ORDER BY random() LIMIT 8");

            List<Jackpot> jackpots = new List<Jackpot>();

            while (reader != null && reader.Read())
            {
                //Debug.Log(reader.GetIntValue("shopid"));

                Jackpot jackpot = new Jackpot();
                jackpot.jackpotid = reader.GetIntValue("jackpotid");
                //jackpot.jackpotname = reader.GetStringValue("jackpotname");
                jackpot.categoryids = reader.GetStringValue("categoryids");
                //jackpot.cost = reader.GetFloatValue("cost");
                jackpot.musicoritem = reader.GetIntValue("musicoritem");
                // jackpot.androidinappid1 = reader.GetStringValue("androidinappid1");
                // jackpot.androidinappid2 = reader.GetStringValue("androidinappid2");
                // jackpot.iosinappid1 = reader.GetStringValue("iosinappid1");
                //jackpot.iosinappid2 = reader.GetStringValue("iosinappid2");
                //jackpot.unlockflag = reader.GetIntValue("unlockflag");
                Debug.Log(jackpot.jackpotid);
                jackpots.Add(jackpot);
            }

            return jackpots;

        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);

            return null;
        }
    }


    public static void SetJackpotPurchaseSucess(Jackpot jackpot)
    {
        try
        {
            //update shop table
            //update shop table
            List<SQLiteDB.DB_DataPair> dataList = new List<SQLiteDB.DB_DataPair>();

            // data to be updated
            SQLiteDB.DB_DataPair data = new SQLiteDB.DB_DataPair();
            data.fieldName = "unlockflag";
            data.value = "1";

            dataList.Add(data);

            // row to be updated
            SQLiteDB.DB_ConditionPair condition = new SQLiteDB.DB_ConditionPair();
            condition.fieldName = "jackpotid";
            condition.value = (jackpot.jackpotid).ToString();
            Debug.Log(condition.value);
            condition.condition = SQLiteDB.DB_Condition.EQUAL_TO;

            int res = databaseManager.database.Update("jackpot", dataList, condition);

            //parse categoru ids

            //System.String[] categoryArray = (jackpot.categoryids).Split('#');

           // Debug.Log(categoryArray);
            //check music or items
            if (jackpot.musicoritem == 0)
            {//items

                List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                // data to be updated
                SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                data2.fieldName = "unlockflag";
                data2.value = "1";

                dataList2.Add(data2);

                // row to be updated
                SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                condition2.fieldName = "categoryid";
                condition2.value = (jackpot.jackpotid).ToString();
                condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                int res2 = databaseManager.database.Update("items", dataList2, condition2);
                /*for (int i = 0; i < categoryArray.Length; i++)
                {
                    // Debug.Log(categoryArray[i]);



                    List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                    data2.fieldName = "unlockflag";
                    data2.value = "1";

                    dataList2.Add(data2);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                    condition2.fieldName = "categoryid";
                    condition2.value = categoryArray[i];
                    condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res2 = databaseManager.database.Update("items", dataList2, condition2);



                }*/

            }
            else//music
            {
                List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                // data to be updated
                SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                data2.fieldName = "unlockflag";
                data2.value = "1";

                dataList2.Add(data2);

                // row to be updated
                SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                condition2.fieldName = "musicid";
                condition2.value = (jackpot.jackpotid).ToString();
                condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                int res2 = databaseManager.database.Update("music", dataList2, condition2);
                /*for (int i = 0; i < categoryArray.Length; i++)
                {
                    List<SQLiteDB.DB_DataPair> dataList2 = new List<SQLiteDB.DB_DataPair>();

                    // data to be updated
                    SQLiteDB.DB_DataPair data2 = new SQLiteDB.DB_DataPair();
                    data2.fieldName = "unlockflag";
                    data2.value = "1";

                    dataList2.Add(data2);

                    // row to be updated
                    SQLiteDB.DB_ConditionPair condition2 = new SQLiteDB.DB_ConditionPair();
                    condition2.fieldName = "musicid";
                    condition2.value = categoryArray[i];
                    condition2.condition = SQLiteDB.DB_Condition.EQUAL_TO;

                    int res2 = databaseManager.database.Update("music", dataList2, condition2);

                }*/
            }





        }

        catch (System.Exception e)
        {
            Debug.Log(e.Message);


        }
    }


}
