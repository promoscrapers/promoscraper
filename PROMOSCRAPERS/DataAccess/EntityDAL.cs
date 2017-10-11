using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using Entity;
using Utilities.Data;

namespace DataAccess
{
    public static class EntityDAL
    {


        public static bool DeleteProductCoupons(int iProdID)
        {
            try { 
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter parm = null;

            parm = new SqlParameter("prodid", System.Data.SqlDbType.Int);
            parm.Value = iProdID;
            list.Add(parm);

            SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
            parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
            list.Add(parm_ReturnMessage);

            SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "delete_previous_coupons", list.ToArray());
            string sReturnMsg = parm_ReturnMessage.Value.ToString();

            if (sReturnMsg == "OK")
                return true;
            else
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool InsertCouponCodes(int iProductId, string sCommaCouponCodes)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter parm = null;


                parm = new SqlParameter("CommaStringCouponCodes", System.Data.SqlDbType.VarChar);
                parm.Value = sCommaCouponCodes;
                list.Add(parm);


                parm = new SqlParameter("ProductID", System.Data.SqlDbType.Int);
                parm.Value = iProductId;
                list.Add(parm);               


                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
                list.Add(parm_ReturnMessage);

                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "Insert_Coupon_Codes", list.ToArray());
                string sReturnMsg = parm_ReturnMessage.Value.ToString();

                if (sReturnMsg == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool InsertKeywordsAndPercentages(int iProductId, string sKeywordPercentageValues)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter parm = null;


                parm = new SqlParameter("CommaStringValues", System.Data.SqlDbType.VarChar);
                parm.Value = sKeywordPercentageValues;
                list.Add(parm);


                parm = new SqlParameter("ProductID", System.Data.SqlDbType.Int);
                parm.Value = iProductId;
                list.Add(parm);


                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
                list.Add(parm_ReturnMessage);

                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "Insert_KEYWORDS_Percentages", list.ToArray());
                string sReturnMsg = parm_ReturnMessage.Value.ToString();

                if (sReturnMsg == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public static bool InsertNewProduct(KotletaProduct prd)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter parm = null;

                parm = new SqlParameter("AmazonASIN", System.Data.SqlDbType.VarChar);
                parm.Value = prd.AmazonASIN;
                list.Add(parm);

                parm = new SqlParameter("ProductName", System.Data.SqlDbType.VarChar);
                parm.Value = prd.Name;
                list.Add(parm);


                parm = new SqlParameter("ProductImageURL", System.Data.SqlDbType.VarChar);
                parm.Value = prd.ImageURL;
                list.Add(parm);

                parm = new SqlParameter("Price", System.Data.SqlDbType.Money);
                parm.Value = prd.Price;
                list.Add(parm);

                parm = new SqlParameter("CategoryId", System.Data.SqlDbType.BigInt);
                parm.Value = prd.Category;
                list.Add(parm);

                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
                list.Add(parm_ReturnMessage);

                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "insert_new_product", list.ToArray());
                string sReturnMsg = parm_ReturnMessage.Value.ToString();

