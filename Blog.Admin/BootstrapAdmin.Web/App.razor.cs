﻿using BootstrapAdmin.Web.Services;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapAdmin.Web
{
    /// <summary>
    /// 
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        [Inject]
        [NotNull]
        private BootstrapAppContext? AppContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            //Title = DictHelper.RetrieveWebTitle(AppContext.AppId);
        }
    }
}