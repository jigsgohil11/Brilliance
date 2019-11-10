using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Brilliance.Infrastructure.DataProvider
{
    public class ProductDataProvider : BaseDataProvider, IProductDataProvider
    {
        public ServiceResponse GetProduct(Guid ProductID)
        {
            ProductViewModel ProductViewModel = new ProductViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ProductID" ,Value = Convert.ToString(ProductID)}
                };

                ProductViewModel = GetMultipleEntity<ProductViewModel>("GetProductdata", searchList);
                response.Data = ProductViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ProductListModel ProductList()
        {
            var productListModel = new ProductListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Product> products = GetEntityList<Product>("GetProductList", searchList);//Constants.GetUserGroupList
                if (products.Count > 0)
                {
                    productListModel.Productlist = products;
                    productListModel.Response.IsSuccess = true;
                }
                else
                {
                    productListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return productListModel;
        }
        public ServiceResponse SaveProducts(ProductViewModel Products)
        {
            var response = new ServiceResponse();
            try
            {


                if (Products.product.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProductID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", Products.product.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyID", Products.product.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProductTypeID", Products.product.ProductTypeID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProductCode", Products.product.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ProductName", Products.product.Name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Products.product.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Products.product.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Product", cmd);

                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProductID", Products.product.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", Products.product.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyID", Products.product.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProductTypeID", Products.product.ProductTypeID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProductCode", Products.product.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ProductName", Products.product.Name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Products.product.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Products.product.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Product", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Updated Successfully.";
                }

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
        public ServiceResponse DeleteProduct(Guid ProductID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (ProductID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "ProductID", Value = Convert.ToString(ProductID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteProduct", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}