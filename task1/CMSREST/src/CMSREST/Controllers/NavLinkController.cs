using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
    public class NavLinkController : ApiController
    {

        public INavLinkRepository NavLinkRepository { get; set; }

        /* public NavLinkController(ApplicationDbContext context)
         {
             this.NavLinkRepository = new NavLinkRepository(context);
         }*/
        public NavLinkController(INavLinkRepository NavLinkRepository)
        {
            this.NavLinkRepository = NavLinkRepository;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/NavLinks/5
        [HttpGet("{id}", Name = "GetNavLink")]
        public IActionResult GetById(int id)
        {
            var item = NavLinkRepository.GetAll().Where(m => m.NavLinkId == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }


        [HttpPost]
        public IActionResult Create([FromBody]NavLink item)
        {

            if (item == null)
            {
                return BadRequest();
            }
            NavLinkRepository.Add(item);
            NavLinkRepository.Save();
            return CreatedAtRoute("GetNavLinks", new { id = item.NavLinkId }, item);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = NavLinkRepository.Remove(id);
            if (todo == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] NavLink item, int id)
        {

            if (item == null)
            {
                return BadRequest();
            }

            var change = NavLinkRepository.GetAll().Where(m => m.NavLinkId == id).ToList().FirstOrDefault();
            if (change == null)
            {
                return NotFound();
            }
            if (item.ParentLinkId != 0) change.ParentLinkId = item.ParentLinkId;
            if (item.PageId!= 0) change.PageId = item.PageId;
            if (item.Position != null) change.Position = item.Position;
            if (item.Title != null) change.Title = item.Title;
            NavLinkRepository.Update(change);
            NavLinkRepository.Save();
            return new NoContentResult();


        }

    }
}

