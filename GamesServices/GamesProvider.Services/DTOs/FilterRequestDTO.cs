using System;
using System.Collections.Generic;
using System.Text;

namespace GamesProvider.Services.DTOs
{
    public class FilterRequestDTO
    {
        public int From { get; set; }
        public int CountPerPage { get; set; }
        public FilterOptionsDTO FilterOptions { get; set; }
    }
}
