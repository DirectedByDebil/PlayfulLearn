using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Extensions;

namespace Web
{
    public sealed class RestService
    {

        private readonly HttpClient _client;

        private readonly UriBuilder _uriBuilder;

        private readonly Encoding _encoding;


        private const string KeysPath = "/getKeys/";
        
        private const string RegistrationPath = "/register/";


        private const string AuthKeysPath = "/getAuthKeys/";
        
        private const string AuthPath = "/auth/";



        public RestService()
        {

            _client = new HttpClient();

            _uriBuilder = new UriBuilder("http", "127.0.0.1", 8888);


            _encoding = Encoding.UTF8;
        }


        public async Task<HttpStatusCode> RegisterAsync(FormRegistrationFields data)
        {

            IdKeys keys = await GetAsync<IdKeys>(KeysPath);

          
            SendRegistrationFields send = new()
            {

                UserName = data.UserName,

                Email = data.Email,

                DateTime = keys.DateTime,

                Password = Crypto.GetHash(keys, data.Password)
            };


            HttpResponseMessage message = await PostAsync(RegistrationPath, send);

            return message.StatusCode;
        }


        public async Task<HttpStatusCode> LoginAsync(FormLoginFields data)
        {

            HttpResponseMessage keysMessage = await PostAsync(AuthKeysPath, data.Email);


            string keysJson = await keysMessage.Content.ReadAsStringAsync();


            IdKeys keys = JsonUtility.FromJson<IdKeys>(keysJson);


            SendLoginFields send = new()
            {

                Email = data.Email,

                DateTime = keys.DateTime,

                Password = Crypto.GetHash(keys, data.Password)
            };


            HttpResponseMessage message = await PostAsync(AuthPath, send);


            return message.StatusCode;
        }


        public async Task<T> GetAsync<T>(string path)
        {

            _uriBuilder.Path = path;

            Uri uri = _uriBuilder.Uri;


            HttpResponseMessage message = await _client.GetAsync(uri);


            if(message.IsSuccessStatusCode)
            {

                string json = await message.Content.ReadAsStringAsync();

                return JsonUtility.FromJson<T>(json);
            }


            return default;
        }


        public async Task<HttpResponseMessage> PostAsync<T>(string path,
            
            T obj)
        {

            _uriBuilder.Path = path;

            Uri uri = _uriBuilder.Uri;


            string json = JsonUtility.ToJson(obj);

            ByteArrayContent content = new (_encoding.GetBytes(json));


            HttpResponseMessage message = await _client.PostAsync(uri, content);

            return message;
        }
    }
}
