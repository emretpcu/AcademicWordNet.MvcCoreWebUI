using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRule
{
    public class ContentValidator:AbstractValidator<Entry>
    {
        public ContentValidator()
        {
            
        }
    }
}
