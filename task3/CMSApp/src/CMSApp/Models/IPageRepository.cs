using CMSApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMSApp.Models
{
    public interface IPageRepository : IDisposable
    {        
          IEnumerable<Page> GetAllPages();
        Page GetPageByID(int? pageId);
        void InsertPage(Page page);
        void DeletePage(int studentID);
        void UpdatePage(Page page);
        void Save();
        void InsertRelatedPages(int page1Id, int page2Id);
        bool checkUrl(int PageId);
        IEnumerable<NavLink> GetAllNavLinks();
    }
}
