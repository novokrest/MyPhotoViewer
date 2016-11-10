using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyPhotoViewer.Validators
{
    public class ImagesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var httpFiles = ExtractHttpFileCollection(validationContext);
            
            if (httpFiles == null || !httpFiles.All(httpFile => ImageChecker.IsImage(httpFile.Data)))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }

        private ICollection<IHttpFile> ExtractHttpFileCollection(ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            return property.GetValue(validationContext.ObjectInstance) as ICollection<IHttpFile>;
        }
    }
}