using MyPhotoViewer.Core;
using MyPhotoViewer.Extensions;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.ModelBinders
{
    public class UploadedImageAttribute : PropertyBindAttribute
    {
        public override bool BindProperty(ControllerContext controllerContext, 
                                          ModelBindingContext bindingContext, 
                                          PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(Image))
            {
                HttpFileCollectionBase files = controllerContext.HttpContext.Request.Files;
                //Verifiers.Verify(files.Count == 1, "Unexpected uploaded files count: {0}", files.Count);

                HttpPostedFileBase uploadedFile = files[0];
                Image image = Image.Create(uploadedFile.ReadData(), uploadedFile.ContentType);

                propertyDescriptor.SetValue(bindingContext.Model, image);
                return true;
            }

            return false;
        }
    }
}