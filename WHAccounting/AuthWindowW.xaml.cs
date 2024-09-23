using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WHAccounting.Classes;

namespace WHAccounting
{
    public partial class AuthWindowW : Window
    {
        private string _login;
        private string _phoneNumber;
        private string _password;
        private string _confirmPassword;

        private bool _IsLoginConfirmed;
        private bool _IsPhoneNumberConfirmed;
        private bool _IsPasswordConfirmed;
        private bool _IsConfirmPasswordConfirmed;
        bool _userExists;

        AppContextW db;
        User user;

        public AuthWindowW()
        {
            InitializeComponent();

            db = new AppContextW();

            regAuthButton.Content = "Sign up";
        }

        private void Registration_Button(object sender, RoutedEventArgs e)
        {
            _login = textBoxLogin.Text.Trim();
            _phoneNumber = textBoxPhone.Text.Trim();
            _password = passwordBoxEnterPassword.Password.Trim();
            _confirmPassword = passwordBoxConfirmPassword.Password.Trim();

            if (regAuthButton.Content == "Sign up")
            {
                if (string.IsNullOrEmpty(_login) || string.IsNullOrWhiteSpace(_login))
                {
                    textBoxLogin.ToolTip = "Логін не може бути порожнім";
                    textBoxLogin.Foreground = Brushes.Red;
                    _IsLoginConfirmed = false;
                }
                else if (_login.Length > 20)
                {
                    textBoxLogin.ToolTip = "Логін не може бути довший за 20 символів";
                    textBoxLogin.Foreground = Brushes.Red;
                    _IsLoginConfirmed = false;
                }
                else
                {
                    textBoxLogin.ToolTip = "";
                    textBoxLogin.Foreground = Brushes.Black;
                    _IsLoginConfirmed = true;
                }

                if (string.IsNullOrEmpty(_phoneNumber) || string.IsNullOrWhiteSpace(_phoneNumber))
                {
                    textBoxPhone.ToolTip = "Номер телефону не може бути порожнім";
                    textBoxPhone.Foreground = Brushes.Red;
                    _IsPhoneNumberConfirmed = false;

                }
                else if (!_phoneNumber.StartsWith("+"))
                {
                    textBoxPhone.ToolTip = "Номер телефону повинен починатись з '+'";
                    textBoxPhone.Foreground = Brushes.Red;
                    _IsPhoneNumberConfirmed = false;
                }
                else if (!IsPhoneNumberValid(_phoneNumber))
                {
                    textBoxPhone.ToolTip = "Номер телефону повинен складатися з цифр";
                    textBoxPhone.Foreground = Brushes.Red;
                    _IsPhoneNumberConfirmed = false;
                }
                else if (_phoneNumber.Length > 13)
                {
                    textBoxPhone.ToolTip = "Номер телефону не може бути більше 13 символів";
                    textBoxPhone.Foreground = Brushes.Red;
                    _IsPhoneNumberConfirmed = false;
                }
                else
                {
                    textBoxPhone.ToolTip = "";
                    textBoxPhone.Foreground = Brushes.Black;
                    _IsPhoneNumberConfirmed = true;
                }

                if (string.IsNullOrEmpty(_password) || string.IsNullOrWhiteSpace(_password))
                {
                    passwordBoxEnterPassword.ToolTip = "Пароль не може бути порожнім";
                    passwordBoxEnterPassword.Foreground = Brushes.Red;
                    _IsPasswordConfirmed = false;
                }
                else if (_password.Length < 6)
                {
                    passwordBoxEnterPassword.ToolTip = "Пароль не може бути менше 6 символів";
                    passwordBoxEnterPassword.Foreground = Brushes.Red;
                    _IsPasswordConfirmed = false;
                }
                else
                {
                    passwordBoxEnterPassword.ToolTip = "";
                    passwordBoxEnterPassword.Foreground = Brushes.Black;
                    _IsPasswordConfirmed = true;
                }

                if (string.IsNullOrEmpty(_confirmPassword) || string.IsNullOrWhiteSpace(_confirmPassword))
                {
                    passwordBoxConfirmPassword.ToolTip = "Пароль не може бути порожнім";
                    passwordBoxConfirmPassword.Foreground = Brushes.Red;
                    _IsConfirmPasswordConfirmed = false;
                }
                else if (_password != _confirmPassword)
                {
                    passwordBoxConfirmPassword.ToolTip = "Паролі не співпадають";
                    passwordBoxConfirmPassword.Foreground = Brushes.Red;
                    _IsConfirmPasswordConfirmed = false;
                }
                else
                {
                    passwordBoxConfirmPassword.ToolTip = "";
                    passwordBoxConfirmPassword.Foreground = Brushes.Black;
                    _IsConfirmPasswordConfirmed = true;
                }

                if (_IsLoginConfirmed && _IsPhoneNumberConfirmed && _IsPasswordConfirmed && _IsConfirmPasswordConfirmed)
                {

                    _userExists = db.Users.Any(u => u.UPhoneNumber == _phoneNumber);

                    if (_userExists)
                    {
                        MessageBox.Show("За даним телефоном вже є аккаунт, введіть інший телефон або увійдіть в існуючий аккаунт.");
                    }
                    else
                    {
                        user = new User(_login, _phoneNumber, _password);
                        db.Users.Add(user);
                        db.SaveChanges();
                        MessageBox.Show($"Користувач '{_login}' успішно зареєстрований!");
                    }
                }
            }
            else
            {
                _userExists = db.Users.Any(u => u.UPhoneNumber == _phoneNumber && u.UPassword == _password);

                if (_userExists)
                {
                    MainWorkSpace mws = new MainWorkSpace();
                    mws.Closed += SecondWindow_Closed;
                    Visibility = Visibility.Hidden;
                    mws.Show();
                }
                else
                {
                    MessageBox.Show($"Користувача не знайдено");
                }
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            return phoneNumber.Skip(1).All(char.IsDigit);
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            signInButtonSection.Style = (Style)FindResource("MaterialDesignFlatDarkBgButton");
            signUpButtonSection.Style = (Style)FindResource("MaterialDesignFlatDarkButton");

            textBoxLogin.Visibility = Visibility.Collapsed;
            passwordBoxConfirmPassword.Visibility = Visibility.Collapsed;

            regAuthButton.Content = "Sign in";
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            signUpButtonSection.Style = (Style)FindResource("MaterialDesignFlatDarkBgButton");
            signInButtonSection.Style = (Style)FindResource("MaterialDesignFlatDarkButton");

            textBoxLogin.Visibility = Visibility.Visible;
            passwordBoxConfirmPassword.Visibility = Visibility.Visible;

            regAuthButton.Content = "Sign up";
        }

        private void SecondWindow_Closed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }
    }
}