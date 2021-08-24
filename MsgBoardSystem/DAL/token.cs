using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{/// <summary>
/// 用來加密解密密碼
/// </summary>
  public  class token
    {
        /// <summary>
        /// 加密:傳入一組字典型的參數我回傳一串加密的String參數
        /// </summary>
        /// <param name="keyValuePairs">打算加密的參數</param>
        /// <returns>以加密的字串</returns>
        public string encode(Dictionary<string,object> keyValuePairs)
        {
            var secret = "wearediediiebutlivelive&&@@dd";//加密與解密密碼不能洩漏
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder base64UrlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, base64UrlEncoder);
            string token = encoder.Encode(keyValuePairs, secret);//開始加密
            return token;
        }
        /// <summary>
        /// 解密:傳入一串加密的參數我回傳一串解密的String參數的json格;無法解密或失敗傳遞string.Empty值
        /// </summary>
        /// <param name="myToken">要解密的參數</param>
        /// <returns>已解密的字串</returns>
        public string decode(string myToken)
        {
            var secret = "wearediediiebutlivelive&&@@dd";//加密與解密密碼不能洩漏
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var json = decoder.Decode(myToken, secret, verify: true);
                return json;

            }catch(TokenExpiredException)
            {
                Console.WriteLine("token has expired");
                return string.Empty;
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
                return string.Empty;
            }
            catch (Exception)
            {
                Console.WriteLine("other error");
                return string.Empty;
            }
        }
    }
}
