﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace DW.ELA.ErrorReportingApi.Controllers
{
    [Route("api/[controller]")]
    public class ErrorsController : Controller
    {
        private readonly CloudStorageAccount storageAccount;

        public ErrorsController()
        {
            storageAccount = CloudStorageAccount.Parse("");
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value" + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            var table = tableClient.GetTableReference("people");

            //JsonSerializer serializer = new JsonSerializer();
            //    var @object = (JObject)serializer.Deserialize<>(value);
            //    OnNext(@object);
            //}

            //// Create the table if it doesn't exist.
            //await table.CreateIfNotExistsAsync();
            //var insertOperation = TableOperation.Insert();

            //// Execute the insert operation.
            //await table.ExecuteAsync(insertOperation);

            //return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
