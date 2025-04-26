using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerHouseSecurity;

namespace AuthorInsights
{
    public partial class Login : System.Web.UI.Page
    {
        /**
         * CODED BY: Dignan Jaslowich
         */
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookies = Request.Cookies["cookieId"]; //getting info from cookies saved with cookieID

            if ((cookies == null) || (cookies["Username"] == "")) //checking if any info was saved
            {
                WelcomeLbl.Text = "Looks like you're not a member yet! Sign up for free below!"; //if info was not saved display new user info
                UsernameLbl.Text = "Username:";
            }
            else
            {
                WelcomeLbl.Text = "Welcome back " + cookies["Username"] + "!"; //if info was saved display welcome back info
                UsernameLbl.Text = "Username: (" + cookies["Username"] + ")";

                SavedUsernameLbl.Text = "Saved Username: " + cookies["Username"]; //if info found display info on testing lables
                SavedPasswordLbl.Text = "Saved Password: " + cookies["Password"];
            }
        }

        /**
         * CODED BY: Dignan Jaslowich
         */
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (SaveCookiesChkbx.Checked) //checking if user wants to save cookies or not
            {
                HttpCookie cookies = new HttpCookie("cookieID"); //making new cookies with cookie id
                cookies["Username"] = UsernameTxtbx.Text; //setting cookies username to entered username
                cookies["Password"] = PasswordTxtbx.Text; //setting cookies password to entered password
                cookies.Expires = DateTime.Now.AddMonths(12); //setting cookie to expire after a year
                Response.Cookies.Add(cookies); //adding created cookies object to cookies

                SavedUsernameLbl.Text = "Saved Username: " + cookies["Username"]; //setting saved username and password labels to show saved info
                SavedPasswordLbl.Text = "Saved Password: " + cookies["Password"];
            }



            //-------CODE BELOW HERE NEEDS TO BE CHANGED SO THAT LOGIN AUTHENTICATES USER BEFORE REDIRECTING THEM---------



            if(MemberLogin(UsernameTxtbx.Text, PasswordTxtbx.Text))
            {
                Response.Redirect("Member/MemberPage.aspx");
            }
            else
            {
                LoginFailedLbl.Text = "Login failed, username or password not recognized";
            }

            //this is what the code did before but it does not use the new members.xml file that we need it to use!!!
            //this checks the root directory web.config file and NOT the members.xml file!!!
            if(FormsAuthentication.Authenticate(UsernameTxtbx.Text, PasswordTxtbx.Text))
            {
                FormsAuthentication.RedirectFromLoginPage(UsernameTxtbx.Text, false);
            }
            else
            {
                LoginFailedLbl.Text = "Login failed, username or password not recognized";
            }
        }

        protected bool MemberLogin(string username, string password)
        {
            string filePath = Server.MapPath("~/App_Data/Members.xml"); // path to your XML file

            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(fs);
            fs.Close();

            System.Xml.XmlNode root = xd.DocumentElement; // get the root

            foreach (System.Xml.XmlNode member in root.ChildNodes) // loop through each <Member>
            {
                string xmlUsername = member["Username"]?.InnerText;
                string xmlPassword = member["Password"]?.InnerText;

                if (xmlUsername == username && xmlPassword == Security.HashPassword(password))
                {
                    return true;
                }
            }

            return false;
        }

        protected void MemberRegister(string username, string password)
        {
            string filePath = Server.MapPath("~/App_Data/Members.xml"); // XML file path

            System.Xml.XmlDocument xd = new System.Xml.XmlDocument();
            xd.Load(filePath);

            System.Xml.XmlNode root = xd.DocumentElement; // Members root

            // Create a new Member node
            System.Xml.XmlElement newMember = xd.CreateElement("Member");

            // Create Username node
            System.Xml.XmlElement userElement = xd.CreateElement("Username");
            userElement.InnerText = username;
            newMember.AppendChild(userElement);

            // Create Password node with hashed password
            System.Xml.XmlElement passElement = xd.CreateElement("Password");
            passElement.InnerText = Security.HashPassword(password);
            newMember.AppendChild(passElement);

            // Add new member to the root
            root.AppendChild(newMember);


            xd.Save(filePath);
        }
    }
}