using ProjectModel;
using System.IO;
using System.Xml.Linq;
using System.Linq;

namespace ProjectDAL
{
    public static class Login
    {
        private static readonly string filePath = "LoginData.xml";

        public static bool Authentication(User input)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return false; // No login file exists
                }

                XDocument doc = XDocument.Load(filePath);

                var users = doc.Descendants("User")
                               .Select(u => new User
                               {
                                   UserName = u.Element("UserName")?.Value,
                                   Password = u.Element("Password")?.Value
                               })
                               .ToList();

                // Check if any user matches
                return users.Any(u => u.UserName == input.UserName && u.Password == input.Password);
            }
            catch
            {
                return false;
            }
        }
    }
}
