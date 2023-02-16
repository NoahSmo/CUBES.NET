using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.ViewModel;

namespace WpfApp
{
    static class ModeCommun
    {
        public static HttpClient client;

        public static UserViewModel CurrentUser;
    }
}
