using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity_fr.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace entity_fr.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContext myBlogContext;
        public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myContext)
        {
            _logger = logger;
            myBlogContext = _myContext; 
        }

        public void OnGet()
        {
            var posts = (from a in myBlogContext.articles
                        orderby a.Created descending
                        select a).ToList();
            ViewData["Posts"] =posts; 
        }
    }
}
