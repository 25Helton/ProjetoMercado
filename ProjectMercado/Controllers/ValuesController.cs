using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ProjectMercado.Model;

namespace ProjectMercado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<List<Categoria>> Get()
        {

           var url = "https://www.globo.com/";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var result = htmlDocument.DocumentNode.Descendants("img")
                .Where(node => node.GetAttributeValue("class", "").Equals("hui-highlight-terciary__photo hui-lazy")).ToList();


            var listCad = new List<Categoria>();

            foreach (var item in result)
            {
                var categoria = new Categoria
                {
                    Nome = item.ChildAttributes("title").FirstOrDefault().Value,
                    UrlImagem = item.ChildAttributes("data-src").FirstOrDefault().Value
                };

                listCad.Add(categoria);
            }

            return listCad;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
