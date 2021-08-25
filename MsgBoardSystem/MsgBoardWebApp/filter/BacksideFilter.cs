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
            //登入API前 檢查代碼 範例如下
            //IEnumerable<string> userNames;
            //if (!actionContext.Request.Headers.TryGetValues("UserName", out userNames))//檢查Headers參數是否正確
            //{
            //    return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //}
            //string userName = userNames.First();
            //if (userName == "amdin")
            //{
            //    return await continuation();
            //}
            //else
            //{
            //    return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            //}

            return await continuation();
        }
    }
}