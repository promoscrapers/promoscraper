using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
   public class EmailEntity
    {

        
        string _FirstName; public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        string _LastName; public string LastName { get { return _LastName; } set { _LastName = value; } }
        string _email; public string Email { get { return _email; } set { _email = value; } }
        string _Subject; public string Subject { get { return _Subject; } set { _Subject = value; } }
        string _EmaiLBody; public string EmaiLBody { get { return _EmaiLBody; } set { _EmaiLBody = value; } }
       
    }
}
