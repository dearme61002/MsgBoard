﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MsgBoardWebApp.filter
{
    public class BacksideFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => true;

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            //登入API前 檢查代碼 代碼如下
            IEnumerable<string> key;
            if (!actionContext.Request.Headers.TryGetValues("key", out key))//檢查Headers參數是否正確
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            string keytext = key.First();
            token token = new token();

            if (token.isAdmin(keytext))
            {
                return await continuation();
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            return await continuation();
        }
    }
}