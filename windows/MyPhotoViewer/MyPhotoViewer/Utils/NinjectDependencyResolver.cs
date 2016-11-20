using MyPhotoViewer.DAL;
using MyPhotoViewer.DAL.Entity;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace MyPhotoViewer.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            RegisterBindings();
        }

        private void RegisterBindings()
        {
            _kernel.Bind<IPhotosDbContext>().To<PhotosDbContext>();
            _kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            _kernel.Bind<IPhotoAlbumRepository>().To<PhotoAlbumRepository>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}