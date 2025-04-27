using ProjectModel;
using System.IO;
using System.Xml.Linq;

namespace ProjectDAL
{
    public static class Register
    {
        private static readonly string filePath = "LoginData.xml";

        public static bool NewUser(User user)
        {
            try
            {
                XDocument doc;

                // If file doesn't exist, create a new document
                if (!File.Exists(filePath))
                {
                    doc = new XDocument(new XElement("Users"));
                }
                else
                {
                    doc = XDocument.Load(filePath);
                }

                // Check if username already exists
                var existingUser = doc.Descendants("User")
                                      .FirstOrDefault(u => u.Element("UserName")?.Value == user.UserName);

                if (existingUser != null)
                {
                    return false; // Username already taken
                }

                // Add new user
                XElement newUser = new XElement("User",
                    new XElement("UserName", user.UserName),
                    new XElement("Password", user.Password)
                );

                doc.Root.Add(newUser);
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
