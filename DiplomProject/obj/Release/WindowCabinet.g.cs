﻿#pragma checksum "..\..\WindowCabinet.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D376EDDB699EA1FD74B66144A0420F694DA405B0C309333DEE05A89B67D435C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DiplomProject;
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


namespace DiplomProject {
    
    
    /// <summary>
    /// WindowCabinet
    /// </summary>
    public partial class WindowCabinet : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelPhone;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelName;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupBoxEdit;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBoxNew;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBoxOld;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxNumberEdit;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxLoginEdit;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonEditUser;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelLogin;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\WindowCabinet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelErrorCab;
        
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
            System.Uri resourceLocater = new System.Uri("/DiplomProject;component/windowcabinet.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowCabinet.xaml"
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
            
            #line 8 "..\..\WindowCabinet.xaml"
            ((DiplomProject.WindowCabinet)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labelPhone = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.labelName = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.groupBoxEdit = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 5:
            this.passwordBoxNew = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.passwordBoxOld = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 7:
            this.textBoxNumberEdit = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\WindowCabinet.xaml"
            this.textBoxNumberEdit.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textBoxNumberEdit_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.textBoxLoginEdit = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.buttonEditUser = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\WindowCabinet.xaml"
            this.buttonEditUser.Click += new System.Windows.RoutedEventHandler(this.buttonEditUser_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.labelLogin = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.labelErrorCab = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

