using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace InstitutionalMVC.Helper
{
    public class AddJsonLanguage
    {
        private string _filePath;
        private readonly IDistributedCache _cache;
        public AddJsonLanguage(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task TrSetLanguage(string key , string value) 
        {
            try
            {
                _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\tr-TR.json");
                 await  SetLanguage(key,value,_filePath);
            }
            catch (Exception ex) 
            {
                
            }
        }
        public async Task EnSetLanguage(string key, string value)
        {
            try
            {
                _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\en-US.json");
               await SetLanguage(key, value, _filePath);
            }
            catch (Exception ex) { }
        }
        public async Task UpdateLangue(string key, string trValue, string enValue)
        {
            string trLangue = await GetValue(key, "tr-TR");
            string enLangue = await GetValue(key, "en-US");
            if (!string.IsNullOrEmpty(trLangue) && trLangue != trValue ||  !string.IsNullOrEmpty(enLangue) && enLangue != enValue && !string.IsNullOrEmpty(key))
            {
               await DeleteTrLanguage(trLangue);
               await DeleteEnLanguage(trLangue);
               await TrSetLanguage(trValue, trValue);
              await  EnSetLanguage(trValue, enValue);
            }
            else if(!string.IsNullOrEmpty(key)&& !string.IsNullOrEmpty(trValue)&& !string.IsNullOrEmpty(enValue)&& string.IsNullOrEmpty(trLangue)&& string.IsNullOrEmpty(enLangue))
            {
              await  TrSetLanguage(key, trValue);
              await  EnSetLanguage(key, enValue);
            }
        }
        public async Task SetLanguage(string key , string value ,string filePath) 
        {

            string urlReuest = "https://localhost:7083/" + "File/LanguageUpload";
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(key), "key");
            content.Add(new StringContent(value), "value");
            content.Add(new StringContent(filePath), "filePath");
            HttpResponseMessage responce = await Extancion.Client.PostAsync(urlReuest, content);
            //post
            //string json = System.IO.File.ReadAllText(filePath);
            //dynamic jsonObj = JsonConvert.DeserializeObject(json);

            //var sectionPath = key.Split(":")[0];
            //if (!string.IsNullOrEmpty(sectionPath))
            //{
            //    //string relativeFilePath = System.IO.File.ReadAllText(_filePath);
            //    string fullFilePath = _filePath;
            //    if (System.IO.File.Exists(fullFilePath))
            //    {
            //        string deger = jsonObj[sectionPath];
            //        if (string.IsNullOrEmpty(deger)) jsonObj[sectionPath] = value;
            //    }                    //var keyPath = key.Split(":")[1];
            //}
            //else
            //    jsonObj[sectionPath] = value;

            //string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            //System.IO.File.WriteAllText(_filePath, output);
        }
        public async Task<string> GetValue(string key,string langue) 
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources\\{langue}.json");
            string json = System.IO.File.ReadAllText(_filePath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);
            string deger = "";
            var sectionPath = key.Split(":")[0];
            if (!string.IsNullOrEmpty(sectionPath))
            {
                //string relativeFilePath = System.IO.File.ReadAllText(_filePath);
                string fullFilePath = _filePath;
                if (System.IO.File.Exists(fullFilePath))
                {
                    deger = jsonObj[sectionPath];
                    if (string.IsNullOrEmpty(deger)) return deger;
                }
                return deger;//var keyPath = key.Split(":")[1];
            }
            else
                return "";
        }
        public async Task DeleteLanguage(string key) 
        {

          await  DeleteLanguageAll(key,"en-US");
          await  DeleteLanguageAll(key, "tr-TR");

        }
        public async Task DeleteTrLanguage(string key)
        {
          await  DeleteLanguageAll(key, "tr-TR");
        }
        public async Task DeleteEnLanguage(string key)
        {
           await DeleteLanguageAll(key, "en-US");
        }
        public async Task DeleteLanguageAll(string key ,string filePath) 
        {
            string urlReuest = "https://localhost:7083/" + "File/LanguageDelete";
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(key), "key");
            content.Add(new StringContent(filePath), "filePath");
            HttpResponseMessage responce = await Extancion.Client.PostAsync(urlReuest, content);


            //postrequest
            //_filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Resources\\{filePath}.json");
            //string json = System.IO.File.ReadAllText(_filePath);
            //dynamic jsonObj = JsonConvert.DeserializeObject(json);

            //var sectionPath = key.Split(":")[0];
            //if (!string.IsNullOrEmpty(sectionPath))
            //{
            //    //string relativeFilePath = System.IO.File.ReadAllText(_filePath);
            //    string fullFilePath = _filePath;
            //    if (System.IO.File.Exists(fullFilePath))
            //    {
            //        string deger = jsonObj[sectionPath];
            //        if (!string.IsNullOrEmpty(deger))
            //        {
            //            JProperty idProp = jsonObj.Property(key);
            //            idProp.Remove();
            //            string updatedJson = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            //            File.WriteAllText(_filePath, updatedJson);
            //        }
            //    }
            //}
        }
    }
}
