using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections;
using System;
using TestApp2.Models;

namespace ConsoleClient
{
    class ClientApi : IDisposable
    {
        private const string APP_PATH = "https://localhost:44311/";

        private HttpClient _client = new HttpClient();

        public int sent(BaseModel model)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = _client.PostAsJsonAsync(APP_PATH + "Api", model).Result;

                int StatusCode = (int)response.StatusCode;
                Console.WriteLine("Код ответа: {0}", StatusCode);
                return StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при выполнении запроса: {0}", ex.Message);
                return sent(model);
            }
        }

        public void Dispose()
        {
	        _client?.Dispose();
        }
    }
}
