using DAL;
using Newtonsoft.Json.Linq;
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
            string decodedate= token.decode(keytext);//解碼

            if (decodedate == String.Empty) //解密失敗或過期
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

            JObject myjsondecodeData = JObject.Parse(decodedate);//解json
            string mydecodeKey = myjsondecodeData["UserID"].ToString();
            if (token.isAdmin(mydecodeKey))
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