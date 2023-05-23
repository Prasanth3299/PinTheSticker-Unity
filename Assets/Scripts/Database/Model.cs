
using System.Collections;
using System.Collections.Generic;
using System;

namespace Model
{
   
    public class Shop
    {
        public int shopid;
        public string shopname;
        public string categoryids;
        public float cost;
        public int musicoritem;
        public string androidinappid1;
        public string androidinappid2;
        public string iosinappid1;
        public string iosinappid2;
        public int unlockflag;
    }
    public class Jackpot
    {
        public int jackpotid;
        public string jackpotname;
        public string categoryids;
        public float cost;
        public int musicoritem;
        public string androidinappid1;
        public string androidinappid2;
        public string iosinappid1;
        public string iosinappid2;
        public int unlockflag;
    }



    public class  Music
    {
        public int musicid;
        public string musicname;
        public int musicindex;
        public int unlockflag;
    }

    public class Gallery
    {
        public int galleryid;
        public int musicindex;
    }

    public class Items
    {
        public int itemid;
        public int itemindex;
        public int categoryid;
        public int unlockflag;
    }
}