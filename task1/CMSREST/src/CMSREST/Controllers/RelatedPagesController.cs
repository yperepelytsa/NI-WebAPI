using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMSREST.Models;
using CMSREST.Data;
using System.Web.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CMSREST.Controllers
{
    [Route("api/[controller]")]
    public class RelatedPagesController : ApiController
    {

        public IRelatedPagesRepository relatedPagesRepository { get; set; }

      /*  public RelatedPagesController (ApplicationDbContext context)
        {
            this.relatedPagesRepository = new RelatedPagesRepository(context);
        }*/
        public RelatedPagesController(IRelatedPagesRepository relatedPagesRepository)
        {
            this.relatedPagesRepository = relatedPagesRepository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/RelatedPagess/5
        [HttpGet("{id}", Name = "GetRelatedPages")]
        public IActionResult GetById(int id)
        {
            var item = relatedPagesRepository.GetAll().Where(m => m.RelatedPagesId == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody]RelatedPages item)
        {

            if (item == null)
            {
                return BadRequest();
            }
            relatedPagesRepository.Add(item);
            relatedPagesRepository.Save();
            return CreatedAtRoute("GetRelatedPagess", new { id = item.RelatedPagesId }, item);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = relatedPagesRepository.Remove(id);
            if (todo == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] RelatedPages item, int id)
        {

            if (item == null)
            {
                return BadRequest();
            }

            var change = relatedPagesRepository.GetAll().Where(m => m.RelatedPagesId == id).ToList().FirstOrDefault();
            if (change == null)
            {
                return NotFound();
            }
            if (item.Page1Id != 0) change.Page1Id = item.Page1Id;
            if (item.Page2Id != 0) change.Page2Id = item.Page2Id;
          
            relatedPagesRepository.Update(change);
            relatedPagesRepository.Save();
            return new NoContentResult();


        }

    }
}
