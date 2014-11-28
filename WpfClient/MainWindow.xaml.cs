using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using RestClientLib;
using Shared;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Token = null;
        MainVM getArticleListVM;
        public MainWindow()
        {
            InitializeComponent();
            getArticleListVM = new MainVM();
            this.DataContext = getArticleListVM;
            getArticleListVM.Endpoint = txtendpoint.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            getArticleListVM.goForIt(Token);
            setWebResponseRequest(getArticleListVM);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var insert = new InsertVM();
            insert.Endpoint = txtendpoint.Text;
            insert.ArticleIns = new Article()
            {
                Id = Id.Text,
                Title = Title.Text,
                Abstract = Abstract.Text,
                Category = Category.Text,
                Body = Body.Text,
                IsDeleted = false,
                IsPublished = IsPublished.IsChecked
            };
            insert.goForIt(Token);
            //txtoutput.Text = insert.Id;
            setWebResponseRequest(insert);
        }

        private void setWebResponseRequest(EndpointVM wc)
        {
            this.txtsended.Text = wc.LastResponse;
            this.txtoutput.Text = wc.LastRequest;
            this.txtReturnCode.Text = wc.LastRequestCode;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Id.Text = "";
            Title.Text = "";
            Abstract.Text = "";
            Category.Text = "";
            Body.Text = "";
            IsPublished.IsChecked = false;
        }

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {
            var specific = new GetByIdVM();
            specific.Endpoint = txtendpoint.Text;
            Article result = specific.goForIt((Article)lstArticles.SelectedItem, Token);
            GetAbstract.Text = result.Abstract;
            GetCategory.Text = result.Category;
            GetBody.Text = result.Body;
            GetIsPublished.IsChecked = result.IsPublished;
            GetIsDeleted.IsChecked = result.IsDeleted;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = new DeleteVM();
            delete.Endpoint = txtendpoint.Text;
            delete.goForIt((Article)lstArticles.SelectedItem,Token);
        }

        private void lstArticles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Article selection = (Article)lstArticles.SelectedItem;
            if (selection != null)
            {
                UpdId.Text = selection.Id;
                UpdTitle.Text = selection.Title;
                UpdAbstract.Text = selection.Abstract;
                UpdCategory.Text = selection.Category;
                UpdBody.Text = selection.Body;
                UpdIsPublished.IsChecked = false;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var update = new UpdateVM();
            update.Endpoint = txtendpoint.Text;
            update.ArticleUpd = new Article()
            {
                Id = UpdId.Text,
                Title = UpdTitle.Text,
                Abstract = UpdAbstract.Text,
                Category = UpdCategory.Text,
                Body = UpdBody.Text,
                IsDeleted = false,
                IsPublished = UpdIsPublished.IsChecked
            };
            update.goForIt(Token);
            txtoutput.Text = update.Id;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string serviceNamespace = "restfulproject";
            string acsHostUrl = "accesscontrol.windows.net";
            string realm = @"http://k31:57614/NewsRESTService.svc";
            string uid = "prova";
            string pwd = "prova";
            Token = RestClientLib.GetToken.GetTokenFromACS(realm, serviceNamespace, acsHostUrl, uid, pwd);
        }

        private void btnDispose_Click(object sender, RoutedEventArgs e)
        {
            this.Token = String.Empty;
        }
    }
}
