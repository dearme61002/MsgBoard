using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SystemAuth
{
    /// <summary>對密碼做ASE加密演算法</summary>
    /// 程式資料來源(Source) : https://dotblogs.com.tw/shadow/2019/03/20/232821
    public class PasswordAESCryptography
    {
        private static string key = "DefaultKeyAndIV0";
        private static string iv = "DefaultKeyAndIV0";

        /// <summary> 檢查Key跟IV長度是否符合ASE加密規範 </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        private static void Validate_KeyIV_Length(string key, string iv)
        {
            // 規範Key, IV bits長度
            List<int> LegalSizes = new List<int>() { 128, 192, 256 };
            int keyBitSize = Encoding.UTF8.GetBytes(key).Length * 8;
            int ivBitSize = Encoding.UTF8.GetBytes(iv).Length * 8;
            // 檢查Key, IV
            if (!LegalSizes.Contains(keyBitSize) || !LegalSizes.Contains(ivBitSize))
            {
                throw new Exception($@"key或iv的長度不在128bits、192bits、256bits其中一個，輸入的key bits:{keyBitSize},iv bits:{ivBitSize}");
            }
        }

        /// <summary> 加密後回傳base64String </summary>
        /// <param name="plain_text"></param>
        /// <returns>Encrypt string(base64)</returns>
        public static string Encrypt(string plain_text)
        {
            Validate_KeyIV_Length(key, iv);
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;//非必須，但加了較安全
            aes.Padding = PaddingMode.PKCS7;//非必須，但加了較安全

            ICryptoTransform transform = aes.CreateEncryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

            byte[] bPlainText = Encoding.UTF8.GetBytes(plain_text);//明碼文字轉byte[]
            byte[] outputData = transform.TransformFinalBlock(bPlainText, 0, bPlainText.Length);//加密
            return Convert.ToBase64String(outputData);
        }

        /// <summary> 解密後，回傳明碼文字</summary>
        /// <param name="base64String"></param>
        /// <returns>Decrypt string</returns>
        public static string Decrypt(string base64String)
        {
            Validate_KeyIV_Length(key, iv);
            Aes aes = Aes.Create();
            aes.Mode = CipherMode.CBC;//非必須，但加了較安全
            aes.Padding = PaddingMode.PKCS7;//非必須，但加了較安全

            ICryptoTransform transform = aes.CreateDecryptor(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            byte[] bEnBase64String = null;
            byte[] outputData = null;
            try
            {
                bEnBase64String = Convert.FromBase64String(base64String);//有可能base64String格式錯誤
                outputData = transform.TransformFinalBlock(bEnBase64String, 0, bEnBase64String.Length);//有可能解密出錯
            }
            catch (Exception ex)
            {
                //todo 寫Log
                throw new Exception($@"解密出錯:{ex.Message}");
            }

            //解密成功
            return Encoding.UTF8.GetString(outputData);
        }
    }
}
