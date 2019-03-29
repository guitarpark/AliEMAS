using AliEMAS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AliEMASTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        IAliEMAS service = DependencyService.Get<IAliEMAS>();
        private void Button_Clicked(object sender, EventArgs e)
        {
            service.UserRegister("测试-苹果-UserRegister");
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            service.UserUpdateAccount("测试-苹果-UserUpdateAccount",Guid.NewGuid().ToString());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            service.UserLoginOut();
        }
    }
}
