<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BPWEBAccessControl._Default" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="padding-top: 35px;">
        <div class="col-md-9">
            <div id="picblock" class="login-form-lefBase">
                <div class="login-form-leftpicContainer LeftContainer-image">
                    <img src="Picture/A003.png" />
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="login-form-rightContainer">
                <div class="row">
                    <div class="col-md-12 my-opacity-min myCard" style="background-color: white; margin-left: 5px; margin-right: 5px;">
                        <div style="text-align: left">
                            <img style="vertical-align: middle; height: 40px; width: 40px;" src="Picture/ADMIN_ACC.png" alt="Admin Acc">
                            <span style="vertical-align: middle">Application Login</span>
                        </div>
                        <p>
                            <asp:LinkButton ID="linkBtnLogin" runat="server" CssClass="btn btn-default" OnClick="linkBtnLogin_Click">Log In</asp:LinkButton>
                        </p>
                    </div>
                </div>
                <div class="row" style="padding-top: 20px;">
                </div>
                <div class="row">
                    <div class="col-md-12 my-opacity-min myCard" style="background-color: white; margin-left: 5px; margin-right: 5px;">
                        <div style="text-align: left">
                            <img style="vertical-align: middle; height: 40px; width: 40px;" src="Picture/ChangePassword.png" alt="Admin Acc">
                            <span style="vertical-align: middle">Manage account</span>
                        </div>
                        <asp:LinkButton ID="linkPwdRenew" runat="server" CssClass="btn btn-default" OnClick="linkPwdRenew_Click">Open</asp:LinkButton>
                    </div>
                </div>
                <div class="row" style="padding-top: 5px;">
                </div>
                <div class="row">
                    <div class="col-md-12 my-opacity-min myCard" style="background-color: white; margin-left: 5px; margin-right: 5px;">
                        <div style="text-align: left">
                            <img style="vertical-align: middle; height: 40px; width: 40px;" src="Picture/Register_ACC.png" alt="Admin Acc">
                            <span style="vertical-align: middle">Administrator log in</span>
                        </div>
                        <asp:LinkButton ID="lnkBtnAdminAccess" CssClass="btn btn-default" runat="server" Text="Data admin login" OnClick="lnkBtnAdminAccess_Click"></asp:LinkButton>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div id="LogInContainer" runat="server" class="myMsgBoxStyleContainer">
        <div class="row">
            <div id="LogIn" runat="server" class="col-sm-12 col-md-4 myMsgBoxStyle login-form-continer-admin">

                <br />
                <div style="text-align: right;">
                    <asp:LinkButton ID="lnkClose" runat="server" Text="Close" OnClick="lnkClose_Click"></asp:LinkButton>
                </div>
                <h4>
                    <asp:Label ID="lblAppLink" runat="server"></asp:Label>
                </h4>
                <label>User ID</label>
                <div>
                    <asp:TextBox ID="TextUserID" runat="server" CssClass="form-control underline-only-ctrl" placeholder="User ID"></asp:TextBox>
                </div>
                <label>Password</label>
                <div>
                    <asp:TextBox ID="TextPWD" runat="server" CssClass="form-control underline-only-ctrl" TextMode="Password" placeholder="Password"></asp:TextBox>
                </div>
                <label>Site</label>
                <div>
                    <asp:DropDownList ID="ddlSite" runat="server" CssClass="form-control dropdown underline-only-ctrl"></asp:DropDownList>
                </div>
                <div>
                    <br />
                    <asp:LinkButton ID="btnLogIn"
                        runat="server"
                        CssClass="btn btn-primary" OnClick="btnLogIn_Click">
                                <span aria-hidden="true" class="glyphicon glyphicon-log-in"></span>&nbsp;Log In
                    </asp:LinkButton>
                </div>
                <div style="margin-bottom: 10px;">
                    <br />
                    <asp:Panel ID="panError" runat="server" CssClass="text-danger" Visible="false">
                        <asp:Label ID="TxtMsgBox" runat="server" Style="word-wrap: break-word;"></asp:Label>
                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
