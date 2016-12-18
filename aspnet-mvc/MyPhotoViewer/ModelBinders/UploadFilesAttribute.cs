using MyPhotoViewer.Converters;
using MyPhotoViewer.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoViewer.ModelBinders
{
    public class UploadedFilesAttribute : PropertyBindAttribute
    {
        private readonly HttpFileCreator _httpFileCreator = new HttpFileCreator();

        public override bool BindProperty(ControllerContext controllerContext, 
                                          ModelBindingContext bindingContext, 
                                          PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType.IsAssignableFrom(typeof(List<IHttpFile>)))
            {
                HttpFileCollectionBase fileCollection = controllerContext.HttpContext.Request.Files;
                if (fileCollection != null && fileCollection.Count > 0)
                {
                    var files = new List<IHttpFile>();

                    for (int i = 0, count = fileCollection.Count; i < count; i++)
                    {
                        var file = _httpFileCreator.Create(fileCollection[i]);
                        files.Add(file);
                    }

                    propertyDescriptor.SetValue(bindingContext.Model, files);

                    return true;
                }
            }

            return false;
        }
    }
}