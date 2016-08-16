using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSApp.Models
{
    public interface IPageRepository
    {
        Task<IEnumerable<Page>> GetAllPages(string searchTitle);
        Task<Page> GetPageByID(int pageId);
        Task InsertPage(Page page);
        Task DeletePage(int pageID);
        Task UpdatePage(Page page);
       
    }
}
