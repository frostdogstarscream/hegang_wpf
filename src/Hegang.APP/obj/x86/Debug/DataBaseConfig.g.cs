﻿#pragma checksum "..\..\..\DataBaseConfig.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C5EE9061A2526438AAF7DB91BF309B5EFA6F391E2A090290C59B22D8A9D3639"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Hegang.APP;
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


namespace Hegang.APP {
    
    
    /// <summary>
    /// DataBaseConfig
    /// </summary>
    public partial class DataBaseConfig : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minimize_btn;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button close_btn;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox server;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox port;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox user;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox password;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox database;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\DataBaseConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_apply;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\DataBaseConfig.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Hegang.APP;component/databaseconfig.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DataBaseConfig.xaml"
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
            
            #line 8 "..\..\..\DataBaseConfig.xaml"
            ((Hegang.APP.DataBaseConfig)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\DataBaseConfig.xaml"
            ((Hegang.APP.DataBaseConfig)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.minimize_btn = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\DataBaseConfig.xaml"
            this.minimize_btn.Click += new System.Windows.RoutedEventHandler(this.minimize_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.close_btn = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\DataBaseConfig.xaml"
            this.close_btn.Click += new System.Windows.RoutedEventHandler(this.close_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.server = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.port = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.user = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.database = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btn_apply = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\DataBaseConfig.xaml"
            this.btn_apply.Click += new System.Windows.RoutedEventHandler(this.btn_apply_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_cancel = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\DataBaseConfig.xaml"
            this.btn_cancel.Click += new System.Windows.RoutedEventHandler(this.btn_cancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

