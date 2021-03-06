﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using CustomControls;
using System.Collections.ObjectModel;
using ATADataModel;
using AutomationTestAssistantCore;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using FirstFloor.ModernUI.Windows;

namespace AutomationTestAssistantDesktopApp
{
    public partial class AddProjectView : UserControl, IContent
    {
        public AddProjectViewModel AddProjectViewModel { get; set; }
        public string ReturnUrl { get; set; }
        private const string ProjectSuccessfullyAddedMessage = "Project successfully added!";

        public AddProjectView()
        {
            InitializeComponent();
            AddProjectViewModel = new AddProjectViewModel();
            mainGrid.DataContext = AddProjectViewModel;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);          
            window.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            List<string> addiotionalPaths = AddProjectViewModel.ObservableAdditionalPaths.ToList();
            string projectName = tbProjectName.Text;
            string tfsPath = tbTfsPath.Text;
            string tfsUrl = tbTfsUrl.Text;
            ATACore.Managers.ProjectManager.AddNew(ATACore.Managers.ContextManager.Context, projectName, tfsPath, tfsUrl, addiotionalPaths);
            ATACore.Managers.ContextManager.Context.Dispose();
            ModernDialog.ShowMessage(ProjectSuccessfullyAddedMessage, "Success!", MessageBoxButton.OK);
            if (ReturnUrl.Equals("/AdminProjectSettingsView.xaml"))
            {
                Window window = Window.GetWindow(this);
                window.Close();
            }
            else
            {
                //AddSettingsWindow mnw = new AddSettingsWindow();
                //mnw.ContentSource = new Uri(ReturnUrl, UriKind.Relative);
                //mnw.Show();
                Navigator.Navigate(ReturnUrl, this);
            }
        }

        private void btnAddAdditionalPaths_Click(object sender, RoutedEventArgs e)
        {
            Navigator.Navigate("/AddAdditionalPathView.xaml#returnUrl=/AddProjectView.xaml", this);
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            FragmentManager fm = new FragmentManager(e.Fragment);
            ReturnUrl = fm.Fragments["returnUrl"];
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            AddProjectViewModel = new AddProjectViewModel();
        }
    }
}
