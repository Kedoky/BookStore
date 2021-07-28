using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Moq;
using Ninject;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /*Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { Name = "Язык программирования C# 5.0 и платформа .NET 4.5", Author = "Троелсен Э", Price = 1179 },
                new Book { Name = "C# 5.0 и платформа .NET 4.5 для профессионалов", Author = "Карли Уотсон и др.", Price = 1155 },
                new Book { Name = "Ассинхронное программирование в C# 5.0", Author = "Девис А.", Price = 359 },
            });

            kernel.Bind<IBookRepository>().ToConstant(mock.Object);*/

            kernel.Bind<IBookRepository>().To<EFBookRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}