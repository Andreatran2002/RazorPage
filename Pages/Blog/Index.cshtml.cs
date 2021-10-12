using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using entity_fr.models;

namespace entity_fr.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly entity_fr.models.MyBlogContext _context;

        public IndexModel(entity_fr.models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        public const int ITEMS_PER_PAGE = 10; 
        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage { get; set; }
        public int countPages{ get; set;}


        public async Task OnGetAsync(string SearchString)
        {
            int totalArticle = await _context.articles.CountAsync(); 
            countPages = (int)Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE); 
            
            if (currentPage <1 ) currentPage = 1; 
            if (currentPage > countPages ) currentPage = countPages; 

            // Article = await _context.articles.ToListAsync();
            var qr=(from a in _context.articles
                  orderby a.Created descending 
                  select a).Skip((currentPage-1)*10).Take(ITEMS_PER_PAGE); 
            if (!string.IsNullOrEmpty(SearchString)){
                Article = qr.Where( t => t.Title.Contains(SearchString)).ToList(); 
            }
            else{

            Article = await qr.ToListAsync();  
            }
            

        }
    }
}