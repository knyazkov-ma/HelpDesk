﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDesk.DataService.Common.DTO.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HelpDesk.DataService.Common.DTO.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Принята в работу.
        /// </summary>
        internal static string StatusRequestEnum_Accepted {
            get {
                return ResourceManager.GetString("StatusRequestEnum_Accepted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выполнена.
        /// </summary>
        internal static string StatusRequestEnum_ApprovedComplete {
            get {
                return ResourceManager.GetString("StatusRequestEnum_ApprovedComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Закрыта исполнителем.
        /// </summary>
        internal static string StatusRequestEnum_Closing {
            get {
                return ResourceManager.GetString("StatusRequestEnum_Closing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Перенос готовности.
        /// </summary>
        internal static string StatusRequestEnum_ExtendedConfirmation {
            get {
                return ResourceManager.GetString("StatusRequestEnum_ExtendedConfirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Перенос срока.
        /// </summary>
        internal static string StatusRequestEnum_ExtendedDeadLine {
            get {
                return ResourceManager.GetString("StatusRequestEnum_ExtendedDeadLine", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Новая.
        /// </summary>
        internal static string StatusRequestEnum_New {
            get {
                return ResourceManager.GetString("StatusRequestEnum_New", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не подтверждена готовность.
        /// </summary>
        internal static string StatusRequestEnum_NotApprovedComplete {
            get {
                return ResourceManager.GetString("StatusRequestEnum_NotApprovedComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пассив.
        /// </summary>
        internal static string StatusRequestEnum_Passive {
            get {
                return ResourceManager.GetString("StatusRequestEnum_Passive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Отказано.
        /// </summary>
        internal static string StatusRequestEnum_Rejected {
            get {
                return ResourceManager.GetString("StatusRequestEnum_Rejected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Состояние не определено.
        /// </summary>
        internal static string StatusRequestEnum_Unknown {
            get {
                return ResourceManager.GetString("StatusRequestEnum_Unknown", resourceCulture);
            }
        }
    }
}
