﻿#pragma checksum "..\..\..\..\Views\Login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "98C34E28C008CCD3AEDE0E138350D5212A953776D6163AD70C97CDCA351BFD70"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Hegang.APP.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Hegang.APP.Views {
    
    
    /// <summary>
    /// Login
    /// </summary>
    public partial class Login : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button min_btn;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close_btn;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox id;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwd;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton user_rb;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton admin_rb;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button login_btn;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Views\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button reg_btn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hegang.APP;component/views/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Login.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\Views\Login.xaml"
            ((Hegang.APP.Views.Login)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.min_btn = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\Views\Login.xaml"
            this.min_btn.Click += new System.Windows.RoutedEventHandler(this.min_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.close_btn = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\Views\Login.xaml"
            this.close_btn.Click += new System.Windows.RoutedEventHandler(this.close_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.pwd = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.user_rb = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.admin_rb = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.login_btn = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\..\Views\Login.xaml"
            this.login_btn.Click += new System.Windows.RoutedEventHandler(this.login_btn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.reg_btn = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\..\Views\Login.xaml"
            this.reg_btn.Click += new System.Windows.RoutedEventHandler(this.reg_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

