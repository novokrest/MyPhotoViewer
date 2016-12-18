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

                if (files.Count != 1)
                {
                    bindingContext.ModelState.AddModelError(propertyDescriptor.DisplayName, "Choose photo for uploading");
                    return true;
                }

                HttpPostedFileBase uploadedFile = files[0];
                byte[] data = uploadedFile.ReadData();
                if (!ImageChecker.IsImage(data))
                {
                    bindingContext.ModelState.AddModelError(propertyDescriptor.DisplayName, "Choose photo for uploading");
                    return true;
                }

                Image image = Image.Create(data, uploadedFile.ContentType);
                propertyDescriptor.SetValue(bindingContext.Model, image);
                return true;
            }

            return false;
        }
    }
}