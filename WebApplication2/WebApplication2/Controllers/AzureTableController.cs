using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AzureTableController : Controller
    {
        // GET: AzureTable
        public ActionResult Index()
        {
            UsersList();
            return View();
        }
        [HttpPost]
    public ActionResult Index(String Id,string Name)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=magnetto;AccountKey=ZPpQmguO9Ds7xoXjjwvsmU7k0VDRPB24u4EjaK8Eo6fg0xaqrSqTiyvj4k1YbpjQsOXFrZsXQeevtGbR6L1Yqg==;");

            CloudTableClient client = account.CreateCloudTableClient();

            CloudTable table = client.GetTableReference("prash");

            table.CreateIfNotExists();
            TableOperation operation = TableOperation.Insert(new UserEntity().CreateUser( Id , Name));

            table.Execute(operation);

            UsersList();
            return View();
        }




        [HttpPost]
        public ActionResult Delete(String RowKey, string PartitionKey)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=magnetto;AccountKey=ZPpQmguO9Ds7xoXjjwvsmU7k0VDRPB24u4EjaK8Eo6fg0xaqrSqTiyvj4k1YbpjQsOXFrZsXQeevtGbR6L1Yqg==;");

            CloudTableClient client = account.CreateCloudTableClient();

            CloudTable table = client.GetTableReference("prash");

            TableOperation operation = TableOperation.Retrieve<UserEntity>(PartitionKey, RowKey);

            TableResult isUser = table.Execute(operation);

            UserEntity user = isUser.Result as UserEntity;

            if(user!=null)
            {
                TableOperation deleteoperation = TableOperation.Delete(user);

                table.Execute(deleteoperation);
            }
           
            return RedirectToAction("Index");
        }


        public void UsersList()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=magnetto;AccountKey=ZPpQmguO9Ds7xoXjjwvsmU7k0VDRPB24u4EjaK8Eo6fg0xaqrSqTiyvj4k1YbpjQsOXFrZsXQeevtGbR6L1Yqg==;");

            CloudTableClient client = account.CreateCloudTableClient();

            CloudTable table = client.GetTableReference("prash");

            TableQuery<UserEntity> query = new TableQuery<UserEntity>();

            ViewBag.userList = table.ExecuteQuery(query);


        }
    }
}