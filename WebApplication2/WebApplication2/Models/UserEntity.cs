using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace WebApplication2.Models
{
    public class UserEntity:TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public UserEntity()
        {

        }

        public UserEntity CreateUser(string Id,string Name)
        {
            this.Id = Id;
            this.Name = Name;
            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = DateTime.Now.Year.ToString();

            return this;
        }

        internal ITableEntity CreateUser(object id, object name)
        {
            throw new NotImplementedException();
        }
    }
}