using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MyPhotoViewer.ModelBinders
{
    public class ExtendedModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            var propBindAttr = propertyDescriptor.Attributes.OfType<UploadedFilesAttribute>().FirstOrDefault();

            if (propBindAttr != null && propBindAttr.BindProperty(controllerContext, bindingContext, propertyDescriptor))
            {
                return;
            }

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}