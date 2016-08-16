using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMSREST.Models;
using CMSREST.Data;
using System.Web.Http;

namespace CMSREST.Controllers
{
    [Route("api/[controller]")]
    public class PagesController : ApiController
    {

        public IPageRepository pageRepository { get; set; }

     /*   public PagesController(ApplicationDbContext context)
        {
            this.pageRepository = new PageRepository(context);
           
        }*/
    
     public PagesController(IPageRepository pageRepository)
        {
            this.pageRepository = pageRepository;
        }

        // GET api/list
        // api/pages/list?title=s&offset=3&count=5
        [HttpGet("list")]
        public IActionResult List(string title,int? offset,int? count)
        {
            var res = pageRepository.GetAll();
            if (title!=null) res = res.Where(m => m.Title.Contains(title));
            if (offset != null && count != null) {
                res = res.Skip(offset.Value).Take(count.Value);             
                    }
            return new ObjectResult(res);
        }

        // GET api/pages/5
        [HttpGet("{id}", Name = "GetPages")]
        public IActionResult GetById(int id)
        {
            var item = pageRepository.GetAll().Where(m=>m.PageId==id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody]Page item)
        {
         
                 if (item == null)
                 {
                     return BadRequest();
                 }
                 pageRepository.Add(item);
                 pageRepository.Save();
                 return CreatedAtRoute("GetPages", new { id = item.PageId }, item);
                    
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo=pageRepository.Remove(id);
            if (todo == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }
        //localhost:50454/api/pages/24
        //page in body
        [HttpPatch("{id}")]
        public IActionResult Update( [FromBody] Page item, int id)
        {

                if (item == null)
                {
                    return BadRequest();
                }

                var change = pageRepository.GetAll().Where(m => m.PageId == id).ToList().FirstOrDefault();
                if (change == null)
                {
                    return NotFound();
                }
            if (item.UrlName != null) change.UrlName = item.UrlName;
            if (item.Title != null) change.Title = item.Title;
            if (item.Description != null) change.Description = item.Description;
            if (item.Content != null) change.Content = item.Content;
            pageRepository.Update(change);
                pageRepository.Save();
                return new NoContentResult();
        
            
        }

    }
}
