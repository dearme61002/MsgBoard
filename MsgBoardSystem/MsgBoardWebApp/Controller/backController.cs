﻿using databaseORM.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MsgBoardWebApp
{
    public class backController : ApiController
    {
        // GET api/<controller>[TypeFilter(type)]
      
        public List<databaseORM.data.ErrorLog> Get()
        {
           using (databaseEF context = new databaseEF())
            {
                List<databaseORM.data.ErrorLog> cc =  context.ErrorLogs.ToList();
                return cc;
            }
           
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}