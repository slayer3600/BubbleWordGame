using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;

public class WebAPI {

    public string Get(string URL)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        try
        {
            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            WebResponse errorResponse = ex.Response;
            using (Stream responseStream = errorResponse.GetResponseStream())
            {
                //StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                //string errorText = reader.ReadToEnd();
                //log errorText
            }
            throw;
        }
    }

}
