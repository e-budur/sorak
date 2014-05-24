using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Globalization;

namespace Sorak.Model.ClientModel
{
    public class SorakUser
    {
        private IPrincipal _user;

        public SorakUser(IPrincipal user, string ipAddress, string machineName)
        {
            this._user = user;
            this._clientMachineName = machineName;
            this._ipAddress = ipAddress;
        }

        public SorakUser()
        {

        }

        private string _companyId = "OTH";

        public string CompanyId
        {
            get
            {
                _companyId = "OTH";

                return _companyId;
            }
            set { _companyId = value; }
        }

        private string _photoUri = "/Content/images/employee_photos/unisex_profile.png";
        public string PhotoUri
        {
            get
            {
                return _photoUri;
            }
            set
            {
                _photoUri = value ?? _photoUri;
            }
        }

        private int _recordNum;

        public int RecordNum
        {
            get { return _recordNum; }
            set { _recordNum = value; }
        }

        private string _ipAddress;

        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
            }
        }

        private string _clientMachineName;

        public string ClientMachineName
        {
            get
            {
                return _clientMachineName;
            }
            set
            {
                _clientMachineName = value;
            }
        }

        private string _windowsUserName;

        public string WindowsUserName
        {
            get
            {
                if (string.IsNullOrEmpty(_windowsUserName))
                {
                    SetUserWindowsName();
                }

                return _windowsUserName;
            }
            set
            {
                _windowsUserName = value;
            }
        }

        private void SetUserWindowsName()
        {
            _windowsUserName = _user.Identity.Name.Split('\\')[1];
        }

        private string _domain;

        public string Domain
        {
            get
            {
                if (string.IsNullOrEmpty(_domain))
                {
                    SetUserDomain();
                }

                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        public void SetUserDomain()
        {
            _domain = _user.Identity.Name.Split('\\')[0];
        }

        private UserPrincipal _userPrincipal;

        private UserPrincipal Principal
        {
            get
            {
                if (_userPrincipal == null)
                {
                    SetUserPrincipal();
                }

                return _userPrincipal;
            }
            set
            {
                _userPrincipal = value;
            }
        }

        private void SetUserPrincipal()
        {
            using (var context = new PrincipalContext(ContextType.Domain, this.ADDomain, null, ContextOptions.Negotiate, @"teknoloji\formpro", "TaSnIf2006"))
            {
                _userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, this.WindowsUserName);
            }
        }

        private string ADDomain
        {
            get
            {
                if (string.IsNullOrEmpty(this.Domain))
                    return "sorak.com.tr";

                return "sorak.com.tr";
                
            }
        }

        private string _emailAddress;

        public string EmailAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_emailAddress))
                    _emailAddress = Principal.EmailAddress;

                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
            }
        }

        private string _fullName;

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                    _fullName = Principal.GivenName + " " + Principal.Surname;

                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public bool IsNewUser()
        {
            return RecordNum <= 0;
        }
    }
}
