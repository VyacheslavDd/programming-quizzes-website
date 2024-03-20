using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Api.PostModels
{
    public class SubcategoryPostModel
    {
        public string? Name { get; set; }
        public Guid LanguageCategoryId { get; set; }
    }
}
