﻿#pragma checksum "..\..\..\..\Views\ModifyUser.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "510A9E932054FE7F953A0452385C5D80938C7AE40541EB41403DBA478C3E4DC6"
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
    /// ModifyUser
    /// </summary>
    public partial class ModifyUser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close_btn;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userName_tb;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pwd_tb;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox age_tb;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nation_tb;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox department_tb;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_modify;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\ModifyUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cancel;
        
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
            System.Uri resourceLocater = new System.Uri("/Hegang.APP;component/views/modifyuser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ModifyUser.xaml"
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
            
            #line 8 "..\..\..\..\Views\ModifyUser.xaml"
            ((Hegang.APP.Views.ModifyUser)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.close_btn = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\Views\ModifyUser.xaml"
            this.close_btn.Click += new System.Windows.RoutedEventHandler(this.close_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.userName_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.pwd_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.age_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.nation_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.department_tb = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btn_modify = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\Views\ModifyUser.xaml"
            this.btn_modify.Click += new System.Windows.RoutedEventHandler(this.btn_modify_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\Views\ModifyUser.xaml"
            this.btn_cancel.Click += new System.Windows.RoutedEventHandler(this.close_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

