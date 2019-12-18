using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestAsynAwat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        public async Task<List<string>> Get()
        {
            //Log
            await Logdata();
            var data = await GetData();
            await Logdata();
            //Log
            return data;
        }

        [HttpGet("{id}")]
        public async Task<List<string>> GetWithoutAwait(int id)
        {
            //Log
            Logdata();
            var data = await GetData();
            Logdata();
            //Log
            return data;
        }

        private async Task Logdata()
        {
            string connString = "Server=tcp:test2035.database.windows.net,1433;Initial Catalog=Testdb;Persist Security Info=False;User ID=ramesh;Password=Admin@1234567890;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            for (int i = 0; i < 10; i++)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand("InsertData");
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        private async Task<List<string>> GetData()
        {
            var data = new List<string>();
            using (HttpClient cl = new HttpClient())
            {
                await cl.GetAsync("https://api.github.com/users/octocat/orgs");
                for (int i = 0; i < 10; i++)
                {
                    data.Add($"Hello->{i}");
                }
            }
            return data;
        }
    }
}
