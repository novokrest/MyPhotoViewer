using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.ModelBinders
{
    public class UploadFilePathsAttribute : PropertyBindAttribute
    {
        public override bool BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(IEnumerable<string>))
            {
                HttpFileCollectionBase files = controllerContext.HttpContext.Request.Files;
                if (files != null && files.Count > 0)
                {
                    var filePaths = new List<string>();

                    for (int i = 0, count = files.Count; i < count; i++)
                    {
                        filePaths.Add(files[i].FileName);
                    }

                    propertyDescriptor.SetValue(bindingContext.Model, filePaths);

                    return true;
                }
            }

            return false;
        }
    }
}