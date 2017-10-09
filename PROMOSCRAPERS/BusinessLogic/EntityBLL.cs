using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using DataAccess;
using Utilities;

namespace BusinessLogic
{
    public static class EntityBLL
    {        

        public static bool InsertNewProduct(KotletaProduct prd)
        {
            
            return EntityDAL.InsertNewProduct(prd);
        }


        public static List<KotletaProduct> ProductList
        {
            get {

                if (CommonLibrary.CacheGet("PRODUCTSLIST") == null)
                    CommonLibrary.CacheInsert(EntityDAL.ProductList, "PRODUCTSLIST");
                return (List<KotletaProduct>)CommonLibrary.CacheGet("PRODUCTSLIST");
            }
            
        }
        
        public static bool InsertCouponCodesAndKeywords(int iProductId, List<CouponEntity> lCE, List<KeywordEntity> lke)
        {
            StringBuilder sbCoupons = new StringBuilder();
            bool bSuccess = false;

            int iTotalCouponCount = 1;
            int Isend = 1;


            //remove all product coupons
            bSuccess = EntityDAL.DeleteProductCoupons(iProductId);

            if (!bSuccess) return bSuccess;

            foreach (CouponEntity ce in lCE)
            {
                sbCoupons.Append(ce.CouponCode.Replace(",",".").Trim() + ",");

                if(Isend==50)
                {
                    bSuccess = EntityDAL.InsertCouponCodes(iProductId, sbCoupons.ToString().TrimEnd(','));

                    if (!bSuccess) break;
                    sbCoupons = new StringBuilder();
                    Isend = 0;
                }

                iTotalCouponCount++;
                Isend++;
            }

            if (!bSuccess) return bSuccess;

            if (sbCoupons.Length > 0)
                bSuccess = EntityDAL.InsertCouponCodes(iProductId, sbCoupons.ToString().TrimEnd(','));


            sbCoupons = new StringBuilder();

            int TotalNumCouponBasedOnpercent = 0;
            int TotalCalc = 0;
            int iCount = 1;

            foreach (KeywordEntity ke in lke)
            {

                if (ke.Percent == 100)
                {
                    TotalNumCouponBasedOnpercent = lCE.Count;
                }
                else
                {
                    if (iCount != lke.Count)
                        TotalNumCouponBasedOnpercent = int.Parse(Math.Floor(CommonLibrary.Percent(lCE.Count, ke.Percent)).ToString());
                    else
                        TotalNumCouponBasedOnpercent = lCE.Count - TotalCalc;
                }

                TotalCalc += TotalNumCouponBasedOnpercent;
                sbCoupons.Append(ke.KeyWord.Replace("{"," ").Trim() + "{" + ke.Percent.ToString().Replace("{", "").Trim() + "{" + TotalNumCouponBasedOnpercent.ToString() + ",");

                iCount++;
            }


            if (sbCoupons.Length > 0)
                bSuccess = EntityDAL.InsertKeywordsAndPercentages(iProductId, sbCoupons.ToString().TrimEnd(','));



            return bSuccess;
        }
    }
}
