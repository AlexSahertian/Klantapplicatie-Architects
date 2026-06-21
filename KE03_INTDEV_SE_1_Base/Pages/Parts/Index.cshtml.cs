using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages.Parts
{
    public class IndexModel : PageModel
    {
        private readonly IPartRepository _partRepository;

        public List<Part> Parts { get; set; } = new();

        public IndexModel(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public void OnGet()
        {
            Parts = _partRepository.GetAllParts().ToList();
        }
    }
}