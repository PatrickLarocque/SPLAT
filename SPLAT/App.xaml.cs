using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SPLAT.MVVM.View;
using SPLAT.Services;
using Refit;
using SPLAT.Http;
using SPLAT.Requests;
using System.Security.Cryptography.X509Certificates;
using SPLAT.Responses;
using SPLAT.Extensions;
using SPLAT.MVVM.ViewModel;

namespace SPLAT
{

    public partial class App : Application
    {


        protected void OnStartup(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();


        }

    }

}
/*
 * 
 *    var loginView = new LoginView();
            loginView.Show();
 *   var registerView = new RegisterView();
            registerView.Show();
 */