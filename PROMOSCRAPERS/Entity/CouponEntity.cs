using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class CouponEntity
    {
        int _ID; public int ID { get { return _ID; } set { _ID = value; } }
        string _CouponCode; public string CouponCode { get { return _CouponCode; } set { _CouponCode = value; } }

    }
}