                if (sReturnMsg == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool InsertProductCategories(string CategoryId,string CategoryName,string ParentId)
        {
            try
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter parm = null;

                parm = new SqlParameter("CategoryId", System.Data.SqlDbType.BigInt);
                parm.Value = CategoryId;
                list.Add(parm);

                parm = new SqlParameter("CategoryName", System.Data.SqlDbType.VarChar);
                parm.Value = CategoryName;
                list.Add(parm);


                parm = new SqlParameter("ParentId", System.Data.SqlDbType.BigInt);
                parm.Value = ParentId;
                list.Add(parm);

                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
                list.Add(parm_ReturnMessage);

                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "insert_new_Category", list.ToArray());
                string sReturnMsg = parm_ReturnMessage.Value.ToString();

                if (sReturnMsg == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static List<KotletaProduct> ProductList
        {
            get
            {
                List<KotletaProduct> lst = new List<KotletaProduct>();
                KotletaProduct SD = new KotletaProduct();

                using (SqlDataReader dr = SqlHelper.ExecuteReader(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "get_list_of_products"))
                {
                    while (dr.Read())
                    {
                        SD.ID = int.Parse(dr["ID"].ToString());
                        SD.Name = dr["Name"].ToString();
                        SD.Price = decimal.Parse(dr["OriginalPrice"].ToString());
                        SD.ImageURL = dr["ImageURL"].ToString();
                        SD.isActive = bool.Parse(dr["isActive"].ToString());
                        SD.DateTimeWhen = DateTime.Parse(dr["DateTimeAdded"].ToString());
                        SD.AmazonASIN = dr["AmazonASIN"].ToString();
                        lst.Add(SD);
                        SD = new KotletaProduct();
                    }

                    dr.Close();
                }

                return lst;
            }
        }


    }



    //        public List<SiteDisplay> SiteDisplayList
    //        {
    //            get
    //            {
    //                List <SiteDisplay> lst = new List<SiteDisplay>();
    //                SiteDisplay SD = new SiteDisplay();


    //                using (SqlDataReader dr = SqlHelper.ExecuteReader(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "prc_get_site_list"))
    //                {
    //                    while (dr.Read())
    //                    {
    //                        SD.ID = int.Parse(dr["ID"].ToString());
    //                        SD.SITENAME = dr["SITENAME"].ToString();
    //                        SD.URLWHEREDISPLAYED = dr["URLWHEREDISPLAYED"].ToString();
    //                        SD.DateTimeWhen = DateTime.Parse(dr["datetimewhen"].ToString());
    //                        lst.Add(SD);
    //                        SD = new SiteDisplay();
    //                    }

    //                    dr.Close();
    //                }

    //                return lst;

    //            }
    //        }

    //        public bool UpdateAmazonProductSIteBitlyURL(int ROWID, string sBitlyURL)
    //        {
    //            try
    //            {

    //                List<SqlParameter> list = new List<SqlParameter>();
    //                SqlParameter parm = null;

    //                parm = new SqlParameter("ROWID", System.Data.SqlDbType.Int);
    //                parm.Value = ROWID;
    //                list.Add(parm);

    //                parm = new SqlParameter("BitlyURL", System.Data.SqlDbType.VarChar, 100);
    //                parm.Value = sBitlyURL;
    //                list.Add(parm);

    //                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
    //                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
    //                list.Add(parm_ReturnMessage);

    //                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "prc_update_bitly_url", list.ToArray());
    //                string sReturnMsg = parm_ReturnMessage.Value.ToString();

    //                if (sReturnMsg == "OK")
    //                    return true;
    //                else
    //                    return false;
    //            }
    //            catch (Exception ex)
    //            {
    //                return false;
    //            }
    //}


    //        public List<AMAZONPRODUCTSITEENTITY> GetAmazonProductSiteEntity
    //        {
    //            get
    //            {
    //                List<AMAZONPRODUCTSITEENTITY> lst = new List<AMAZONPRODUCTSITEENTITY>();
    //                AMAZONPRODUCTSITEENTITY SD = new AMAZONPRODUCTSITEENTITY();


    //                using (SqlDataReader dr = SqlHelper.ExecuteReader(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "prc_get_site_amazon"))
    //                {
    //                    while (dr.Read())
    //                    {
    //                        SD.ID = int.Parse(dr["ROWID"].ToString());
    //                        SD.SITEID = int.Parse(dr["SITEID"].ToString());
    //                        SD.AMAZONPRODUCTID = int.Parse(dr["AMAZONPRODUCTID"].ToString());
    //                        SD.SITENAME = dr["SITENAME"].ToString();
    //                        SD.SITEURLWHERETODISPLAY = dr["SITEURLWHERETODISPLAY"].ToString();
    //                        SD.AMAZONPRODUCTNAME = dr["AMAZONPRODUCTNAME"].ToString();
    //                        SD.BITLYURLTODISPLAY = dr["BITLYURLTODISPLAY"].ToString();
    //                        SD.DATETIMEWHEN = DateTime.Parse(dr["datetimewhen"].ToString());
    //                        SD.AMAZONASIN = dr["AMAZONASIN"].ToString();
    //                        SD.AMAZONURL = dr["AMAZONURL"].ToString();
    //                        SD.IMAGELOCATION = dr["IMAGELOCATION"].ToString();
    //                        SD.AMAZBRANDNAME = dr["AMAZBRANDNAME"].ToString();
    //                        lst.Add(SD);
    //                        SD = new AMAZONPRODUCTSITEENTITY();
    //                    }

    //                    dr.Close();
    //                }

    //                return lst;

    //            }
    //        }

    //        public bool InsertPromoCode(DiscountCode dCode)
    //        {
    //            try
    //            {
    //                List<SqlParameter> list = new List<SqlParameter>();
    //                SqlParameter parm = null;

    //                parm = new SqlParameter("promocode", System.Data.SqlDbType.VarChar, 500);
    //                parm.Value = dCode.DiscCode.Trim();
    //                list.Add(parm);

    //                parm = new SqlParameter("discountpercent", System.Data.SqlDbType.Int);
    //                parm.Value = dCode.DiscountPercent;
    //                list.Add(parm);

    //                parm = new SqlParameter("expdate", System.Data.SqlDbType.DateTime);
    //                parm.Value = dCode.ExpirationDateTimeDT;
    //                list.Add(parm);

    //                parm = new SqlParameter("AmazBrandName", System.Data.SqlDbType.VarChar, 50);
    //                parm.Value = dCode.AmazBrandName;
    //                list.Add(parm);



    //                SqlParameter parm_ReturnMessage = new SqlParameter("oReturnMessage", System.Data.SqlDbType.VarChar, 50);
    //                parm_ReturnMessage.Direction = System.Data.ParameterDirection.Output;
    //                list.Add(parm_ReturnMessage);

    //                SqlHelper.ExecuteScalar(DbConnection.GetConnectionString(), System.Data.CommandType.StoredProcedure, "prc_insert_promo_code", list.ToArray());
    //                string sReturnMsg = parm_ReturnMessage.Value.ToString();

    //                if (sReturnMsg == "OK")
    //                    return true;
    //                else
    //                    return false;
    //            }
    //            catch (Exception ex)
    //            {
    //                return false;
    //            }
    //        }
    //    }
}
