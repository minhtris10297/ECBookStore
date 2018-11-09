using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowledgeStore.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { set; get; }
        public string Email { set; get; }
    }
}