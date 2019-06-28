using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalSmartCoding.Views.Mains
{
    public partial class BlankView : Form
    {
        public BlankView()
        {
            InitializeComponent();
        }
    }
}



//#region HttpOAuth

//public static class HttpOAuth
//{
//    static HttpClient client = new HttpClient();

//    public static HttpResponseMessage TsaBarcodeRead(TsaBarcode tsaBarcode)
//    {
//        try
//        {
//            Thread.Sleep(168);
//            return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Fail!" };


//            //return RunAsync(tsaBarcode).GetAwaiter().GetResult();
//        }
//        catch (Exception e)
//        {
//            return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message };
//        }
//    }

//    public static HttpResponseMessage TsaBarcodeUpdate(TsaBarcode tsaBarcode)
//    {
//        try
//        {
//            Thread.Sleep(168);
//            return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "Fail!" };


//            return RunAsync(tsaBarcode).GetAwaiter().GetResult();
//        }
//        catch (Exception e)
//        {
//            return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message };
//        }
//    }

//    private static async Task<HttpResponseMessage> RunAsync(TsaBarcode tsaBarcode)
//    {
//        try
//        {
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            return await PutAsync(tsaBarcode);
//        }
//        catch (Exception e)
//        {
//            return new HttpResponseMessage() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message };
//        }
//    }


//    private static async Task<HttpResponseMessage> PutAsync(TsaBarcode tsaBarcode)
//    {
//        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, GetRequestURL("PUT", tsaBarcode));
//        httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(tsaBarcode.TsaLabel), Encoding.UTF8, "application/json");


//        HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
//        httpResponseMessage.EnsureSuccessStatusCode();
//        if (httpResponseMessage.IsSuccessStatusCode)
//        {
//            var a = await httpResponseMessage.Content.ReadAsStringAsync();
//            Console.WriteLine("\r\n" + "\r\n" + "\r\n" + "TSA: " + a);
//        }

//        return httpResponseMessage;
//    }

//    private static string GetRequestURL(string HTTP_Method, TsaBarcode tsaBarcode)
//    {
//        List<string> parameters = new List<string>() { "q_id1=" + tsaBarcode.Q_id1 };
//        OAuth_CSharp oauth_CSharp = new OAuth_CSharp(tsaBarcode.ConsumerKey, tsaBarcode.ConsumerSecret);
//        return oauth_CSharp.GenerateRequestURL(tsaBarcode.BaseUri, HTTP_Method, parameters);
//    }
//}
//#endregion