using System;
using System.Net.Http;
using System.Text;

namespace UpdateD
{
    public class Update
    {
        public string[]? after;

        public bool GetUpdate(string ID, string Ver)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                string url = $"http://2018k.cn/api/checkVersion?id={ID}&version={Ver}";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string pageData = response.Content.ReadAsStringAsync().Result;
                after = pageData.Split(new char[] { '|' });
                return after[0] == "true";
            }
        }

        public string GetUpdateRem(string ID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                string url = $"http://2018k.cn/api/getExample?id={ID}&data=remark";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string pageData = response.Content.ReadAsStringAsync().Result;
                return pageData;
            }
        }

        public string GetUpdateFile(string ID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                string url = $"http://2018k.cn/api/getExample?id={ID}&data=url";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string pageData = response.Content.ReadAsStringAsync().Result;
                return pageData;
            }
        }

        public string GetUpdateNotice(string ID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                string url = $"http://2018k.cn/api/getExample?id={ID}&data=notice";
                HttpResponseMessage response = client.GetAsync(url).Result;
                string pageData = response.Content.ReadAsStringAsync().Result;
                return pageData;
            }
        }
    }
}


