﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MahApps.MessegeBox
{
    /// <summary>
    /// Interaction logic for MessegeBox.xaml
    /// </summary>
    public partial class MessegeBox : Window
    {
        public MessegeBox()
        {
            InitializeComponent();
        }
        protected override void OnActivated(EventArgs e)
        {
            if (Owner != null)
            {
                if (Owner.WindowState == WindowState.Maximized)
                {
                    Left = 0;
                    Top = 200;
                    Width = System.Windows.SystemParameters.PrimaryScreenWidth;


                }
                else
                {
                    Left = Owner.Left + 1;
                    Top = Owner.Top + ((Owner.Height - 200) / 2);
                    Width = Owner.Width - 2;
                }
            }

            base.OnActivated(e);
        }

        private MessageBoxButton _Buttons = MessageBoxButton.OK;
        private MessageBoxResult _Result = MessageBoxResult.None;


        #region internal Properties
        internal MessageBoxButton Buttons
        {
            get { return _Buttons; }
            set
            {
                _Buttons = value;
                // Set all Buttons Visibility Properties
                SetButtonsVisibility();
            }
        }

        internal MessageBoxResult Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        #endregion

        #region SetButtonsVisibility Method
        internal void SetButtonsVisibility()
        {
            switch (_Buttons)
            {
                case MessageBoxButton.OK:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNoCancel:
                    btnOk.Visibility = Visibility.Collapsed;
                    btnCancel.Visibility = Visibility.Visible;
                    btnYes.Visibility = Visibility.Visible;
                    btnNo.Visibility = Visibility.Visible;
                    break;
            }
        }
        #endregion

        #region Button Click Events
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            this.Close();
        }
        #endregion

        #region Windows Drag Event
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove();
        }
        #endregion

        #region Deactivated Event
        private void Window_Deactivated(object sender, EventArgs e)
        {
            // If only an OK button is displayed, 
            // allow the user to just move away from this dialog box
            if (Buttons == MessageBoxButton.OK)
                this.Close();
        }
        #endregion


        public MessageBoxResult Show(string message)
        {
            return Show(message, string.Empty, MessageBoxButton.OK);
        }

        public MessageBoxResult Show(string message, string caption)
        {
            return Show(message, caption, MessageBoxButton.OK);
        }

        public MessageBoxResult Show(string message, string caption, MessageBoxButton buttons)
        {

            MessageBoxResult result = MessageBoxResult.None;




            title.Text = caption;
            tbMessage.Text = message;
            Buttons = buttons;
            // If just an OK button, allow the user to just move away from the dialog
            if (buttons == MessageBoxButton.OK)
                this.ShowDialog();
            else
            {
                this.ShowDialog();
                result = this.Result;
            }

            return result;
        }

    
    }
}
