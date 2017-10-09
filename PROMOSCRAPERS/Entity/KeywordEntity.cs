using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class KeywordEntity
    {
        int _ID; public int ID { get { return _ID; } set { _ID = value; } }
        string _KeyWord; public string KeyWord { get { return _KeyWord; } set { _KeyWord = value; } }
        int _Percent; public int Percent { get { return _Percent; } set { _Percent = value; } }
        int _totalcouponnum; public int TotalCouponNumber { get { return _totalcouponnum; } set { _totalcouponnum = value; } }
    }
}
