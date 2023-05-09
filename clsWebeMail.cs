using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;

namespace WebMail
{
    public class clsWebeMail
    {
        System.Net.Mail.SmtpClient client = null;


        #region Initializeation
        /// <summary>
        /// Web Mail initiated with default setup from WEB.CONFIG
        /// </summary>
        public clsWebeMail()
        {
            webConfigSetUp();
        }//eof
        /// <summary>
        /// Web Mail setup with google / custom setup
        /// </summary>
        /// <param name="setupChoice">Setup Choice : Google or Custom Setup</param>
        /// <param name="id">Google : GmailId / Custom : Custom Server User Id</param>
        /// <param name="password">Google : Gmail Password / Custom : Custom Server Password</param>
        /// <param name="host">Google : No Need to fill / Custom: SMTP Host</param>
        /// <param name="port">Google : No Need to fill / Custom: Network Port No</param>
        /// <param name="credential">Google : No Need to fill / Custom: Network Credential</param>
        /// <param name="enableSSL">Google : No Need to fill / Custom: SSL Status</param>
        public clsWebeMail(serverChoice setupChoice, string id, string password, string host = "smtp.gmail.com", int port = 587, bool credential = false, bool enableSSL = true)
        {
            int portNo = 0;
            try
            {
                if (string.IsNullOrEmpty(id.Trim()) == true)
                {
                    throw new Exception("ID cannot be null or blank; Please insert your Log-In ID .........");
                }
                if (string.IsNullOrEmpty(password) == true)
                {
                    throw new Exception("Password is not matched ..........");
                }
                if (setupChoice == serverChoice.Custom)
                {
                    if (string.IsNullOrEmpty(host) == true)
                    {
                        throw new Exception("Host Cannot be blank ..........");
                    }
                    if (Int32.TryParse(port.ToString().Trim(), out portNo) == false && port <= 0)
                    {
                        throw new Exception("Invalid port no. ..........");
                    }
                }
                if (setupChoice == serverChoice.Google)
                {
                    registerMailClient(id.Trim(), password.Trim(), "smtp.gmail.com", 587, false, true);
                }
                else
                {
                    registerMailClient(id.Trim(), password.Trim(), host, port, credential, enableSSL);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }//eof
        #endregion

        #region Mail Setups
        private void webConfigSetUp()
        {
            string host = "";
            string passwd = "";
            string userName = "";
            int port = 25;
            bool defaultCredential = false;
            try
            {
                // MS Office 365 – security support need TLS 1.2 ; From Jan 23,2022 it seems enforcement will be there 
                //To manage same in case office 365 SMTP server access case below line need to add -  Bappaditya Sinha.  
                // This setting should be set at top of method 
                // If SMTP not office.365 or Google then check with provider if not support TLS then to remove.
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                //Programmatically accessing the SMTP mail Settings
                Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
                host = settings.Smtp.Network.Host;
                passwd = settings.Smtp.Network.Password;
                userName = settings.Smtp.Network.UserName;
                port = settings.Smtp.Network.Port;
                defaultCredential = settings.Smtp.Network.DefaultCredentials;
                //programmatically by pass the web.config settings
                if (string.IsNullOrEmpty(host) == true)
                {
                    //host = "localhost";
                    //port = 25;
                    throw new Exception("Host cannot be null or blank; Please check the Mail Settings .........");
                }
                if (string.IsNullOrEmpty(userName) == true || string.IsNullOrEmpty(passwd) == true)
                {
                    //userName = "sa";
                    //passwd = "sa";
                    throw new Exception("User Name or Password is not matched; Please check the Mail Settings ..........");
                }
                registerMailClient(userName.Trim(), passwd.Trim(), host, port, defaultCredential, true);

                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }

            /*When you have your SMTP settings in the web.config file, ASP.NET will automatically default to those settings.  
        * So, you don't want to specify "localhost" which would tell .
        * NET to try and send the email using IIS installed on your webserver. 
        * */

            /*WEB. CONFIG SETTINGS
             * <configuration>
             *<system.net>
                <mailSettings>      
                    <smtp from="noreply@yourdomain.com" deliveryMethod="Network">  //----- OPTIONAL
                        <network
                            host="localhost"
                            port="25"
                            defaultCredentials="true" 
                            userName="XX"
                            password="ZZ"/>/>  //-------<  password=  >and< userName=  > here
                    </smtp>
                </mailSettings>
                </system.net> 
              </configuration>
             * 
             * */
        }//eof
        private void registerMailClient(string id, string password, string host, int port, bool credential, bool SSL)
        {
            try
            {
                client = new System.Net.Mail.SmtpClient(host, port);
                client.UseDefaultCredentials = credential;
                client.EnableSsl = SSL;
                client.Credentials = new System.Net.NetworkCredential(id, password);
            }
            catch (SmtpException SMTPEx)
            {
                throw (SMTPEx);
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }//eof
        #endregion

        /// <summary>
        /// Send Mail With CC and BCC;
        /// </summary>
        /// <param name="To">email address of receiver;</param>
        /// <param name="From">email address of sender;</param>
        /// <param name="Cc">CC address [ Comma separated email address ];</param>
        /// <param name="Bccs">BCC address [ Comma separated email address ];</param>
        /// <param name="subject">Subject of the email;</param>
        /// <param name="body">Main body of the email;</param>
        /// <param name="isHTML">Mail Format(true/false) - if false, then all html signs will remove from the mail body and subject;</param>
        public void SendMails(string To, string From, string subject, string body, bool isHTML = true, string Cc = "NA", string Bcc = "NA")
        {
            System.Net.Mail.MailMessage objMailMsg = null;
            string strMsg = @body.Trim();
            string strSub = @subject.Trim();
            bool status = false;
            string[] strAddr;
            try
            {
                objMailMsg = new System.Net.Mail.MailMessage();

                if (isHTML == false)
                {
                    msgBody(ref @strMsg);
                    msgBody(ref @strSub);
                }
                To = To.Replace("\"", "");
                From = From.Replace("\"", "");
                Cc = string.IsNullOrWhiteSpace(Cc.Trim()) ? "NA" : Cc;
                Bcc = string.IsNullOrWhiteSpace(Bcc.Trim()) ? "NA" : Bcc;

                if (status == false)
                {
                    if (string.IsNullOrWhiteSpace(From.Trim()))
                    {
                        throw new Exception("Define the Address of Mail Sender...........");
                    }
                    if (string.IsNullOrWhiteSpace(@strMsg.Trim()))
                    {
                        throw new Exception("Blank Message cannot be sent...........");
                    }
                    status = true;
                }

                if (status == true)
                {

                    #region Address : TO
                    if (!string.IsNullOrWhiteSpace(To.Trim()))
                    {
                        if (To.LastIndexOf(',') == (To.Length - 1))
                        {
                            To = To.Substring(0, To.LastIndexOf(','));
                        }
                        if (To.IndexOf(',') >= 0)
                        {
                            if (To.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray().Count() > 0)
                            {
                                strAddr = To.Split(',');
                                foreach (string addr in strAddr)
                                {
                                    if (addr.Trim() != "")
                                    {
                                        objMailMsg.To.Add(new System.Net.Mail.MailAddress(addr));
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("NO Address found for 'TO'...........");
                            }
                        }
                        else
                        {
                            objMailMsg.To.Add(new System.Net.Mail.MailAddress(To));
                        }
                    }
                    else
                    {
                        throw new Exception("NO Address found for 'TO'...........");
                    }//To
                    #endregion

                    #region Address : CC
                    if (Cc.Trim().ToUpper() != "NA" && string.IsNullOrWhiteSpace(Cc.Trim()) == false)
                    {
                        if (Cc.LastIndexOf(',') == (Cc.Length - 1))
                        {
                            Cc = Cc.Substring(0, Cc.LastIndexOf(','));
                        }
                        if (Cc.IndexOf(',') > 0)
                        {
                            strAddr = Cc.Split(',');
                            foreach (string addr in strAddr)
                            {
                                if (!string.IsNullOrWhiteSpace(addr.Trim()))
                                {
                                    objMailMsg.CC.Add(new System.Net.Mail.MailAddress(addr));
                                }
                            }
                        }
                        else
                        {
                            objMailMsg.CC.Add(new System.Net.Mail.MailAddress(Cc));
                        }
                    }//Cc
                    #endregion

                    #region Address : BCC
                    if (Bcc.Trim().ToUpper() != "NA" && string.IsNullOrWhiteSpace(Bcc.Trim()) == false)
                    {
                        if (Bcc.LastIndexOf(',') == (Bcc.Length - 1))
                        {
                            Bcc = Bcc.Substring(0, Bcc.LastIndexOf(','));
                        }
                        if (Bcc.IndexOf(',') > 0)
                        {
                            strAddr = Bcc.Split(',');
                            foreach (string addr in strAddr)
                            {
                                if (addr.Trim() != "")
                                {
                                    objMailMsg.Bcc.Add(new System.Net.Mail.MailAddress(addr));
                                }
                            }
                        }
                        else
                        {
                            objMailMsg.Bcc.Add(new System.Net.Mail.MailAddress(Bcc));
                        }
                    }//Bcc 
                    #endregion

                    objMailMsg.From = new System.Net.Mail.MailAddress(From);
                    objMailMsg.Subject = @strSub.Length > 0 ? @strSub : "System Mail";
                    objMailMsg.Body = @strMsg.Trim();
                    objMailMsg.Priority = MailPriority.High;
                    objMailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    objMailMsg.IsBodyHtml = isHTML;

                    client.Send(objMailMsg);
                }
            }
            catch (System.FormatException Fex)
            {
                throw (Fex);
            }
            catch (SmtpException STMPex)
            {
                throw (STMPex);
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objMailMsg.Dispose();
            }

        }//eof - Simple mail

        /// <summary>
        /// Send Mail With CC and BCC and Attachments;
        /// </summary>
        /// <param name="To">email address of receiver;</param>
        /// <param name="Cc">CC address [ Comma separated email address ];</param>
        /// <param name="Bccs">BCC address [ Comma separated email address ];</param>
        /// <param name="subject">Subject of the email;</param>
        /// <param name="body">Main body of the email;</param>
        /// <param name="isHTML">Mail Format(true/false) - if false, then all html signs will remove from the mail body and subject;</param>
        /// <param name="attachFile">string of File path;</param>
        /// <param name="attachFileSize">Set maximum attached file size (less than or equal) in byte; Set 0 if you do not need the check of attached file size;</param>
        public void SendMailsWithAttachments(string To, string From, string subject, string body, string attachFile, bool isHTML = true, int attachFileSize = 0, string Cc = "NA", string Bcc = "NA")
        {
            System.Net.Mail.MailMessage objMailMsg = null;
            string strMsg = @body.Trim();
            string strSub = @subject.Trim();
            bool status = false;
            string attachedDoc = "";
            string[] strAddr;
            try
            {
                objMailMsg = new System.Net.Mail.MailMessage();

                if (isHTML == false)
                {
                    msgBody(ref @strMsg);
                    msgBody(ref @strSub);
                }
                To = To.Replace("\"", "");
                From = From.Replace("\"", "");
                Cc = string.IsNullOrWhiteSpace(Cc.Trim()) ? "NA" : Cc;
                Bcc = string.IsNullOrWhiteSpace(Bcc.Trim()) ? "NA" : Bcc;

                if (status == false)
                {
                    if (string.IsNullOrWhiteSpace(From.Trim()))
                    {
                        throw new Exception("Define the Address of Mail Sender...........");
                    }
                    if (string.IsNullOrWhiteSpace(@strMsg.Trim()))
                    {
                        throw new Exception("Blank Message cannot be sent...........");
                    }
                    if(!checkAttachment(attachFile))
                    {
                        throw new Exception("No File found for Attachment...........");
                    }
                    if(attachFileSize!=0)
                    {
                        if(!checkAttachmentSize(attachFile, attachFileSize))
                        {
                            throw new Exception("Unable to Attach File...........[File Size is larger than "+attachFileSize+ "] byte");
                        }
                    }
                    status = true;
                }

                if (status == true)
                {

                    #region Address : TO
                    if (!string.IsNullOrWhiteSpace(To.Trim()))
                    {
                        if (To.LastIndexOf(',') == (To.Length - 1))
                        {
                            To = To.Substring(0, To.LastIndexOf(','));
                        }
                        if (To.IndexOf(',') >= 0)
                        {
                            if (To.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray().Count() > 0)
                            {
                                strAddr = To.Split(',');
                                foreach (string addr in strAddr)
                                {
                                    if (addr.Trim() != "")
                                    {
                                        objMailMsg.To.Add(new System.Net.Mail.MailAddress(addr));
                                    }
                                }
                            }
                            else
                            {
                                throw new Exception("NO Address found for 'TO'...........");
                            }
                        }
                        else
                        {
                            objMailMsg.To.Add(new System.Net.Mail.MailAddress(To));
                        }
                    }
                    else
                    {
                        throw new Exception("NO Address found for 'TO'...........");
                    }//To
                    #endregion

                    #region Address : CC
                    if (Cc.Trim().ToUpper() != "NA" && string.IsNullOrWhiteSpace(Cc.Trim()) == false)
                    {
                        if (Cc.LastIndexOf(',') == (Cc.Length - 1))
                        {
                            Cc = Cc.Substring(0, Cc.LastIndexOf(','));
                        }
                        if (Cc.IndexOf(',') > 0)
                        {
                            strAddr = Cc.Split(',');
                            foreach (string addr in strAddr)
                            {
                                if (!string.IsNullOrWhiteSpace(addr.Trim()))
                                {
                                    objMailMsg.CC.Add(new System.Net.Mail.MailAddress(addr));
                                }
                            }
                        }
                        else
                        {
                            objMailMsg.CC.Add(new System.Net.Mail.MailAddress(Cc));
                        }
                    }//Cc
                    #endregion

                    #region Address : BCC
                    if (Bcc.Trim().ToUpper() != "NA" && string.IsNullOrWhiteSpace(Bcc.Trim()) == false)
                    {
                        if (Bcc.LastIndexOf(',') == (Bcc.Length - 1))
                        {
                            Bcc = Bcc.Substring(0, Bcc.LastIndexOf(','));
                        }
                        if (Bcc.IndexOf(',') > 0)
                        {
                            strAddr = Bcc.Split(',');
                            foreach (string addr in strAddr)
                            {
                                if (addr.Trim() != "")
                                {
                                    objMailMsg.Bcc.Add(new System.Net.Mail.MailAddress(addr));
                                }
                            }
                        }
                        else
                        {
                            objMailMsg.Bcc.Add(new System.Net.Mail.MailAddress(Bcc));
                        }
                    }//Bcc 
                    #endregion

                    attachedDoc = System.Web.HttpContext.Current.Server.MapPath(attachFile);
                    objMailMsg.From = new System.Net.Mail.MailAddress(From);
                    objMailMsg.Attachments.Add(new System.Net.Mail.Attachment(attachedDoc));
                    objMailMsg.Subject = @strSub;
                    objMailMsg.Body = @strMsg.Trim();
                    objMailMsg.Priority = MailPriority.High;
                    objMailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    objMailMsg.IsBodyHtml = isHTML;

                    client.Send(objMailMsg);
                }
            }
            catch (System.FormatException Fex)
            {
                throw (Fex);
            }
            catch (SmtpException STMPex)
            {
                throw (STMPex);
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objMailMsg.Dispose();
            }

        }//eof - attached mail
        
        #region Support Functions

        private void msgBody(ref string @msgBody)
        {
            string strBody = @msgBody;
            try
            {
                if (@strBody.Trim() != "")
                {
                    strBody = @strBody.Replace("</", " ");
                    strBody = @strBody.Replace("/>", " ");
                    strBody = @strBody.Replace("<!", " ");
                }
                msgBody = "";
                msgBody = @strBody.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //
            }
        }//eof
        private bool checkAttachment(string attachDoc)
        {
            bool isValid = false;
            string path = "";
            System.IO.FileInfo myFile = null;
            try
            {
                if (string.IsNullOrEmpty(attachDoc.Trim()) == false)
                {
                    path = System.Web.HttpContext.Current.Server.MapPath(attachDoc);
                    myFile = new System.IO.FileInfo(path);
                    if (myFile.Exists == true)
                    {
                        isValid = true;
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                myFile = null;
            }
        }//eof
        private bool checkAttachmentSize(string attachDoc, int iSize)
        {
            bool isValid = false;
            string path = "";
            System.IO.FileInfo myFile = null;
            try
            {
                if (string.IsNullOrEmpty(attachDoc.Trim()) == false)
                {
                    path = System.Web.HttpContext.Current.Server.MapPath(attachDoc);
                    myFile = new System.IO.FileInfo(path);
                    if (myFile.Exists == true)
                    {
                        if (myFile.Length <= iSize)
                        {
                            isValid = true;
                        }
                    }
                }
                return isValid;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                myFile = null;
            }
        }//eof
        public int checkMail(string strMailList)
        {
            int ireturn = 0;
            int iLength = 0;
            string strMail = "";
            try
            {
                if (string.IsNullOrWhiteSpace(strMailList))
                {
                    ireturn = 0;
                }
                else
                {
                    iLength = strMailList.Trim().Length;
                    if (strMailList.Last() == ',')
                    {
                        strMailList = strMailList.Substring(0, strMailList.LastIndexOf(','));
                    }

                    if (strMailList.Contains(","))
                    {
                        foreach (var vMail in strMailList.Split(','))
                        {
                            strMail = "";
                            strMail = vMail;
                            if (isEmail(strMail.Trim()))
                            {
                                //epic custom set up
                                //if (strMail.Trim().Contains("@epic"))
                                //{
                                //    ireturn = 1;
                                //}
                                //else
                                //{
                                //    ireturn = -2;
                                //    break;
                                //}

                                ireturn = 1;
                            }
                            else
                            {
                                ireturn = -1;
                                break;
                            }
                        }

                        if (ireturn == 1)
                        {
                            ireturn = iLength;
                        }
                    }
                    else
                    {
                        strMail = "";
                        strMail = strMailList;
                        if (isEmail(strMail.Trim()))
                        {
                            //epic custom set up
                            if (strMail.Trim().Contains("@epic"))
                            {
                                ireturn = iLength;
                            }
                            else
                            {
                                ireturn = -2;
                            }

                            //if the above set up on please off this
                            //ireturn = iLength;
                        }
                        else
                        {
                            ireturn = -1;
                        }
                    }


                }
                return ireturn;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {

            }
        }//eof
        private bool isEmail(string inputEmail)
        {
            int iAtTheRatePosition = 0;
            int iDotPosition = 0;
            if (!string.IsNullOrWhiteSpace(inputEmail))
            {
                //string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                //      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                //      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                //System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(strRegex);
                //if (re.IsMatch(inputEmail))
                //{
                //    return (true);
                //}
                //else
                //{
                //    return (false);
                //}
                iAtTheRatePosition = inputEmail.LastIndexOf('@');
                iDotPosition = inputEmail.LastIndexOf('.');
                if (iAtTheRatePosition <= 0)
                {
                    return (false);
                }
                else if (iDotPosition <= 0)
                {
                    return (false);
                }
                else if (iDotPosition <= iAtTheRatePosition)
                {
                    return (false);
                }
                else
                {
                    return (true);
                }

            }
            else
            {
                return (true); //if string is empty
            }
        }//eof
        public enum serverChoice
        {
            Google = 0,
            Custom = 1
        }//eof
        #endregion
    }
}