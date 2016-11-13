using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace MyPhotoViewer.ModelBinders
{
    public class ExtendedModelBinder : System.Web.Mvc.DefaultModelBinder
    {
        private static readonly IPropertyAttributeBinder<PropertyBindAttribute>[] PropertyAttributeBinders = new IPropertyAttributeBinder<PropertyBindAttribute>[] 
        {
            new PropertyAttributeBinder<UploadedFilesAttribute>(),
            new PropertyAttributeBinder<UploadedImageAttribute>()
        };

        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (PropertyAttributeBinders.Any(binder => binder.TryBind(controllerContext, bindingContext, propertyDescriptor)))
            {
                return;
            }

            base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }

    interface IPropertyAttributeBinder<out T> where T: PropertyBindAttribute
    {
        bool TryBind(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor);
    }

    public class PropertyAttributeBinder<T> : IPropertyAttributeBinder<T> where T : PropertyBindAttribute
    {
        public bool TryBind(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            var propBindAttr = propertyDescriptor.Attributes.OfType<T>().FirstOrDefault();

            return propBindAttr != null && propBindAttr.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}