<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BPWEBAccessControl.About" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid frmSupportContainer02">

        <div class="row frmSupportContainer02-header">
            <div class="col-md-6 col-sm-6 text-left">
                <%--<asp:Label ID="lblCustIDName" runat="server" Visible="false"></asp:Label>--%>
            </div>
            <div class="col-md-6 col-sm-6 text-right">
                <asp:Label ID="lblfrmName" runat="server">frmName</asp:Label>
            </div>
        </div>

        <div class="row frmSupportContainer02-body">
            <div class="col-md-3">
                <p class="font-small">
                    This site best viewed with Microsoft EDGE or Google Chrome.  This site is an application client. If you have any pop up blocking option on you can deactivate for this site. If you are facing any technical problem to view this site, feel free to mail to our 
        <a href="mailto: BHSAdmin@bhsinfotech.com" target="_blank" style="font-weight: bold; font-size: 8pt; color: #0000FF; font-family: Verdana, Arial; text-decoration: none">tech support team. </a>&nbsp;
                </p>
            </div>
            <div class="col-md-9">
                <h3><%=BPWEBAccessControl.Global.TitleText() %></h3>
                <hr style="color: darkgray;" />
                <p>
                    This application has designed to help PW office Admin, to follow-up and track basic info about employees, bank info, lease  and business registration. 
                </p>
                <br />
                <hr style="color: darkgray;" />
                <small><% = BPWEBAccessControl.Global.FooterText()%></small>
                <hr style="color: darkgray;" />
                <br />
                <br />
                <p>
                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="w3-button" OnClick="lblClose_Click">Close</asp:LinkButton>
                </p>
            </div>
        </div>

        <div class="row frmSupportContainer02-footer">
            <div class="col-md-12 col-sm-12">
                <div class="row">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4">
                        <p class="text-center text-info">
                            <small>Copyright © BHS Infotech 2021. All rights reserved.</small>
                        </p>
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
