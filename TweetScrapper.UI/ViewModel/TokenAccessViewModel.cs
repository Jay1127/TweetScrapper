using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    /// <summary>
    /// Token access view's datacontext
    /// </summary>
    public class TokenAccessViewModel : ViewModelBase, IRequestCloseViewModel
    {
        /// <summary>
        /// Tweet access token
        /// </summary>
        public Token Token { get; set; }

        /// <summary>
        /// consumer key(binding textbox)
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// consumer secret(binding textbox)
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Authorize oauth token(binding button)
        /// </summary>
        public RelayCommand AccessCommand { get; set; }

        /// <summary>
        /// if authorize token, return true
        /// </summary>
        public bool IsAccessed { get; set; }

        /// <summary>
        /// request close event(after click button, raise event)
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// constructor
        /// </summary>
        public TokenAccessViewModel()
        {
            AccessCommand = new RelayCommand(AccessToken);
            ConsumerKey = "egjNlASi3o3r4nriynMAnlTjE";
            ConsumerSecret = "fhuBNzdwrs1Tt9NrkzCvYJbzQeCr5WvIGvHPOG95lod9SVJ7Qd";
        }

        /// <summary>
        /// AccessToken command's action
        /// </summary>
        private void AccessToken()
        {
            try
            {
                Token = Authorizer.Authorize(ConsumerKey, ConsumerSecret);
                IsAccessed = true;
            }
            catch(Exception e)
            {
                IsAccessed = false;
            }
            finally
            {
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
