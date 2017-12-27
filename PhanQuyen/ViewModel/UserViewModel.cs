﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;
namespace ViewModel
{
    public class UserViewModel
    {
        #region Initialize
        private User user;
        private String userID;
        private String password;
        private bool success;
        public Boolean Success
        {
            get { return success; }
            set
            {
                success = value;
                OnPropertyChanged("Success");
            }
        }
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public String UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                OnPropertyChanged("UserID");
            }
        }
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }


        #endregion

        public UserViewModel()
        {
            //Innitialize();
            LoginCommand = new RelayCommand<UIElementCollection>((p) => true, Login);
        }
        private void Login(UIElementCollection p)
        {
            //User = UserDBViewModel.getInstance.getUser(UserID, Password);
            //if (User.UserName != null)
            //    HandleLoginSuccess();
            //else
            //    HandleLoginFail();
        }
        private void HandleLoginSuccess()
        {
            var w = Application.Current.Windows[1];
            w.Close();
        }
        private void HandleLoginFail()
        {
            MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
        }
        public ICommand LoginCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));

        }
    }
}
