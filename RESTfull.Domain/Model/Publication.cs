using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string PublicationName { get; set; } = String.Empty;
        public string PublicationTheme { get; set; } = String.Empty;
        public string PublicationDate { get; set; } = String.Empty;



        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;


    }
}
